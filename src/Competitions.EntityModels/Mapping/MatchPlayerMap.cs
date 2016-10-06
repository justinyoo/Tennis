using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="MatchPlayer"/> class.
    /// </summary>
    public class MatchPlayerMap : EntityTypeConfiguration<MatchPlayer>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="MatchPlayerMap"/> class.
        /// </summary>
        public MatchPlayerMap()
        {
            // Primary Key
            this.HasKey(p => p.MatchPlayerId);

            // Properties
            this.Property(p => p.MatchPlayerId).IsRequired();
            this.Property(p => p.MatchId).IsRequired();
            this.Property(p => p.PlayerId).IsOptional();
            this.Property(p => p.HomeOrAway).IsRequired().HasMaxLength(8);
            this.Property(p => p.PlayerNumber).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("MatchPlayer");
            this.Property(p => p.MatchPlayerId).HasColumnName("MatchPlayerId");
            this.Property(p => p.MatchId).HasColumnName("MatchId");
            this.Property(p => p.PlayerId).HasColumnName("PlayerId");
            this.Property(p => p.HomeOrAway).HasColumnName("HomeOrAway");
            this.Property(p => p.PlayerNumber).HasColumnName("PlayerNumber");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.Match)
                .WithMany(p => p.MatchPlayers)
                .HasForeignKey(p => p.MatchId);

            this.HasOptional(p => p.Player)
                .WithMany(p => p.MatchPlayers)
                .HasForeignKey(p => p.PlayerId);
        }
    }
}
