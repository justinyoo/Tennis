using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="CompetitionClub"/> class.
    /// </summary>
    public class CompetitionClubMap : EntityTypeConfiguration<CompetitionClub>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CompetitionClubMap"/> class.
        /// </summary>
        public CompetitionClubMap()
        {
            // Primary Key
            this.HasKey(p => p.CompetitionClubId);

            // Properties
            this.Property(p => p.CompetitionClubId).IsRequired();
            this.Property(p => p.CompetitionId).IsRequired();
            this.Property(p => p.ClubId).IsRequired();
            this.Property(p => p.ClubTag).IsOptional().HasMaxLength(8);
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("CompetitionClub");
            this.Property(p => p.CompetitionClubId).HasColumnName("CompetitionClubId");
            this.Property(p => p.CompetitionId).HasColumnName("CompetitionId");
            this.Property(p => p.ClubId).HasColumnName("ClubId");
            this.Property(p => p.ClubTag).HasColumnName("ClubTag");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.Competition)
                .WithMany(p => p.CompetitionClubs)
                .HasForeignKey(p => p.CompetitionId);

            this.HasRequired(p => p.Club)
                .WithMany(p => p.CompetitionClubs)
                .HasForeignKey(p => p.ClubId);
        }
    }
}
