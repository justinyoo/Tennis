using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="Competition"/> class.
    /// </summary>
    public class CompetitionMap : EntityTypeConfiguration<Competition>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CompetitionMap"/> class.
        /// </summary>
        public CompetitionMap()
        {
            // Primary Key
            this.HasKey(p => p.CompetitionId);

            // Properties
            this.Property(p => p.CompetitionId).IsRequired();
            this.Property(p => p.DistrictId).IsRequired();
            this.Property(p => p.Name).IsRequired().HasMaxLength(128);
            this.Property(p => p.Year).IsRequired();
            this.Property(p => p.Season).IsRequired().HasMaxLength(8);
            this.Property(p => p.Day).IsRequired().HasMaxLength(16);
            this.Property(p => p.Type).IsRequired().HasMaxLength(8);
            this.Property(p => p.Grade).IsRequired().HasMaxLength(32);
            this.Property(p => p.Level).IsRequired().HasMaxLength(8);
            this.Property(p => p.TrolsDaytimeCode).IsOptional().HasMaxLength(32);
            this.Property(p => p.TrolsSectionCode).IsOptional().HasMaxLength(32);
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("Competition");
            this.Property(p => p.CompetitionId).HasColumnName("CompetitionId");
            this.Property(p => p.DistrictId).HasColumnName("DistrictId");
            this.Property(p => p.Name).HasColumnName("Name");
            this.Property(p => p.Year).HasColumnName("Year");
            this.Property(p => p.Season).HasColumnName("Season");
            this.Property(p => p.Day).HasColumnName("Day");
            this.Property(p => p.Type).HasColumnName("Type");
            this.Property(p => p.Grade).HasColumnName("Grade");
            this.Property(p => p.Level).HasColumnName("Level");
            this.Property(p => p.TrolsDaytimeCode).HasColumnName("TrolsDaytimeCode");
            this.Property(p => p.TrolsSectionCode).HasColumnName("TrolsSectionCode");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.District)
                .WithMany(p => p.Competitions)
                .HasForeignKey(p => p.DistrictId);
        }
    }
}
