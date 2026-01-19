using futoapp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace futoapp
{
    internal class Program
    {
        static int cPoint = 0;
        static ConsoleColor activeBackground = ConsoleColor.Black;
        static ConsoleColor activeForeground = ConsoleColor.White;
        static ConsoleColor altTitle = ConsoleColor.Magenta;
        static ConsoleColor activeTitle = ConsoleColor.Yellow;
        static ConsoleColor activeBack = ConsoleColor.Red;

        static void Main(string[] args)
        {
            Futas.Beolvasas();
            ApplyTheme();
            Fomenu();
        }

        #region Téma színek és segéd metódusok


        static void ApplyTheme()
        {
            Console.BackgroundColor = activeBackground;
            Console.ForegroundColor = activeForeground;
        }
        static void CurrentTitle() { Console.ForegroundColor = activeTitle; }
        static void CurrentaltTitle() { Console.ForegroundColor = altTitle; }
        static void OptionColor()
        {
            if (activeBackground == ConsoleColor.DarkGreen) Console.ForegroundColor = ConsoleColor.Blue;
            else Console.ForegroundColor = ConsoleColor.Green;
        }
        static void WriteCentered(string text)
        {
            string StripAnsi(string Rtext) { return System.Text.RegularExpressions.Regex.Replace(Rtext, @"\u001b\[[0-9;]*m", ""); }
            string temptext = StripAnsi(text);
            int width = Console.WindowWidth;
            int leftPadding = (width - temptext.Length) / 2;
            if (leftPadding < 0) leftPadding = 0;
            Console.WriteLine(new string(' ', leftPadding) + text);
        }
        static void WriteCenteredText(string text)
        {
            string StripAnsi(string Rtext) { return System.Text.RegularExpressions.Regex.Replace(Rtext, @"\u001b\[[0-9;]*m", ""); }
            string temptext = StripAnsi(text);
            int width = Console.WindowWidth;
            int leftPadding = (width - temptext.Length) / 2 - 2;
            if (leftPadding < 0) leftPadding = 0;
            Console.Write(new string(' ', leftPadding) + text);
        }
        #endregion
        static void Fomenu()
        {
            cPoint = 0;
            do
            {
                bool selected = false;
                do
                {
                    ShowMenu1(cPoint);
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Enter: selected = true; break;
                        case ConsoleKey.UpArrow: if (cPoint > 0) cPoint -= 1; break;
                        case ConsoleKey.DownArrow: if (cPoint < 5) cPoint += 1; break;
                    }
                } while (!selected);

                switch (cPoint)
                {
                    case 0: Console.Clear(); Egyeniadatfelvitel(); WriteCentered("Enterre tovább..."); Console.ReadLine(); break;
                    case 1: Console.Clear(); FutasMenu(); break;
                    case 2: Console.Clear(); WriteCentered("Enterre tovább..."); Console.ReadLine(); break;

                    case 3:
                        Console.Clear();
                        ListaMegjelenites();
                        WriteCentered("Enterre tovább...");
                        Console.ReadLine();
                        break;

                    case 4: Console.Clear(); Beallitasok(); break;
                    case 5:
                        Console.Clear();
                        WriteCentered("Biztosan kilép? (i/n): ");
                        if (Console.ReadKey().Key == ConsoleKey.I) return;
                        break;
                }
            } while (true);
        }

        static void ShowMenu1(int cPoint)
        {
            ApplyTheme();
            Console.Clear();
            CurrentTitle();
            WriteCentered("*** Futó App ***");
            Console.ForegroundColor = activeForeground;

            void WriteOption(string text, int index)
            {
                if (cPoint == index) OptionColor();
                else Console.ForegroundColor = activeForeground;
                WriteCentered(text);
            }

            WriteOption("Egyéni adatok", 0);
            WriteOption("Edzési adatok (Felvitel/Módosítás)", 1);
            WriteOption("Egyéni adat módosítása", 2);
            WriteOption("Edzési adatok megjelenítése", 3);

            if (cPoint == 4) Console.ForegroundColor = altTitle;
            else Console.ForegroundColor = activeForeground;
            WriteCentered("Beállítások");

            if (cPoint == 5) Console.ForegroundColor = activeBack;
            else Console.ForegroundColor = activeForeground;
            WriteCentered("Kilépés");
        }


        public static void Egyeniadatfelvitel()
        {
            ApplyTheme();
            CurrentTitle();
            WriteCentered("*** ÚJ EGYÉNI ADAT FELVITELE ***\n");

        }


        public static void Edzesiadatfelvitel()
        {
            ApplyTheme();
            CurrentTitle();
            Console.Clear();
            WriteCentered("*** ÚJ EDZÉSI ADAT FELVITELE ***\n");
            Console.ForegroundColor = activeForeground;

            try
            {
                WriteCenteredText("Dátum (éééé-hh-nn): ");
                DateTime datum = DateTime.Parse(Console.ReadLine());

                WriteCenteredText("Távolság (km): ");
                int tavolsag = int.Parse(Console.ReadLine());

                WriteCenteredText("Idő (óó:pp:mp): ");
                string ido = Console.ReadLine();

                WriteCenteredText("Max pulzus: ");
                string maxpulz = Console.ReadLine();

                Futas ujFutas = new Futas(datum, tavolsag, ido, maxpulz);

                Futas.Hozzaad(ujFutas);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                WriteCentered("Sikeres mentés!");
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteCentered("Hibás adatmegadás! Próbáld újra.");
            }
        }

        static void FutasMenu()
        {
            // Egyszerű almenü
            while (true)
            {
                ApplyTheme();
                Console.Clear();
                CurrentTitle();
                WriteCentered("*** FUTÁS ADATOK KEZELÉSE ***");
                Console.ForegroundColor = activeForeground;
                Console.WriteLine();
                WriteCentered("1. Új adat felvitele");
                WriteCentered("2. Adatok Módosítása / Törlése");
                WriteCentered("3. Vissza a főmenübe");

                Console.Write("\n\tVálasztás: ");
                string valasztas = Console.ReadLine();

                if (valasztas == "1")
                {
                    Edzesiadatfelvitel();
                    Console.ReadLine();
                }
                else if (valasztas == "2")
                {
                    FutasModositas();
                }
                else if (valasztas == "3")
                {
                    return;
                }
            }
        }

        static void ListaMegjelenites()
        {
            // Listázás sorszámmal
            if (Futas.Futasok.Count == 0)
            {
                WriteCentered("Nincs rögzített futás.");
                return;
            }

            Console.WriteLine("\n\tSorszám | Dátum      | Táv  | Idő      | Pulzus");
            Console.WriteLine("\t" + new string('-', 50));

            for (int i = 0; i < Futas.Futasok.Count; i++)
            {
                var f = Futas.Futasok[i];
                Console.WriteLine($"\t{i + 1}.      | {f.Datum:yyyy-MM-dd} | {f.Tav,-3} | {f.Idotart,-8} | {f.Maxpulz}");
            }
        }

        static void FutasModositas()
        {
            ApplyTheme();
            Console.Clear();
            CurrentTitle();
            WriteCentered("*** FUTÁS ADATOK MÓDOSÍTÁSA / TÖRLÉSE ***");
            Console.ForegroundColor = activeForeground;

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
                    WriteCentered("Sikeres törlés!");
                    Console.ReadLine();
                }
                else
                {
                    WriteCentered("Érvénytelen sorszám!");
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
                    if (!string.IsNullOrWhiteSpace(ujIdo)) kivalasztott.Idotart = ujIdo;

                    // Pulzus
                    Console.Write($"\tÚj pulzus (Jelenlegi: {kivalasztott.Maxpulz}): ");
                    string ujPulzus = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(ujPulzus)) kivalasztott.Maxpulz = ujPulzus;

                    // MENTÉS
                    Futas.Mentes();
                    Console.ForegroundColor = ConsoleColor.Green;
                    WriteCentered("Sikeres módosítás!");
                    Console.ReadLine();
                }
                else
                {
                    WriteCentered("Érvénytelen sorszám!");
                    Console.ReadLine();
                }
            }
        }

        
        public static void Beallitasok()
        {
            belcPoint = 0;
            do
            {
                bool selected = false;
                do
                {
                    AlShowMenu(belcPoint);
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Enter: selected = true; break;
                        case ConsoleKey.UpArrow: if (belcPoint > 0) belcPoint -= 1; break;
                        case ConsoleKey.DownArrow: if (belcPoint < 2) belcPoint += 1; break;
                    }
                } while (!selected);

                switch (belcPoint)
                {
                    case 0: Console.Clear(); Tema(); break;
                    case 1: Console.Clear(); WriteCentered("Enterre tovább..."); Console.ReadLine(); break;
                    case 2: return;
                }
            } while (true);
        }

        static int belcPoint = 0;
        static void AlShowMenu(int belcPoint)
        {
            ApplyTheme();
            Console.Clear();
            CurrentaltTitle();
            WriteCentered("--- Beállítások ---");
            Console.ForegroundColor = activeForeground;
            if (belcPoint == 0) OptionColor(); else Console.ForegroundColor = activeForeground;
            WriteCentered("Téma");
            if (belcPoint == 1) OptionColor(); else Console.ForegroundColor = activeForeground;
            WriteCentered("Szöveg elrendezés (kezdetleges)");
            if (belcPoint == 2) Console.ForegroundColor = activeBack; else Console.ForegroundColor = activeForeground;
            WriteCentered("Vissza");
        }

        static void Tema()
        {
            
            belcPoint = 0;
            do
            {
                bool selected = false;
                do
                {
                    AlShowMenu2(belcPoint);
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Enter: selected = true; break;
                        case ConsoleKey.UpArrow: if (belcPoint > 0) belcPoint -= 1; break;
                        case ConsoleKey.DownArrow: if (belcPoint < 11) belcPoint += 1; break;
                    }
                } while (!selected);

                switch (belcPoint)
                {
                    case 0: activeBackground = ConsoleColor.Black; activeForeground = ConsoleColor.White; altTitle = ConsoleColor.Magenta; activeTitle = ConsoleColor.Yellow; activeBack = ConsoleColor.Red; break;
                    
                    case 11: return;
                }
                ApplyTheme();
                Console.Clear();
            } while (true);
        }

        static void AlShowMenu2(int belcPoint)
        {
            ApplyTheme();
            Console.Clear();
            CurrentaltTitle();
            WriteCentered("--- Téma ---");    
            Console.ForegroundColor = activeForeground;
            void WriteThemeOption(string text, int index)
            {
                if (belcPoint == index) OptionColor();
                else Console.ForegroundColor = activeForeground;
                WriteCentered(text);
            }
            WriteThemeOption("Fekete háttér - Fehér szöveg (alap)", 0);
           
            if (belcPoint == 11) Console.ForegroundColor = activeBack; else Console.ForegroundColor = activeForeground;
            WriteCentered("Vissza");
        }
    }
}