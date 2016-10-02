using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="TeamPlayer"/> class.
    /// </summary>
    public class TeamPlayerMap : EntityTypeConfiguration<TeamPlayer>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TeamPlayerMap"/> class.
        /// </summary>
        public TeamPlayerMap()
        {
            // Primary Key
            this.HasKey(p => p.TeamPlayerId);

            // Properties
            this.Property(p => p.TeamPlayerId).IsRequired();
            this.Property(p => p.PlayerId).IsOptional();
            this.Property(p => p.TeamId).IsRequired();
            this.Property(p => p.Order).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("TeamPlayer");
            this.Property(p => p.TeamPlayerId).HasColumnName("TeamPlayerId");
            this.Property(p => p.PlayerId).HasColumnName("PlayerId");
            this.Property(p => p.TeamId).HasColumnName("TeamId");
            this.Property(p => p.Order).HasColumnName("Order");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.Player)
                .WithMany(p => p.TeamPlayers)
                .HasForeignKey(p => p.PlayerId);

            this.HasRequired(p => p.Team)
                .WithMany(p => p.TeamPlayers)
                .HasForeignKey(p => p.TeamId);
        }
    }
}