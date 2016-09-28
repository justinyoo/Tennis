using System.Data.Entity.ModelConfiguration;

namespace Tournaments.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="Tournament"/> class.
    /// </summary>
    public class TournamentMap : EntityTypeConfiguration<Tournament>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TournamentMap"/> class.
        /// </summary>
        public TournamentMap()
        {
            // Primary Key
            this.HasKey(p => p.TournamentId);

            // Properties
            this.Property(p => p.TournamentId).IsRequired();
            this.Property(p => p.TournamentKey).IsRequired();
            this.Property(p => p.Title).IsRequired().HasMaxLength(256);
            this.Property(p => p.Summary).IsRequired().HasMaxLength(512);
            this.Property(p => p.DatePublished).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("Tournament");
            this.Property(p => p.TournamentId).HasColumnName("TournamentId");
            this.Property(p => p.TournamentKey).HasColumnName("TournamentKey");
            this.Property(p => p.Title).HasColumnName("Title");
            this.Property(p => p.Summary).HasColumnName("Summary");
            this.Property(p => p.DatePublished).HasColumnName("DatePublished");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
        }
    }
}