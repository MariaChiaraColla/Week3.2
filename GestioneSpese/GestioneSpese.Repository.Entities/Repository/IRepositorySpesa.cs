using GestioneSpese.Repository.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestioneSpese.Repository.Entities.Repository
{
    public interface IRepositorySpesa : IRepository<Spesa>
    {
        //metodi propri della classe
        bool ApprovaSpesa(int id);
        List<Spesa> SpeseApprovate();
        List<Spesa> SpeseUtente(string utente);
        void SpesePerCategoria();
    }
}
