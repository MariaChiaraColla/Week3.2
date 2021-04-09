using GestioneSpese.Repository.EF.Context;
using GestioneSpese.Repository.Entities.Entities;
using GestioneSpese.Repository.Entities.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestioneSpese.Repository.EF.Repository
{
    public class RepositorySpesaEF : IRepositorySpesa
    {
        public bool Cancella(int id)
        {
            using (var ctx = new GestioneSpeseContext())
            {
                //controlli sull'input
                if (id < 0)
                {
                    return false;
                }
                var spesa = ctx.Spese.Find(id);

                if (spesa != null)
                {
                    
                    ctx.Spese.Remove(spesa);
                    ctx.SaveChanges();

                    return true;
                }
                return false;
            }
        }

        public bool Inserisci(Spesa item)
        {
            using (var ctx = new GestioneSpeseContext())
            {
                //controlli
                if(item == null)
                {
                    return false;
                }
                //so che se devo inserire una spesa devo inserire anche la sua categoria
                var categoria = ctx.Categorie.Include(s => s.Spese)
                                             .Where(c => c.ID == item.CategoriaId)
                                             .SingleOrDefault();
                if(categoria != null)
                {
                    categoria.Spese.Add(item);
                }

                //devo inserire la nuova spesa
                ctx.Spese.Add(item);
                ctx.SaveChanges();

                return true;
            }
        }

        public bool ApprovaSpesa(int id)
        {
            using (var ctx = new GestioneSpeseContext())
            {
                var spesa = ctx.Spese.Find(id);

                bool saved = false;
                do
                {
                    //per gestire la concorrenza
                    try
                    {
                        spesa.Approvato = true;

                        //questo elemento marchialo come modificato, salva e ritorna true
                        ctx.Entry<Spesa>(spesa).State = EntityState.Modified;
                        ctx.SaveChanges();

                        saved = true;
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        //sto rischiando di perdere i dati, se qualcosa va male, recupero dal db i valori iniziali
                        //e li riassegno a tutte le entità modificate
                        foreach (var entity in ex.Entries)
                        {
                            var dbValue = entity.GetDatabaseValues();
                            entity.OriginalValues.SetValues(dbValue);
                        }
                    }

                } while (!saved);

                return true;
            }

        }

        public List<Spesa> SpeseApprovate()
        {
            using (var ctx = new GestioneSpeseContext())
            {
                List<Spesa> speseApprovate = null;

                speseApprovate = ctx.Spese.Where(s => s.Approvato == true)
                                          .ToList();

                return speseApprovate;
            }
        }

        public List<Spesa> SpeseUtente(string utente)
        {
            using (var ctx = new GestioneSpeseContext())
            {
                List<Spesa> speseUtente = null;

                if( utente != null)
                {
                    speseUtente = ctx.Spese.Where(s => s.Utente == utente)
                                       .ToList();
                }

                return speseUtente;
            }
        }

        public void SpesePerCategoria()
        {
            using (var ctx = new GestioneSpeseContext())
            {
                var spese = ctx.Spese.GroupBy(s => s.CategoriaId)
                                     .Select(SpeseCategoria =>
                                             new
                                             {
                                                 Categoria = SpeseCategoria.Key,
                                                 TotaleImporti = SpeseCategoria.Sum(i => i.Importo)
                                             })
                                     .ToList();
                foreach (var s in spese)
                {
                    Console.WriteLine(s.Categoria + ") " + s.TotaleImporti + " euro");
                }

            }
        }
    }
}
