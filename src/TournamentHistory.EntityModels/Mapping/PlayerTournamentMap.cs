using System.Data.Entity.ModelConfiguration;

namespace TournamentHistory.EntityModels.Mapping
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="PlayerTournament"/> class.
    /// </summary>
    public class PlayerTournamentMap : EntityTypeConfiguration<PlayerTournament>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlayerTournament"/> class.
        /// </summary>
        public PlayerTournamentMap()
        {
            // Primary Key
            this.HasKey(p => p.PlayerTournamentId);

            // Properties
            this.Property(p => p.PlayerTournamentId).IsRequired();
            this.Property(p => p.PlayerId).IsRequired();
            this.Property(p => p.TournamentId).IsRequired();
            this.Property(p => p.PlayerNumber).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("PlayerTournament");
            this.Property(p => p.PlayerTournamentId).HasColumnName("PlayerTournamentId");
            this.Property(p => p.PlayerId).HasColumnName("PlayerId");
            this.Property(p => p.TournamentId).HasColumnName("TournamentId");
            this.Property(p => p.PlayerNumber).HasColumnName("PlayerNumber");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
            this.HasRequired(p => p.Player)
                .WithMany(p => p.PlayerTournaments)
                .HasForeignKey(p => p.PlayerId);

            this.HasRequired(p => p.Tournament)
                .WithMany(p => p.PlayerTournaments)
                .HasForeignKey(p => p.TournamentId);
        }
    }
}