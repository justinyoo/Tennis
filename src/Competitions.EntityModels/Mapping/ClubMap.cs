using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="Club"/> class.
    /// </summary>
    public class ClubMap : EntityTypeConfiguration<Club>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ClubMap"/> class.
        /// </summary>
        public ClubMap()
        {
            // Primary Key
            this.HasKey(p => p.ClubId);

            // Properties
            this.Property(p => p.ClubId).IsRequired();
            this.Property(p => p.VenueId).IsRequired();
            this.Property(p => p.Name).IsRequired().HasMaxLength(256);
            this.Property(p => p.Manager).IsOptional().HasMaxLength(128);
            this.Property(p => p.Phone).IsOptional().HasMaxLength(16);
            this.Property(p => p.Mobile).IsOptional().HasMaxLength(16);
            this.Property(p => p.Email).IsOptional().HasMaxLength(256);
            this.Property(p => p.Url).IsOptional().HasMaxLength(512);
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("Club");
            this.Property(p => p.ClubId).HasColumnName("ClubId");
            this.Property(p => p.VenueId).HasColumnName("VenueId");
            this.Property(p => p.Name).HasColumnName("Name");
            this.Property(p => p.Manager).HasColumnName("Manager");
            this.Property(p => p.Phone).HasColumnName("Phone");
            this.Property(p => p.Mobile).HasColumnName("Mobile");
            this.Property(p => p.Email).HasColumnName("Email");
            this.Property(p => p.Url).HasColumnName("Url");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.Venue)
                .WithRequiredPrincipal(p => p.Club);
        }
    }
}
