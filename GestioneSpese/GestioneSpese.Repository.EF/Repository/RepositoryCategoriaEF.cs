using GestioneSpese.Repository.EF.Context;
using GestioneSpese.Repository.Entities.Entities;
using GestioneSpese.Repository.Entities.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestioneSpese.Repository.EF.Repository
{
    public class RepositoryCategoriaEF : IRepositoryCategoria
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
                var cat = ctx.Categorie.Find(id);

                if (cat != null)
                {
                    ctx.Categorie.Remove(cat);
                    ctx.SaveChanges();

                    return true;
                }
                return false;
            }
        }

        public bool Inserisci(CategoriaClass item)
        {
            using (var ctx = new GestioneSpeseContext())
            {
                //controlli
                if (item == null)
                {
                    return false;
                }

                //devo inserire la nuova categoria
                ctx.Categorie.Add(item);
                ctx.SaveChanges();

                return true;
            }
        }
    }
}
