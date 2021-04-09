using GestioneSpese.Repository.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestioneSpese.Repository.EF.Context
{
    public class GestioneSpeseContext : DbContext
    {
        public GestioneSpeseContext() : base() { }
        public GestioneSpeseContext(DbContextOptions<GestioneSpeseContext> options) : base(options) { }

        //dichiaro tutte le tabelle che voglio nel mio db
        public DbSet<Spesa> Spese { get; set; }
        public DbSet<CategoriaClass> Categorie { get; set; }

        //configurazione con il db
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //configurazione del db, con la stringa di connessione
            builder.UseSqlServer(@"Persist Security Info = False; Integrated Security = true; Initial Catalog = GestioneSpese; Server = .\SQLEXPRESS");
        }

        //creo le relazioni con le fluent api
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //la spesa ha 1 categoria, la quale ha più spese, ha una chiave esterna che la collega a categoria
            builder.Entity<Spesa>().HasOne(c => c.Categoria)
                                   .WithMany(s => s.Spese)
                                   .HasForeignKey(x => x.CategoriaId); //non necessario perchè uso le convenzioni

        }


    }
}
