using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="ClubPlayer"/> class.
    /// </summary>
    public class ClubPlayerMap : EntityTypeConfiguration<ClubPlayer>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ClubPlayerMap"/> class.
        /// </summary>
        public ClubPlayerMap()
        {
            // Primary Key
            this.HasKey(p => p.ClubPlayerId);

            // Properties
            this.Property(p => p.ClubPlayerId).IsRequired();
            this.Property(p => p.ClubId).IsRequired();
            this.Property(p => p.PlayerId).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("ClubPlayer");
            this.Property(p => p.ClubPlayerId).HasColumnName("ClubPlayerId");
            this.Property(p => p.ClubId).HasColumnName("ClubId");
            this.Property(p => p.PlayerId).HasColumnName("PlayerId");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.Club)
                .WithMany(p => p.ClubPlayers)
                .HasForeignKey(p => p.ClubId);

            this.HasOptional(p => p.Player)
                .WithMany(p => p.ClubPlayers)
                .HasForeignKey(p => p.PlayerId);
        }
    }
}