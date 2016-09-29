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
            this.Property(p => p.Url).IsOptional().HasMaxLength(512);
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("Competition");
            this.Property(p => p.CompetitionId).HasColumnName("CompetitionId");
            this.Property(p => p.DistrictId).HasColumnName("DistrictId");
            this.Property(p => p.Name).HasColumnName("Name");
            this.Property(p => p.Url).HasColumnName("Url");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.District)
                .WithMany(p => p.Competitions)
                .HasForeignKey(p => p.DistrictId);
        }
    }
}
