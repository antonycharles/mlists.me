using Me.Mlists.Data;
using Me.Mlists.Models;
using Me.Mlists.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Me.Mlists.Data.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Lista> Listas { get; set; }
        public DbSet<ListaSecao> ListaSecoes { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Convidado> Convidados { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lista>(new ListaMap().Configure);
            modelBuilder.Entity<ListaSecao>(new ListaSecaoMap().Configure);
            modelBuilder.Entity<Monster>(new MonsterMap().Configure);
            modelBuilder.Entity<Participante>(new ParticipanteMap().Configure);
            modelBuilder.Entity<Tarefa>(new TarefaMap().Configure);
            modelBuilder.Entity<Convidado>(new ConvidadoMap().Configure);
        }
    }
}
