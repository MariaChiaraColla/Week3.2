using GestioneSpese.Repository.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestioneSpese.Repository.Entities.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        //metodi comuni
        bool Inserisci(T item);
        bool Cancella(int id);
        
    }
}
