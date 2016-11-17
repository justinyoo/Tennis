using System;
using System.Collections.Generic;
using System.Linq;

using Competitions.EntityModels;
using Competitions.Services;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using Tennis.AppSettings;
using Tennis.AppSettings.Extensions;
using Tennis.Common.Blob;
using Tennis.Mappers;
using Tennis.WebApp.ServiceContexts;

using Tournaments.EntityModels;
using Tournaments.Helpers;
using Tournaments.Services;

namespace Tennis.WebApp
{
    /// <summary>
    /// This represents the entity for startup.
    /// </summary>
    public class Startup
    {
        private const string CompetitionsDbContext = "CompetitionDbContext";
        private const string TournamentsDbContext = "TournamentDbContext";
        private const string ScoreSheetsBlobContext = "ScoreSheetBlobContext";

        /// <summary>
        /// Initialises a new insatance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the <see cref="IConfigurationRoot"/> instance.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Adds services to the container. This method gets called by the runtime.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> instance.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStrings = this.Configuration.Get<List<ConnectionStringSettings>>("connectionStrings");
            var blobStorage = this.Configuration.Get<BlobStorageSettings>("blobStorage");

            // Add framework services.
            services.AddMvc()
                    .AddMvcOptions(o => o.Filters.Add(new RequireHttpsAttribute()))
                    .AddJsonOptions(o =>
                                    {
                                        o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                                        o.SerializerSettings.Converters.Add(new StringEnumConverter());
                                        o.SerializerSettings.Formatting = Formatting.Indented;
                                        o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                                        o.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                                    });

            services.AddAuthentication(o => o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            // DB Contexts
            services.AddScoped<ICompetitionDbContext, CompetitionDbContext>(_ => new CompetitionDbContext(connectionStrings.Single(p => p.Name.Equals(CompetitionsDbContext, StringComparison.CurrentCultureIgnoreCase)).ConnectionString));
            services.AddScoped<ITournamentDbContext, TournamentDbContext>(_ => new TournamentDbContext(connectionStrings.Single(p => p.Name.Equals(TournamentsDbContext, StringComparison.CurrentCultureIgnoreCase)).ConnectionString));

            // Blob Contexts
            var blobConnString = connectionStrings.Single(p => p.Name.Equals(ScoreSheetsBlobContext, StringComparison.CurrentCultureIgnoreCase));
            services.AddScoped<IBlobContainerConnectionOptions, BlobContainerConnectionOptions>(_ => new BlobContainerConnectionOptions(blobConnString, blobStorage));
            services.AddTransient<IBlobContainerConnection, BlobContainerConnection>();
            services.AddTransient<IBlobContainerContext, BlobContainerContext>();

            // Helpers
            services.AddTransient<IFeedHelper, FeedHelper>();

            // Wrappers
            services.AddTransient<ISyndicationFeedWrapper, SyndicationFeedWrapper>();

            // Contexts
            services.AddTransient<IClubServiceContext, ClubServiceContext>();
            services.AddTransient<ICompetitionServiceContext, CompetitionServiceContext>();
            services.AddTransient<IFeedContext, FeedContext>();
            services.AddTransient<ITournamentServiceContext, TournamentServiceContext>();

            // Factories
            services.AddTransient<IMapperFactory, MapperFactory>();

            // Services
            services.AddTransient<IClubService, ClubService>();
            services.AddTransient<ICompetitionService, CompetitionService>();
            services.AddTransient<IDistrictService, DistrictService>();
            services.AddTransient<IFixtureService, FixtureService>();
            services.AddTransient<Competitions.Services.IPlayerService, Competitions.Services.PlayerService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IVenueService, VenueService>();

            services.AddTransient<Tournaments.Services.IPlayerService, Tournaments.Services.PlayerService>();
            services.AddTransient<ITournamentService, TournamentService>();
        }

        /// <summary>
        /// Configures the HTTP request pipeline. This method gets called by the runtime.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> instance.</param>
        /// <param name="env"><see cref="IHostingEnvironment"/> instance.</param>
        /// <param name="loggerFactory"><see cref="ILoggerFactory"/> instance.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var auth = this.Configuration.Get<AuthenticationSettings>("authentication");

            loggerFactory.AddConsole(Configuration.GetSection("logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCookieAuthentication();

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
                                               {
                                                   ClientId = auth.AzureAd.ClientId,
                                                   ClientSecret = auth.AzureAd.ClientSecret,
                                                   Authority = $"{auth.AzureAd.AadInstance.TrimEnd('/')}/{auth.AzureAd.TenantId}",
                                                   CallbackPath = auth.AzureAd.CallbackPath,
                                                   ResponseType = OpenIdConnectResponseType.CodeIdToken
                                               });

            app.UseMvcWithDefaultRoute();
        }
    }
}