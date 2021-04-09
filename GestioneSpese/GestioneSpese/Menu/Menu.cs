using GestioneSpese.Repository.EF.Repository;
using GestioneSpese.Repository.Entities.Entities;
using GestioneSpese.Repository.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestioneSpese.Menu
{
    public static class Menu
    {
        public static void MostraMenu()
        {
            //uso il repository con EF
            IRepositorySpesa repoSpesa = new RepositorySpesaEF();
            IRepositoryCategoria repoCategoria = new RepositoryCategoriaEF();

            //Creo le categoria
            List<CategoriaClass> Categorie = CategoriaClass.CreaCategorie();

            //le inserisco nel db
            foreach (var categoria in Categorie)
            {
                repoCategoria.Inserisci(categoria);
            }
 
            bool esci = true;

            while(esci != false)
            {
                Console.WriteLine("----Menu----");
                Console.WriteLine("Scegli una della seguenti opzioni:");
                Console.WriteLine("   a) Inserisci una nuova Spesa,");
                Console.WriteLine("   b) Elimina una Spesa esistente,");
                Console.WriteLine("   c) Approva una Spesa esistente,");
                Console.WriteLine("   d) Mostra tutte le Spese approvate,");
                Console.WriteLine("   e) Mostra le Spese di un determinato utente,");
                Console.WriteLine("   f) Mostra l'importo Totale per ogni Categoria di Spese,");
                Console.WriteLine("   q) Per Uscire.");
                Console.WriteLine();

                char key = 'x';
                bool ok = Char.TryParse(Console.ReadLine(), out key);
                while (ok != true && (key == 'a' || key == 'b' || key == 'c' || key == 'd' || key == 'e' || key == 'f' || key == 'q'))
                {
                    Console.WriteLine("Inserisci una lettera valida");
                    ok = Char.TryParse(Console.ReadLine(), out key);
                }

                if (key == 'q')
                {
                    Environment.Exit(0);
                }

                switch (key)
                {
                    case 'a':
                        Console.WriteLine();
                        Console.WriteLine("--Inserisci una nuova Spesa--");
                        Console.WriteLine("Inserisci L'id della categoria:");
                        int idCat = 0;
                        bool ok1 = Int32.TryParse(Console.ReadLine(), out idCat);
                        while (ok1 != true)
                        {
                            Console.WriteLine("Inserisci un id valido:");
                            ok1 = Int32.TryParse(Console.ReadLine(), out idCat);
                        }
                        Console.WriteLine("Inserisci la descrizione delle spese:");
                        string desc = Console.ReadLine();
                        Console.WriteLine("Inserisci il nome dell'utente che ha effetuato le spese:");
                        string utente = Console.ReadLine();
                        Console.WriteLine("Inserisci l'importo delle spese:");
                        decimal imp = 0;
                        bool ok2 = Decimal.TryParse(Console.ReadLine(), out imp);
                        while (ok2 != true)
                        {
                            Console.WriteLine("Inserisci un id valido:");
                            ok2 = Decimal.TryParse(Console.ReadLine(), out imp);
                        }
                        Console.WriteLine();

                        Spesa s = new Spesa(idCat, desc, utente, imp);
                        bool successo = repoSpesa.Inserisci(s);
                        if (successo == true)
                        {
                            Console.WriteLine("Spesa aggiunta con successo!");
                        }
                        else
                        {
                            Console.WriteLine("Errore, la spesa non è stata aggiunta");
                        }
                        break;
                    case 'b':
                        Console.WriteLine();
                        Console.WriteLine("--Elimina una Spesa--");
                        Console.WriteLine("Inserisci l'ID della spesa che vuoi eliminare:");
                        int id = 0;
                        bool ok3 = Int32.TryParse(Console.ReadLine(), out id);
                        while (ok3 != true)
                        {
                            Console.WriteLine("Inserisci un id valido:");
                            ok3 = Int32.TryParse(Console.ReadLine(), out id);
                        }
                        Console.WriteLine();

                        bool suc = repoSpesa.Cancella(id);
                        if(suc == true)
                        {
                            Console.WriteLine("Spesa eliminata");
                        }
                        else
                        {
                            Console.WriteLine("Errore, Spesa non eliminata");
                        }
                        break;
                    case 'c':
                        Console.WriteLine();
                        Console.WriteLine("--Approva una Spesa--");
                        Console.WriteLine("Inserisci l'ID della spesa che vuoi approvare:");
                        int id1 = 0;
                        bool ok4 = Int32.TryParse(Console.ReadLine(), out id1);
                        while (ok4 != true)
                        {
                            Console.WriteLine("Inserisci un id valido:");
                            ok4 = Int32.TryParse(Console.ReadLine(), out id1);
                        }
                        Console.WriteLine();

                        bool succ = repoSpesa.ApprovaSpesa(id1);
                        if (succ == true)
                        {
                            Console.WriteLine("Spesa approvata");
                        }
                        else
                        {
                            Console.WriteLine("Errore, Spesa non approvata");
                        }
                        break;
                    case 'd':
                        List<Spesa> speseAp = repoSpesa.SpeseApprovate();
                        if(speseAp != null)
                        {
                            foreach (var item in speseAp)
                            {
                                Console.WriteLine(item.Descrizione + " Importo: " + item.Importo + ", approvata: " + item.Approvato);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nessuna spesa approvata");
                        }
                        
                        break;
                    case 'e':
                        Console.WriteLine();
                        Console.WriteLine("--Spese di un utente--");
                        Console.WriteLine("Inserisci il nome dell'utente:");
                        string ut = Console.ReadLine();
                        List<Spesa> speseUt = repoSpesa.SpeseUtente(ut);
                        if(speseUt != null)
                        {
                            foreach (var item in speseUt)
                            {
                                Console.WriteLine(item.Descrizione + " Importo: " + item.Importo + ", approvata: " + item.Approvato);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nessuna spesa con questo utente");
                        }
                        
                        Console.WriteLine();
                        break;
                    case 'f':
                        Console.WriteLine();
                        Console.WriteLine("--Importo totale per categoria--");
                        repoSpesa.SpesePerCategoria();
                        break;
                    case 'q':
                        Environment.Exit(0);
                        esci = false;
                        break;
                }
            }

            
        }
        

    }
}
