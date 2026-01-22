using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using futoapp.Models;
using futoapp.View;
using System.IO;

namespace futoapp
{
    internal class Program
    {
        static int cPoint = 0;

        
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
            Fomenu();
        }
        
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
                        Rendezes.ApplyTheme();
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
                        Beallitasok();

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

            Rendezes.ApplyTheme();

            Console.Clear();

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
            Rendezes.ApplyTheme(); // Színek biztosítása
            Rendezes.CurrentTitle();
            Rendezes.WriteCentered("*** EGYÉNI ADAT FELVITELE/SZERKESZTÉSE ***\n");

            SzemelyAdatokMegjelenitese();
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
        # region EdzésiAdatfelvitel
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

                    case 1:

                        return;
                }

            } while (true);
        }

        static void AlShowMenu(int belcPoint)
        {

            Rendezes.ApplyTheme();
            Console.Clear();
            Rendezes.CurrentaltTitle();
            Rendezes.WriteCentered("--- Beállítások ---");
            Console.ForegroundColor = Rendezes.activeForeground;

            if (belcPoint == 0) Rendezes.OptionColor();
            else Console.ForegroundColor = Rendezes.activeForeground;
            Rendezes.WriteCentered("Téma");

            if (belcPoint == 1) Console.ForegroundColor = Rendezes.activeBack;
            else Console.ForegroundColor = Rendezes.activeForeground;
            Rendezes.WriteCentered("Vissza");
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
                    case 0: Rendezes.activeBackground = ConsoleColor.Black; 
                        Rendezes.activeForeground = ConsoleColor.White;
                        Rendezes.altTitle = ConsoleColor.Magenta;
                        Rendezes.activeTitle = ConsoleColor.Yellow;
                        Rendezes.activeBack = ConsoleColor.Red;
                        break;

                    case 1: Rendezes.activeBackground = ConsoleColor.White;
                        Rendezes.activeForeground = ConsoleColor.Black;
                        Rendezes.altTitle = ConsoleColor.Magenta;
                        Rendezes.activeTitle = ConsoleColor.DarkYellow;
                        Rendezes.activeBack = ConsoleColor.Red;
                        break;

                    case 2: Rendezes.activeBackground = ConsoleColor.DarkRed;
                        Rendezes.activeForeground = ConsoleColor.White;
                        Rendezes.altTitle = ConsoleColor.Blue;
                        Rendezes.activeTitle = ConsoleColor.Yellow;
                        Rendezes.activeBack = ConsoleColor.Blue;
                        break;

                    case 3: Rendezes.activeBackground = ConsoleColor.DarkMagenta;
                        Rendezes.activeForeground = ConsoleColor.White;
                        Rendezes.altTitle = ConsoleColor.Black;
                        Rendezes.activeTitle = ConsoleColor.Yellow;
                        Rendezes.activeBack = ConsoleColor.DarkYellow;
                        break;

                    case 4: Rendezes.activeBackground = ConsoleColor.Black;
                        Rendezes.activeForeground = ConsoleColor.Cyan;
                        Rendezes.altTitle = ConsoleColor.Magenta;
                        Rendezes.activeTitle = ConsoleColor.DarkYellow;
                        Rendezes.activeBack = ConsoleColor.Red;
                        break;

                    case 5: Rendezes.activeBackground = ConsoleColor.Black;
                        Rendezes.activeForeground = ConsoleColor.DarkYellow;
                        Rendezes.altTitle = ConsoleColor.Magenta;
                        Rendezes.activeTitle = ConsoleColor.Cyan;
                        Rendezes.activeBack = ConsoleColor.Red;
                        break;

                    case 6: Rendezes.activeBackground = ConsoleColor.Black;
                        Rendezes.activeForeground = ConsoleColor.DarkRed;
                        Rendezes.altTitle = ConsoleColor.Magenta;
                        Rendezes.activeTitle = ConsoleColor.Cyan;
                        Rendezes.activeBack = ConsoleColor.Blue;
                        break;

                    case 7: Rendezes.activeBackground = ConsoleColor.DarkYellow;
                        Rendezes.activeForeground = ConsoleColor.White;
                        Rendezes.altTitle = ConsoleColor.Magenta;
                        Rendezes.activeTitle = ConsoleColor.Blue;
                        Rendezes.activeBack = ConsoleColor.Red;
                        break;

                    case 8: Rendezes.activeBackground = ConsoleColor.DarkBlue;
                        Rendezes.activeForeground = ConsoleColor.White;
                        Rendezes.altTitle = ConsoleColor.Magenta;
                        Rendezes.activeTitle = ConsoleColor.Yellow;
                        Rendezes.activeBack = ConsoleColor.Red;
                        break;

                    case 9: Rendezes.activeBackground = ConsoleColor.DarkGreen;
                        Rendezes.activeForeground = ConsoleColor.White;
                        Rendezes.altTitle = ConsoleColor.Magenta;
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
                Console.ForegroundColor = Rendezes.activeBack;
            }
            else
            {
                Console.ForegroundColor = Rendezes.activeForeground;
            }
            Rendezes.WriteCentered("Vissza");

            Console.ForegroundColor = Rendezes.activeForeground;
        }
        #endregion
        static void FutasMenu()
        {
            // Egyszerű almenü
            while (true)
            {
                Rendezes.ApplyTheme();
                Console.Clear();
                Rendezes.CurrentTitle();
                Rendezes.WriteCentered("*** FUTÁS ADATOK KEZELÉSE ***");
                Console.ForegroundColor = Rendezes.activeForeground;
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