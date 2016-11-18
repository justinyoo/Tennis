﻿using System;
using System.Configuration;

using Autofac;

using Tennis.Mappers;

using Tournaments.EntityModels;
using Tournaments.Helpers;
using Tournaments.Services;

namespace Tennis.Common.IoC
{
    /// <summary>
    /// This represents the service locator entity.
    /// </summary>
    public class ServiceLocator : IServiceLocator
    {
        private ContainerBuilder _builder;
        private IContainer _container;
        private bool _disposed;

        /// <summary>
        /// Initialises a new instance of the <see cref="ServiceLocator"/> class.
        /// </summary>
        /// <param name="autoRegister">Value that specifies whether to register required dependencies automatically or not. Default value is true.</param>
        public ServiceLocator(bool autoRegister = true)
        {
            this._builder = new ContainerBuilder();

            if (!autoRegister)
            {
                return;
            }

            this.RegisterDependencies();
        }

        /// <summary>
        /// Registers dependency.
        /// </summary>
        /// <typeparam name="TImplementer">Type to implement.</typeparam>
        /// <typeparam name="TService">Type to be used.</typeparam>
        /// <param name="func">The delegate to register.</param>
        public void Register<TImplementer, TService>(Func<IComponentContext, TImplementer> func)
        {
            this._builder.Register(func).As<TService>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Registers dependency.
        /// </summary>
        /// <typeparam name="TImplementer">Type to implement.</typeparam>
        /// <typeparam name="TService">Type to be used.</typeparam>
        public void RegisterType<TImplementer, TService>()
        {
            this._builder.RegisterType<TImplementer>().As<TService>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Builds all dependencies registered.
        /// </summary>
        public void Build()
        {
            this._container = this._builder.Build();
        }

        /// <summary>
        /// Gets the service instance to use.
        /// </summary>
        /// <typeparam name="TService">Type to be used.</typeparam>
        /// <returns>Returns the service instance resolved.</returns>
        public TService GetService<TService>()
        {
            using (var scope = this._container.BeginLifetimeScope())
            {
                var service = scope.Resolve<TService>();
                return service;
            }
        }

        /// <summary>
        /// Registers all dependencies pre-defined.
        /// </summary>
        public void RegisterDependencies()
        {
            // Helpers
            this.RegisterType<FeedHelper, IFeedHelper>();

            // Wrappers
            this.RegisterType<SyndicationFeedWrapper, ISyndicationFeedWrapper>();

            // Factories
            this.RegisterType<MapperFactory, IMapperFactory>();

            // Services
            this.RegisterType<FeedService, IFeedService>();
            this.RegisterType<PlayerService, IPlayerService>();
            this.RegisterType<TournamentService, ITournamentService>();

            // Contexts
            var connString = ConfigurationManager.ConnectionStrings["TournamentDbContext"].ConnectionString;
            this.Register<TournamentDbContext, ITournamentDbContext>(p => new TournamentDbContext(connString));
            this.RegisterType<FeedContext, IFeedContext>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._container.Dispose();
            this._builder = null;

            this._disposed = true;
        }
    }
}