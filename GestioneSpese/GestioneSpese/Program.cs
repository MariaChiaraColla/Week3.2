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

            Menu.Menu.MostraMenu();

        }
    }
}
