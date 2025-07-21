using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VotacionObjetos.Models;

namespace VotacionDAL
{
    public class VotacionContext : DbContext
    {
        public VotacionContext(DbContextOptions<VotacionContext> options)
            : base(options)
        {
        }

        public DbSet<Votante> Votantes { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Voto> Votos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Votante>()
                .HasOne(v => v.Voto)
                .WithOne(vt => vt.Votante)
                .HasForeignKey<Voto>(vt => vt.CedulaVotante)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Partido>()
                .HasMany(p => p.Votos)
                .WithOne(v => v.Partido)
                .HasForeignKey(v => v.PartidoId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }

}
