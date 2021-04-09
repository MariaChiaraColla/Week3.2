using GestioneSpese.Repository.EF.Repository;
using GestioneSpese.Repository.Entities.Entities;
using GestioneSpese.Repository.Entities.Repository;
using System;
using System.Collections.Generic;

namespace GestioneSpese
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gestione delle spese!");
            Console.WriteLine();

            //uso il repository con EF
            IRepositorySpesa repoSpesa = new RepositorySpesaEF();
            IRepositoryCategoria repoCategoria = new RepositoryCategoriaEF();

            CategoriaClass c = new CategoriaClass("Amministrazione");
            repoCategoria.Inserisci(c);

            Spesa s = new Spesa(c.ID, "Spese amministrative", "Maria Rossi", 150.50m);

            bool i = repoSpesa.Inserisci(s);
            Console.WriteLine("Inserisci spesa: " + i);
            Console.WriteLine();
            bool r = repoSpesa.ApprovaSpesa(s);
            Console.WriteLine("Approva spesa: " + r);
            Console.WriteLine();

            List<Spesa> SA = repoSpesa.SpeseApprovate();
            Console.WriteLine("Spese Approvate:");
            foreach (var item in SA)
            {
                Console.WriteLine(item.Descrizione);
            }
            Console.WriteLine();

            //Console.WriteLine("Spese Categoria:");
            //List<Spesa> SC = repoSpesa.SpesePerCategoria();
            //foreach (var item in SC)
            //{
            //    Console.WriteLine(item.Descrizione);
            //}

            Console.WriteLine();
            Console.WriteLine("Spese Utente:");
            List<Spesa> SU = repoSpesa.SpeseUtente("Maria Rossi");
            foreach (var item in SU)
            {
                Console.WriteLine(item.Descrizione);
            }
        }
    }
}
