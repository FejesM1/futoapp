using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using futoapp.Models;

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
            ApplyTheme(); //színek biztosítása
            Fomenu();
        }
        #region Téma színek
        static void ApplyTheme()
        {
            Console.BackgroundColor = activeBackground;
            Console.ForegroundColor = activeForeground;
        }
        #endregion
        #region Alcím
        static void CurrentaltTitle()
        {
            Console.ForegroundColor = altTitle;
        }
        #endregion
        #region Cím
        static void CurrentTitle()
        {
            Console.ForegroundColor = activeTitle;
        }
        #endregion
        #region Jelenlegi választás
        static void OptionColor()
        {
            if (activeBackground == ConsoleColor.DarkGreen)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else
                Console.ForegroundColor = ConsoleColor.Green;
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
                        case ConsoleKey.Enter:
                            selected = true;
                            break;
                        case ConsoleKey.UpArrow:
                            if (cPoint > 0) cPoint -= 1;
                            break;
                        case ConsoleKey.DownArrow:
                            if (cPoint < 3) cPoint += 1;
                            break;
                    }
                } while (!selected);

                switch (cPoint)
                {
                    case 0:  //Egyéniadat felvitele
                        Console.Clear();
                        Egyeniadatfelvitel();
                        WriteCentered("Enterre tovább...");
                        Console.ReadLine();
                        break;

                    case 1: //Edzésiadat felvitele
                        Console.Clear();
                        FutasMenu();
                        WriteCentered("Enterre tovább...");
                        Console.ReadLine();
                        break;

                    case 2: //Egyéniadat módosítása
                        Console.Clear();


                        WriteCentered("Enterre tovább...");
                        Console.ReadLine();
                        break;

                    

                    case 3: //Beállítások
                        Console.Clear();
                        Beallitasok();

                        break;

                    case 4: // Kilépés
                        Console.Clear();
                        WriteCentered("Biztosan kilép? (i/n): ");
                        if (Console.ReadKey().Key == ConsoleKey.I)
                        {
                            Console.Clear();

                            return;
                        }
                        else
                        {
                            cPoint = 0;
                        }
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
            WriteOption("Edzési adatok", 1);


            if (cPoint == 2) Console.ForegroundColor = altTitle;
            else Console.ForegroundColor = activeForeground;
            WriteCentered("Beállítások");

            if (cPoint == 3) Console.ForegroundColor = activeBack;
            else Console.ForegroundColor = activeForeground;
            WriteCentered("Kilépés");

            Console.ForegroundColor = activeForeground;
            Console.WriteLine($"\nJelenlegi Cpoint: {cPoint}");
        }

        #region Középrehelyezés
        static void WriteCentered(string text)
        {
            string StripAnsi(string Rtext)
            {
                return System.Text.RegularExpressions.Regex.Replace(Rtext, @"\u001b\[[0-9;]*m", "");
            }
            string temptext = StripAnsi(text);

            int width = Console.WindowWidth;
            int leftPadding = (width - temptext.Length) / 2;
            if (leftPadding < 0) leftPadding = 0;
            Console.WriteLine(new string(' ', leftPadding) + text);
        }
        #endregion
        #region Középrehelyezés Szöveg
        static void WriteCenteredText(string text)
        {
            string StripAnsi(string Rtext)
            {
                return System.Text.RegularExpressions.Regex.Replace(Rtext, @"\u001b\[[0-9;]*m", "");
            }
            string temptext = StripAnsi(text);

            int width = Console.WindowWidth;
            int leftPadding = (width - temptext.Length) / 2 - 2;
            if (leftPadding < 0) leftPadding = 0;
            Console.Write(new string(' ', leftPadding) + text);
        }
        #endregion

        #region EgyeniAdatFelvitel
        public static void Egyeniadatfelvitel()
        {
            ApplyTheme(); // Színek biztosítása
            CurrentTitle();
            WriteCentered("*** ÚJ EGYÉNI ADAT FELVITELE ***\n");

            Console.ForegroundColor = activeForeground;
            WriteCenteredText("Magasság(cm): ");
            int magassag = int.Parse(Console.ReadLine());
            WriteCenteredText("Testtömeg(kg): ");
            int testtomeg = int.Parse(Console.ReadLine());
            WriteCenteredText("Nyugalmi pulzus (csak a szám, pl: 60): ");
            int nyugpulz = int.Parse(Console.ReadLine());
            WriteCenteredText("Kitűzött lefutás idejére cél (óó:pp:mm): ");
            DateTime celido = DateTime.Parse(Console.ReadLine());
            Szemely ujSzemely = new Szemely(magassag, testtomeg, nyugpulz, celido);

            // 3. A KiirTxt metódus meghívása
            // Meg kell adni a fájl nevét paraméterként (pl. "szemelyek.txt")
            try
            {
                ujSzemely.KiirTxt("szemelyek.txt");
                WriteCentered("Sikeres mentés!");
            }
            catch (Exception ex)
            {
                WriteCentered("\nHiba történt a mentéskor: " + ex.Message);
            }
        }
        #endregion

        # region EdzésiAdatfelvitel
        public static void Edzesiadatfelvitel()
        {
            ApplyTheme();
            CurrentTitle();
            WriteCentered("*** ÚJ EDZÉSI ADAT FELVITELE ***\n");
            Console.ForegroundColor = activeForeground;
            WriteCenteredText("Dátum (éé-hh-nn): ");
            DateTime datum = DateTime.Parse(Console.ReadLine());
            WriteCenteredText("Távolság (km): ");
            int tavolsag = int.Parse(Console.ReadLine());
            WriteCenteredText("5km lefutási ideje (példa: óó:pp:mm): ");
            string ido = Console.ReadLine();
            WriteCenteredText("Maximális pulzus edzés során (példa: 180/perc): ");
            string maxpulz = Console.ReadLine();

            Futas ujEdzes = new Futas(datum, tavolsag, ido, maxpulz);

            try
            {
                Futas.Hozzaad(ujEdzes);
                WriteCentered("Sikeres mentés!");
            }
            catch (Exception ex)
            {
                WriteCentered("\nHiba történt a mentéskor: " + ex.Message);
            }
            
        }
        #endregion
        #region Középen beolvasás
        static string ReadCentered(string prompt)
        {
            int width = Console.WindowWidth;
            int leftPadding = (width - prompt.Length) / 2;
            if (leftPadding < 0) leftPadding = 0;
            Console.Write(new string(' ', leftPadding) + prompt);
            // Kurzor pozíciójának beállítása.
            return Console.ReadLine();
        }
        #endregion
        #region Beállítások
        static int belcPoint = 0;

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
                        Tema();
                        break;

                    case 1: //Szövegelrendezés
                        Console.Clear();
                        WriteCentered("Enterre tovább...");
                        Console.ReadLine();
                        break;

                    case 2: // Kilépés a Főmenübe

                        return;
                }

            } while (true);
        }

        static void AlShowMenu(int belcPoint)
        {

            ApplyTheme();
            Console.Clear();
            CurrentaltTitle();
            WriteCentered("--- Beállítások ---");
            Console.ForegroundColor = activeForeground;

            if (belcPoint == 0) OptionColor();
            else Console.ForegroundColor = activeForeground;
            WriteCentered("Téma");

            if (belcPoint == 1) OptionColor();
            else Console.ForegroundColor = activeForeground;
            WriteCentered("Szöveg elrendezés (kezdetleges)");



            if (belcPoint == 2) Console.ForegroundColor = activeBack;
            else Console.ForegroundColor = activeForeground;
            WriteCentered("Vissza");

            Console.ForegroundColor = activeForeground;
            Console.WriteLine($"\nJelenlegi Cpoint: {belcPoint}");
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
                        case ConsoleKey.Enter:
                            selected = true;
                            break;
                        case ConsoleKey.UpArrow:
                            if (belcPoint > 0) belcPoint -= 1;
                            break;
                        case ConsoleKey.DownArrow:
                            if (belcPoint < 11) belcPoint += 1;
                            break;
                    }
                } while (!selected);


                switch (belcPoint)
                {
                    case 0: activeBackground = ConsoleColor.Black; activeForeground = ConsoleColor.White; altTitle = ConsoleColor.Magenta; activeTitle = ConsoleColor.Yellow; activeBack = ConsoleColor.Red; break;
                    case 1: activeBackground = ConsoleColor.White; activeForeground = ConsoleColor.Black; altTitle = ConsoleColor.Magenta; activeTitle = ConsoleColor.DarkYellow; activeBack = ConsoleColor.Red; break;
                    case 2: activeBackground = ConsoleColor.DarkRed; activeForeground = ConsoleColor.White; altTitle = ConsoleColor.Blue; activeTitle = ConsoleColor.Yellow; activeBack = ConsoleColor.Blue; break;
                    case 3: activeBackground = ConsoleColor.DarkMagenta; activeForeground = ConsoleColor.White; altTitle = ConsoleColor.Black; activeTitle = ConsoleColor.Yellow; activeBack = ConsoleColor.DarkYellow; break;
                    case 4: activeBackground = ConsoleColor.Black; activeForeground = ConsoleColor.Cyan; altTitle = ConsoleColor.Magenta; activeTitle = ConsoleColor.DarkYellow; activeBack = ConsoleColor.Red; break;
                    case 5: activeBackground = ConsoleColor.Black; activeForeground = ConsoleColor.DarkYellow; altTitle = ConsoleColor.Magenta; activeTitle = ConsoleColor.Cyan; activeBack = ConsoleColor.Red; break;
                    case 6: activeBackground = ConsoleColor.Black; activeForeground = ConsoleColor.DarkRed; altTitle = ConsoleColor.Magenta; activeTitle = ConsoleColor.Cyan; activeBack = ConsoleColor.Blue; break;
                    case 7: activeBackground = ConsoleColor.DarkYellow; activeForeground = ConsoleColor.White; altTitle = ConsoleColor.Magenta; activeTitle = ConsoleColor.Blue; activeBack = ConsoleColor.Red; break;
                    case 8: activeBackground = ConsoleColor.DarkBlue; activeForeground = ConsoleColor.White; altTitle = ConsoleColor.Magenta; activeTitle = ConsoleColor.Yellow; activeBack = ConsoleColor.Red; break;
                    case 9: activeBackground = ConsoleColor.DarkGreen; activeForeground = ConsoleColor.White; altTitle = ConsoleColor.Magenta; activeTitle = ConsoleColor.Yellow; activeBack = ConsoleColor.Red; break;
                    case 10: activeBackground = ConsoleColor.DarkYellow; activeForeground = ConsoleColor.Blue; altTitle = ConsoleColor.Magenta; activeTitle = ConsoleColor.Cyan; activeBack = ConsoleColor.Red; break;
                    case 11:

                        belcPoint = 0;
                        return;
                }

                // Azonnali alkalmazás, hogy látszódjon az eredmény
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

            // Segédfüggvény a lista kirajzoláshoz
            void WriteThemeOption(string text, int index)
            {
                if (belcPoint == index)
                {
                    OptionColor();
                }
                else Console.ForegroundColor = activeForeground;
                WriteCentered(text);
            }

            WriteThemeOption("Fekete háttér - Fehér szöveg (alap)", 0);
            WriteThemeOption("Fehér háttér - Fekete szöveg", 1);
            WriteThemeOption("Vörös háttér - Fehér szöveg", 2);
            WriteThemeOption("Magenta háttér - Fehér szöveg", 3);
            WriteThemeOption("Fekete háttér - Cyán szöveg", 4);
            WriteThemeOption("Fekete háttér - Sárga szöveg", 5);
            WriteThemeOption("Fekete háttér - Vörös szöveg", 6);
            WriteThemeOption("Sárga háttér - Fehér szöveg", 7);
            WriteThemeOption("Kék háttér - Fehér szöveg", 8);
            WriteThemeOption("Zöld háttér - Fehér szöveg", 9);
            WriteThemeOption("Sárga háttér - Kék szöveg", 10);



            if (belcPoint == 11)
            {
                Console.ForegroundColor = activeBack;
            }
            else
            {
                Console.ForegroundColor = activeForeground;
            }
            WriteCentered("Vissza");

            Console.ForegroundColor = activeForeground;
            Console.WriteLine($"\nJelenlegi Cpoint: {belcPoint}");
        }
        #endregion
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
                    Console.Clear();
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
            Console.WriteLine("\t" + new string('=', 50));
            string osszIdoSzoveg = Futas.OsszesitettIdo();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n\tÖsszes edzésidő: {osszIdoSzoveg}");
            Console.ForegroundColor = activeForeground;
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
    }
}