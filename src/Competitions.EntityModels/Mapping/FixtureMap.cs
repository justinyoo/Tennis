﻿using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="Fixture"/> class.
    /// </summary>
    public class FixtureMap : EntityTypeConfiguration<Fixture>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="FixtureMap"/> class.
        /// </summary>
        public FixtureMap()
        {
            // Primary Key
            this.HasKey(p => p.FixtureId);

            // Properties
            this.Property(p => p.FixtureId).IsRequired();
            this.Property(p => p.CompetitionId).IsRequired();
            this.Property(p => p.VenueId).IsRequired();
            this.Property(p => p.Week).IsRequired();
            this.Property(p => p.DateScheduled).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("Fixture");
            this.Property(p => p.FixtureId).HasColumnName("FixtureId");
            this.Property(p => p.CompetitionId).HasColumnName("CompetitionId");
            this.Property(p => p.VenueId).HasColumnName("VenueId");
            this.Property(p => p.Week).HasColumnName("Week");
            this.Property(p => p.DateScheduled).HasColumnName("DateScheduled");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.Competition)
                .WithMany(p => p.Fixtures)
                .HasForeignKey(p => p.CompetitionId);

            this.HasRequired(p => p.Venue)
                .WithMany(p => p.Fixtures)
                .HasForeignKey(p => p.VenueId)
                .WillCascadeOnDelete(false);
        }
    }
}