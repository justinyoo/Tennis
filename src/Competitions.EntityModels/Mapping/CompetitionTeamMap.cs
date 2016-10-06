using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="CompetitionTeam"/> class.
    /// </summary>
    public class CompetitionTeamMap : EntityTypeConfiguration<CompetitionTeam>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CompetitionTeamMap"/> class.
        /// </summary>
        public CompetitionTeamMap()
        {
            // Primary Key
            this.HasKey(p => p.CompetitionTeamId);

            // Properties
            this.Property(p => p.CompetitionTeamId).IsRequired();
            this.Property(p => p.CompetitionId).IsRequired();
            this.Property(p => p.TeamId).IsRequired();
            this.Property(p => p.TeamNumber).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("CompetitionTeam");
            this.Property(p => p.CompetitionTeamId).HasColumnName("CompetitionTeamId");
            this.Property(p => p.CompetitionId).HasColumnName("CompetitionId");
            this.Property(p => p.TeamId).HasColumnName("TeamId");
            this.Property(p => p.TeamNumber).HasColumnName("TeamNumber");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.Competition)
                .WithMany(p => p.CompetitionTeams)
                .HasForeignKey(p => p.CompetitionId);

            this.HasOptional(p => p.Team)
                .WithMany(p => p.CompetitionTeams)
                .HasForeignKey(p => p.TeamId);
        }
    }
}