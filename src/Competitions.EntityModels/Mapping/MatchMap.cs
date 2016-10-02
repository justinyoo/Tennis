using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="Match"/> class.
    /// </summary>
    public class MatchMap : EntityTypeConfiguration<Match>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MatchMap"/> class.
        /// </summary>
        public MatchMap()
        {
            // Primary Key
            this.HasKey(p => p.MatchId);

            // Properties
            this.Property(p => p.MatchId).IsRequired();
            this.Property(p => p.FixtureId).IsRequired();
            this.Property(p => p.SetNumber).IsRequired();
            this.Property(p => p.HomeSetScore).IsRequired();
            this.Property(p => p.AwaySetScore).IsRequired();
            this.Property(p => p.HomeGameScore).IsRequired();
            this.Property(p => p.AwayGameScore).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("Match");
            this.Property(p => p.MatchId).HasColumnName("MatchId");
            this.Property(p => p.FixtureId).HasColumnName("FixtureId");
            this.Property(p => p.SetNumber).HasColumnName("SetNumber");
            this.Property(p => p.HomeSetScore).HasColumnName("HomeSetScore");
            this.Property(p => p.AwaySetScore).HasColumnName("AwaySetScore");
            this.Property(p => p.HomeGameScore).HasColumnName("HomeGameScore");
            this.Property(p => p.AwayGameScore).HasColumnName("AwayGameScore");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.Fixture)
                .WithMany(p => p.Matches)
                .HasForeignKey(p => p.FixtureId);
        }
    }
}
