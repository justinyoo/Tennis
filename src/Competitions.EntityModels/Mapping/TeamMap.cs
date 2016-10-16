using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="Team"/> class.
    /// </summary>
    public class TeamMap : EntityTypeConfiguration<Team>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TeamMap"/> class.
        /// </summary>
        public TeamMap()
        {
            // Primary Key
            this.HasKey(p => p.TeamId);

            // Properties
            this.Property(p => p.TeamId).IsRequired();
            this.Property(p => p.ClubId).IsRequired();
            this.Property(p => p.Name).IsRequired().HasMaxLength(128);
            this.Property(p => p.Tag).IsOptional().HasMaxLength(16);
            this.Property(p => p.TrolsTeamCode).IsOptional().HasMaxLength(32);
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("Team");
            this.Property(p => p.TeamId).HasColumnName("TeamId");
            this.Property(p => p.ClubId).HasColumnName("ClubId");
            this.Property(p => p.Name).HasColumnName("Name");
            this.Property(p => p.Tag).HasColumnName("Tag");
            this.Property(p => p.TrolsTeamCode).HasColumnName("TrolsTeamCode");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.Club)
                .WithMany(p => p.Teams)
                .HasForeignKey(p => p.ClubId);
        }
    }
}