using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="District"/> class.
    /// </summary>
    public class DistrictMap : EntityTypeConfiguration<District>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DistrictMap"/> class.
        /// </summary>
        public DistrictMap()
        {
            // Primary Key
            this.HasKey(p => p.DistrictId);

            // Properties
            this.Property(p => p.DistrictId).IsRequired();
            this.Property(p => p.Name).IsRequired().HasMaxLength(128);
            this.Property(p => p.Url).IsOptional().HasMaxLength(512);
            this.Property(p => p.TrolsUrl).IsOptional().HasMaxLength(512);
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("District");
            this.Property(p => p.DistrictId).HasColumnName("DistrictId");
            this.Property(p => p.Name).HasColumnName("Name");
            this.Property(p => p.Url).HasColumnName("Url");
            this.Property(p => p.TrolsUrl).HasColumnName("TrolsUrl");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
        }
    }
}
