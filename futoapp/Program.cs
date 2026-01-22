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
        static int cPoint = 0;

        static ConsoleColor activeBackground = ConsoleColor.Black;
        static ConsoleColor activeForeground = ConsoleColor.White;
        static ConsoleColor altTitle = ConsoleColor.Magenta;
        static ConsoleColor activeTitle = ConsoleColor.Yellow;
        static ConsoleColor activeBack = ConsoleColor.Red;
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
                    case 0:  // Egyéniadat menü
                        Console.Clear();
                        ApplyTheme();
                        Rendezes.WriteCentered("*** SZEMÉLYES ADATOK KEZELÉSE ***\n");
                        Rendezes.WriteCentered("1. Új mérés rögzítése / Adatok frissítése");
                        Rendezes.WriteCentered("2. Előzmények (súly/pulzus változás) megtekintése");
                        Rendezes.WriteCentered("3. Vissza");

                        Console.Write("\n\tVálasztás: ");
                        char subValasztas = Console.ReadKey().KeyChar;

                        if (subValasztas == '1')
                        {
                            Console.Clear();
                            Egyeniadatfelvitel();
                            Rendezes.WriteCentered("Enterre tovább...");
                            Console.ReadLine();
                        }
                        else if (subValasztas == '2')
                        {
                            ElozmenyekMegtekintese();
                        }
                        break;

                    case 1: //Edzésiadat felvitele
                        Console.Clear();
                        FutasMenu();
                        break;

                    case 2: //Beállítások
                        Console.Clear();
                        Kontroller.Beallitasok();

                        break;

                    case 3: // Kilépés
                        Console.Clear();
                        Rendezes.WriteCentered("Biztosan kilép? (i/n): ");
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
            Rendezes.WriteCentered("*** Futó App ***");

            Console.ForegroundColor = activeForeground;


            void WriteOption(string text, int index)
            {
                if (cPoint == index) OptionColor();
                else Console.ForegroundColor = activeForeground;
                Rendezes.WriteCentered(text);
            }

            WriteOption("Egyéni adatok", 0);
            WriteOption("Edzési adatok", 1);


            if (cPoint == 2) Console.ForegroundColor = altTitle;
            else Console.ForegroundColor = activeForeground;
            Rendezes.WriteCentered("Beállítások");

            if (cPoint == 3) Console.ForegroundColor = activeBack;
            else Console.ForegroundColor = activeForeground;
            Rendezes.WriteCentered("Kilépés");

        }

        public static void ElozmenyekMegtekintese()
        {
            Console.Clear();
            ApplyTheme();
            CurrentTitle();
            Rendezes.WriteCentered("*** TESTTÖMEG ÉS PULZUS VÁLTOZÁSA ***\n");
            Console.ForegroundColor = activeForeground;

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

        

        # region EdzésiAdatfelvitel
        
        #endregion
        #region Beállítások
        

       

        public static void AlShowMenu(int belcPoint)
        {

            ApplyTheme();
            Console.Clear();
            CurrentaltTitle();
            Rendezes.WriteCentered("--- Beállítások ---");
            Console.ForegroundColor = activeForeground;

            if (belcPoint == 0) OptionColor();
            else Console.ForegroundColor = activeForeground;
            Rendezes.WriteCentered("Téma");

            if (belcPoint == 1) Console.ForegroundColor = activeBack;
            else Console.ForegroundColor = activeForeground;
            Rendezes.WriteCentered("Vissza");
        }

        public static void Tema()
        {
            int belcPoint = 0;
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
            Rendezes.WriteCentered("--- Téma ---");
            Console.ForegroundColor = activeForeground;

            // Segédfüggvény a lista kirajzoláshoz
            void WriteThemeOption(string text, int index)
            {
                if (belcPoint == index)
                {
                    OptionColor();
                }
                else Console.ForegroundColor = activeForeground;
                Rendezes.WriteCentered(text);
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
            Rendezes.WriteCentered("Vissza");

            Console.ForegroundColor = activeForeground;
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
                Rendezes.WriteCentered("*** FUTÁS ADATOK KEZELÉSE ***");
                Console.ForegroundColor = activeForeground;
                Console.WriteLine();
                ListaMegjelenites();
                Console.WriteLine("\n");
                if (Futas.Futasok.Count > 0)
                {
                    // Példa: az első személy adataival példányosítunk (vagy módosítsd, ha több személy van!)
                    Szemely szemely = new Szemely(0, 0, 0, DateTime.Now); // Add meg a megfelelő paramétereket!
                    szemely.HanyszorSikerultCelIdonBelul("futasok.txt");
                }
                Rendezes.WriteCentered("1. Új adat felvitele");
                Rendezes.WriteCentered("2. Adatok Módosítása / Törlése");
                Rendezes.WriteCentered("3. Vissza a főmenübe");

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
            Console.ForegroundColor = activeForeground;
        }

        static void FutasModositas()
        {
            ApplyTheme();
            Console.Clear();
            CurrentTitle();
            Rendezes.WriteCentered("*** FUTÁS ADATOK MÓDOSÍTÁSA / TÖRLÉSE ***");
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
    }
}