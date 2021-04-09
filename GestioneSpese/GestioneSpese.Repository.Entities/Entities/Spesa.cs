using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestioneSpese.Repository.Entities.Entities
{
    public class Spesa : IEntity
    {
        //proprietà
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto-incrementale
        public int ID { get; set; }
        public DateTime Data { get; set; }
        public int CategoriaId { get; set; }
        [MaxLength(500)] //lunghezza massima
        public string Descrizione { get; set; }
        [MaxLength(100)] //lunghezza massima
        public string Utente { get; set; }
        public decimal Importo { get; set; }
        public bool Approvato { get; set; }
        //per EF, qui dove ho la relazione 1 (di 1-N) devo definire due campi, 1 per salvare solo l'id (CategoriaId)
        //e uno del tipo della classe esterna per salvare tutti i dati dell'oggetto a cui si riferisce CategoriaId
        public CategoriaClass Categoria { get; set; }

        //costruttore
        public Spesa() { }

        public Spesa(int categoriaId, string descrizione, string utente, decimal importo)
        {
            Data = DateTime.Now;
            CategoriaId = categoriaId;
            Descrizione = descrizione;
            Utente = utente;
            Importo = importo;
            Approvato = false;
        }
    }
}
