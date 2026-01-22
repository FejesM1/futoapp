using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using futoapp.Models;
using futoapp.View;
using System.IO;
using futoapp.Controllers;

namespace futoapp
{
    internal class Program
    {
        

        #region Main
        static void Main(string[] args)
        {
            string utvonal = "futasok.txt";

            // Ellenőrzés
            if (!File.Exists(utvonal))
            {
                
                File.Create(utvonal).Close();
                
            }
            else
            {
                Console.WriteLine("Már létezik.");
            }
            Futas.Beolvasas();
            Rendezes.ApplyTheme(); //színek biztosítása
            Rendezes.Fomenu();
        }
        #endregion
       
        

        
        

            Rendezes.CurrentTitle();
            Rendezes.WriteCentered("*** Futó App ***");

            Console.ForegroundColor = Rendezes.activeForeground;


            void WriteOption(string text, int index)
            {
                if (cPoint == index) Rendezes.OptionColor();
                else Console.ForegroundColor = Rendezes.activeForeground;
                Rendezes.WriteCentered(text);
            }

            WriteOption("Egyéni adatok", 0);
            WriteOption("Edzési adatok", 1);


            if (cPoint == 2) Console.ForegroundColor = Rendezes.altTitle;
            else Console.ForegroundColor = Rendezes.activeForeground;
            Rendezes.WriteCentered("Beállítások");

            if (cPoint == 3) Console.ForegroundColor = Rendezes.activeBack;
            else Console.ForegroundColor = Rendezes.activeForeground;
            Rendezes.WriteCentered("Kilépés");

        }

        public static void ElozmenyekMegtekintese()
        {
            Console.Clear();
            Rendezes.ApplyTheme();
            Rendezes.CurrentTitle();
            Rendezes.WriteCentered("*** TESTTÖMEG ÉS PULZUS VÁLTOZÁSA ***\n");
            Console.ForegroundColor = Rendezes.activeForeground;

            if (!File.Exists("szemelyek.txt"))
            {
                Rendezes.WriteCentered("Még nincsenek rögzített adatok.");
                return;
            }

            string[] sorok = File.ReadAllLines("szemelyek.txt");

            // Fejléc
            Rendezes.WriteCentered("Dátum       | Súly (kg) | Pulzus (bpm)");
            Rendezes.WriteCentered("--------------------------------------");

            foreach (string sor in sorok)
            {
                if (!string.IsNullOrWhiteSpace(sor))
                {
                    try
                    {
                        Szemely sz = new Szemely(sor);
                        // Formázott kiírás
                        Rendezes.WriteCentered($"{sz.RogzitesIdeje:yyyy-MM-dd}  | {sz.Testtomeg,-9} | {sz.NyugalmiPulzus}");
                    }
                    catch
                    {
                        // Ha véletlenül rossz sor van, átugorjuk
                    }
                }
            }
            Console.WriteLine();
            Rendezes.WriteCentered("Nyomj Entert a visszalépéshez...");
            Console.ReadLine();
        }
        public static void SzemelyAdatokMegjelenitese()
        {
            try
            {
                string[] sorok = File.ReadAllLines("szemelyek.txt");
                if (sorok.Length > 0)
                {
                    // FONTOS: Az utolsó sort vesszük ki, mert az a legfrissebb állapot!
                    Szemely szemely = new Szemely(sorok[sorok.Length - 1]);

                    Rendezes.WriteCentered($"--- Jelenlegi állapot ({szemely.RogzitesIdeje:yyyy-MM-dd}) ---");
                    Rendezes.WriteCentered($"Magasság: {szemely.Magassag} cm");
                    Rendezes.WriteCentered($"Testtömeg: {szemely.Testtomeg} kg");
                    Rendezes.WriteCentered($"Nyugalmi pulzus: {szemely.NyugalmiPulzus} bpm");
                    Rendezes.WriteCentered($"Cél idő: {szemely.Cel:hh\\:mm\\:ss}");
                    Rendezes.WriteCentered("-----------------------------");
                }
                else
                {
                    Rendezes.WriteCentered("Nincsenek személyes adatok.");
                }
            }
            catch (Exception)
            {
                Rendezes.WriteCentered("Hiba az adatok olvasásakor vagy nincsenek adatok.");
            }
        }

        #region EgyeniAdatFelvitel
        public static void Egyeniadatfelvitel()
        {
        public static void Edzesiadatfelvitel()
        {
            Rendezes.ApplyTheme();
            Rendezes.CurrentTitle();
            Rendezes.WriteCentered("*** ÚJ EDZÉSI ADAT FELVITELE ***\n");
            Console.ForegroundColor = Rendezes.activeForeground;
            Futas ujEdzes = null;
            try { 
            Rendezes.WriteCenteredText("Dátum (éé-hh-nn): ");
            DateTime datum = DateTime.Parse(Console.ReadLine());
            Rendezes.WriteCenteredText("Távolság (km): ");
            int tavolsag = int.Parse(Console.ReadLine());
            Rendezes.WriteCenteredText("5km lefutási ideje (példa: óó:pp:mm): ");
            TimeSpan ido = TimeSpan.Parse(Console.ReadLine());
            Rendezes.WriteCenteredText("Maximális pulzus edzés során (példa: 180/perc): ");
            string maxpulz = Console.ReadLine();


                ujEdzes = new Futas(datum, tavolsag, ido, maxpulz);
            }
            catch (Exception ex)
            {
                Rendezes.WriteCentered($"\nHibás adatbevitel! Hiba: {ex.Message}");
                return;
            }
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
            Rendezes.CurrentTitle();
            Rendezes.WriteCentered("*** EGYÉNI ADAT FELVITELE/SZERKESZTÉSE ***\n");
        
                Rendezes.WriteCentered("Sikeres mentés!");
       
            Rendezes.ApplyTheme(); // Színek biztosítása
                        return;
                }

            } while (true);
        }

        static void AlShowMenu(int belcPoint)
        {
                Rendezes.WriteCenteredText("Magasság(cm): ");
                int magassag = int.Parse(Console.ReadLine());
                Rendezes.WriteCenteredText("Testtömeg(kg): ");
        public static void FutasMenu()
        {
                        Rendezes.activeTitle = ConsoleColor.Yellow;
                        Rendezes.activeBack = ConsoleColor.Red;
                        break;

                    case 10: Rendezes.activeBackground = ConsoleColor.DarkYellow;
                        Rendezes.activeForeground = ConsoleColor.Blue;
                        Rendezes.altTitle = ConsoleColor.Magenta;
                        Rendezes.activeTitle = ConsoleColor.Cyan;
                        Rendezes.activeBack = ConsoleColor.Red;
                        break;

                    case 11:

                        belcPoint = 0;
                        return;
                }

                // Azonnali alkalmazás, hogy látszódjon az eredmény
                Rendezes.ApplyTheme();

                Console.Clear();

            } while (true);
        }

        static void AlShowMenu2(int belcPoint)
        {
            Rendezes.ApplyTheme();
            Console.Clear();
            Rendezes.CurrentaltTitle();
            Rendezes.WriteCentered("--- Téma ---");
            Console.ForegroundColor = Rendezes.activeForeground;

            // Segédfüggvény a lista kirajzoláshoz
            void WriteThemeOption(string text, int index)
            {
                if (belcPoint == index)
                {
                    Rendezes.OptionColor();
                }
                else Console.ForegroundColor = Rendezes.activeForeground;
        static void ListaMegjelenites()
        {
            // Listázás sorszámmal
            

            Rendezes.WriteCentered("Sorszám | Dátum      | Táv  | Idő      | Pulzus");
            Rendezes.WriteCentered("" + new string('-', 50));

            for (int i = 0; i < Futas.Futasok.Count; i++)
            {
                var f = Futas.Futasok[i];
                Rendezes.WriteCentered($"{i + 1}. | {f.Datum:yyyy-MM-dd} | {f.Tav,-3} | {f.Idotart,-8} | {f.Maxpulz}");
            }
            Rendezes.WriteCentered("" + new string('=', 50));
            string osszIdoSzoveg = Futas.OsszesitettIdo();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Rendezes.WriteCentered($"Összes edzésidő: {osszIdoSzoveg}");
            Console.ForegroundColor = Rendezes.activeForeground;
        }

        static void FutasModositas()
        {
            Rendezes.ApplyTheme();
            Console.Clear();
            Rendezes.CurrentTitle();
            Rendezes.WriteCentered("*** FUTÁS ADATOK MÓDOSÍTÁSA / TÖRLÉSE ***");
            Console.ForegroundColor = Rendezes.activeForeground;

            // 1. Listázzuk ki, hogy lássa mit választhat
            ListaMegjelenites();

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

            for (int i = 0; i < Futas.Futasok.Count; i++)
            {
                var f = Futas.Futasok[i];
                Rendezes.WriteCentered($"{i + 1}. | {f.Datum:yyyy-MM-dd} | {f.Tav,-3} | {f.Idotart,-8} | {f.Maxpulz}");
            }
            Rendezes.WriteCentered("" + new string('=', 50));
            string osszIdoSzoveg = Futas.OsszesitettIdo();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Rendezes.WriteCentered($"Összes edzésidő: {osszIdoSzoveg}");
            Console.ForegroundColor = Rendezes.activeForeground;
        }

        static void FutasModositas()
        {
            Rendezes.ApplyTheme();
            Console.Clear();
            Rendezes.CurrentTitle();
            Rendezes.WriteCentered("*** FUTÁS ADATOK MÓDOSÍTÁSA / TÖRLÉSE ***");
            Console.ForegroundColor = Rendezes.activeForeground;

            // 1. Listázzuk ki, hogy lássa mit választhat
            ListaMegjelenites();

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

        
    }
}