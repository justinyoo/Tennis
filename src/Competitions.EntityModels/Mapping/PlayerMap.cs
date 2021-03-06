﻿using System.Data.Entity.ModelConfiguration;

namespace Competitions.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="Player"/> class.
    /// </summary>
    public class PlayerMap : EntityTypeConfiguration<Player>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlayerMap"/> class.
        /// </summary>
        public PlayerMap()
        {
            // Primary Key
            this.HasKey(p => p.PlayerId);

            // Properties
            this.Property(p => p.PlayerId).IsRequired();
            this.Property(p => p.FirstName).IsRequired().HasMaxLength(128);
            this.Property(p => p.LastName).IsRequired().HasMaxLength(64);
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("Player");
            this.Property(p => p.PlayerId).HasColumnName("PlayerId");
            this.Property(p => p.FirstName).HasColumnName("FirstName");
            this.Property(p => p.LastName).HasColumnName("LastName");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
        }
    }
}
