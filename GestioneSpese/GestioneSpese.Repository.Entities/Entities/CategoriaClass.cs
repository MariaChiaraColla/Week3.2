using GestioneSpese.Repository.Entities.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestioneSpese.Repository.Entities.Entities
{
    public class CategoriaClass : IEntity
    {
        //proprietà
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(100)]
        public string Categoria { get; set; }
        //per EF, qui dove ho la relazione N (di 1-N) devo salvare una collezione di dati del tipo della classe
        //esterna
        public ICollection<Spesa> Spese { get; set; } = new List<Spesa>();

        //costruttori
        public CategoriaClass() { }
        public CategoriaClass(string categoria)
        {
            Categoria = categoria;
        }

        public static List<CategoriaClass> CreaCategorie()
        {
            List<CategoriaClass> Categorie = new List<CategoriaClass>
            {
                new CategoriaClass("Amministrazione"),
                new CategoriaClass("Officina"),
                new CategoriaClass("Ufficio"),
            };
            return Categorie;
        }
    }
}
