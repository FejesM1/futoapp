using futoapp.Models;
using futoapp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace futoapp.Controllers
{
    internal class Kontroller
    {
        #region beallitasok
        public static void Beallitasok()
        {
            int belcPoint = 0;
            do
            {
                bool selected = false;
                do
                {
                    Program.AlShowMenu(belcPoint);
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Enter:
                            selected = true;
                            break;
                        case ConsoleKey.UpArrow:
                            if (belcPoint > 0) belcPoint -= 1;
                            break;
                        case ConsoleKey.DownArrow:
                            if (belcPoint < 2) belcPoint += 1;
                            break;
                    }
                } while (!selected);

                switch (belcPoint)
                {
                    case 0:  //Téma
                        Console.Clear();
                        Program.Tema();
                        break;

                    case 1:

                        return;
                }

            } while (true);
        }
        #endregion

        #region egyeniadatfelvitel
        public static void Egyeniadatfelvitel()
        {
            Rendezes.ApplyTheme(); // Színek biztosítása
            Rendezes.CurrentTitle();
            Rendezes.WriteCentered("*** EGYÉNI ADAT FELVITELE/SZERKESZTÉSE ***\n");

            Program.SzemelyAdatokMegjelenitese();
            Console.ForegroundColor = Rendezes.activeForeground;
            Rendezes.WriteCenteredText("Magasság(cm): ");
            int magassag = int.Parse(Console.ReadLine());
            Rendezes.WriteCenteredText("Testtömeg(kg): ");
            int testtomeg = int.Parse(Console.ReadLine());
            Rendezes.WriteCenteredText("Nyugalmi pulzus (csak a szám, pl: 60): ");
            int nyugpulz = int.Parse(Console.ReadLine());
            Rendezes.WriteCenteredText("Kitűzött lefutás idejére cél (óó:pp:mm): ");
            DateTime celido = DateTime.Parse(Console.ReadLine());
            Szemely ujSzemely = new Szemely(magassag, testtomeg, nyugpulz, celido);

            try
            {
                ujSzemely.KiirTxt("szemelyek.txt");
                Rendezes.WriteCentered("Sikeres mentés!");
            }
            catch (Exception ex)
            {
                Rendezes.WriteCentered("\nHiba történt a mentéskor: " + ex.Message);
            }
        }
        #endregion

        #region edzesiadatfelvitel
        public static void Edzesiadatfelvitel()
        {
            Rendezes.ApplyTheme();
            Rendezes.CurrentTitle();
            Rendezes.WriteCentered("*** ÚJ EDZÉSI ADAT FELVITELE ***\n");
            Console.ForegroundColor = Rendezes.activeForeground;
            Rendezes.WriteCenteredText("Dátum (éé-hh-nn): ");
            DateTime datum = DateTime.Parse(Console.ReadLine());
            Rendezes.WriteCenteredText("Távolság (km): ");
            int tavolsag = int.Parse(Console.ReadLine());
            Rendezes.WriteCenteredText("5km lefutási ideje (példa: óó:pp:mm): ");
            TimeSpan ido = TimeSpan.Parse(Console.ReadLine());
            Rendezes.WriteCenteredText("Maximális pulzus edzés során (példa: 180/perc): ");
            string maxpulz = Console.ReadLine();

            Futas ujEdzes = new Futas(datum, tavolsag, ido, maxpulz);

            try
            {
                Futas.Hozzaad(ujEdzes);
                Rendezes.WriteCentered("Sikeres mentés!");
            }
            catch (Exception ex)
            {
                Rendezes.WriteCentered("\nHiba történt a mentéskor: " + ex.Message);
            }

        }
        #endregion

        #region futasmodositas
        public static void FutasModositas()
        {
            Rendezes.ApplyTheme();
            Console.Clear();
            Rendezes.CurrentTitle();
            Rendezes.WriteCentered("*** FUTÁS ADATOK MÓDOSÍTÁSA / TÖRLÉSE ***");
            Console.ForegroundColor = Rendezes.activeForeground;

            // 1. Listázzuk ki, hogy lássa mit választhat
            Program.ListaMegjelenites();

            if (Futas.Futasok.Count == 0)
            {
                Console.ReadLine();
                return;
            }

            Console.WriteLine("\n\tMit szeretnél tenni?");
            Console.WriteLine("\t1. Törlés");
            Console.WriteLine("\t2. Módosítás");
            Console.WriteLine("\t3. Mégse");
            Console.Write("\tVálasztás: ");
            string opcio = Console.ReadLine();

            if (opcio == "1") // TÖRLÉS
            {
                Console.Write("\n\tAdd meg a törölni kívánt sorszámot: ");
                if (int.TryParse(Console.ReadLine(), out int sorszam) && sorszam > 0 && sorszam <= Futas.Futasok.Count)
                {
                    // A felhasználó 1-től számol, a lista 0-tól, ezért kivonunk 1-et
                    Futas.Torles(sorszam - 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Rendezes.WriteCentered("Sikeres törlés!");
                    Console.ReadLine();
                }
                else
                {
                    Rendezes.WriteCentered("Érvénytelen sorszám!");
                    Console.ReadLine();
                }
            }
            else if (opcio == "2") // MÓDOSÍTÁS
            {
                Console.Write("\n\tAdd meg a módosítani kívánt sorszámot: ");
                if (int.TryParse(Console.ReadLine(), out int sorszam) && sorszam > 0 && sorszam <= Futas.Futasok.Count)
                {
                    // Kiválasztott elem referenciája
                    Futas kivalasztott = Futas.Futasok[sorszam - 1];

                    Console.WriteLine("\n\t(Hagyj üresen egy mezőt, ha nem akarod módosítani)");

                    // Dátum
                    Console.Write($"\tÚj dátum (Jelenlegi: {kivalasztott.Datum:yyyy-MM-dd}): ");
                    string ujDatumStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(ujDatumStr)) kivalasztott.Datum = DateTime.Parse(ujDatumStr);

                    // Táv
                    Console.Write($"\tÚj táv (Jelenlegi: {kivalasztott.Tav}): ");
                    string ujTavStr = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(ujTavStr)) kivalasztott.Tav = int.Parse(ujTavStr);

                    // Idő
                    Console.Write($"\tÚj idő (Jelenlegi: {kivalasztott.Idotart}): ");
                    string ujIdo = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(ujIdo)) kivalasztott.Idotart = TimeSpan.Parse(ujIdo);

                    // Pulzus
                    Console.Write($"\tÚj pulzus (Jelenlegi: {kivalasztott.Maxpulz}): ");
                    string ujPulzus = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(ujPulzus)) kivalasztott.Maxpulz = ujPulzus;

                    // MENTÉS
                    Futas.Mentes();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Rendezes.WriteCentered("Sikeres módosítás!");
                    Console.ReadLine();
                }
                else
                {
                    Rendezes.WriteCentered("Érvénytelen sorszám!");
                    Console.ReadLine();
                }
            }
        }
        #endregion
    }
}
