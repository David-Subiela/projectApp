using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;

namespace Clase5_proyecto.Models
{
    public class PlayContext : DbContext
    {
        // CONTRUCTOR PREDETERMINADO CONTEXTO
        public PlayContext(DbContextOptions<PlayContext> options) : base(options)
        {

        }

        // INDICACIÓN DE LOS MODELOS
        public DbSet<Aeronave> Aeronaves { get; set; }
        public DbSet<Piloto> Pilotos { get; set; }
        public DbSet<MisionEmergencia> MisionesEmergencia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MisionEmergencia>()
                .HasOne(a => a.Aeronave) // Una misión tiene una aeronave
                .WithMany(me => me.MisionesEmergencia) // Una aeronave puede tener muchas misiones
                .HasForeignKey(m => m.AeronaveId); // Clave foránea

            modelBuilder.Entity<MisionEmergencia>()
               .HasOne(p => p.Piloto) // Una misión tiene un piloto
               .WithMany(me => me.MisionesEmergencia) // Un piloto puede tener muchas misiones
               .HasForeignKey(m => m.PilotoId); // Clave foránea

            base.OnModelCreating(modelBuilder);
        }


    }
}
