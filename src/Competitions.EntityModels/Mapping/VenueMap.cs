using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="Venue"/> class.
    /// </summary>
    public class VenueMap : EntityTypeConfiguration<Venue>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="VenueMap"/> class.
        /// </summary>
        public VenueMap()
        {
            // Primary Key
            this.HasKey(p => p.VenueId);

            // Properties
            this.Property(p => p.VenueId).IsRequired();
            this.Property(p => p.Address1).IsRequired().HasMaxLength(256);
            this.Property(p => p.Address2).IsOptional().HasMaxLength(256);
            this.Property(p => p.Suburb).IsRequired().HasMaxLength(64);
            this.Property(p => p.State).IsRequired().HasMaxLength(32);
            this.Property(p => p.Postcode).IsRequired().HasMaxLength(4);
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("Venue");
            this.Property(p => p.VenueId).HasColumnName("VenueId");
            this.Property(p => p.Address1).HasColumnName("Address1");
            this.Property(p => p.Address2).HasColumnName("Address2");
            this.Property(p => p.Suburb).HasColumnName("Suburb");
            this.Property(p => p.State).HasColumnName("State");
            this.Property(p => p.Postcode).HasColumnName("Postcode");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
        }
    }
}
