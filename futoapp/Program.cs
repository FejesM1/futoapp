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

        

        # region EdzésiAdatfelvitel
        
        #endregion
        #region Beállítások
        

       

        public static void AlShowMenu(int belcPoint)
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

            Console.ForegroundColor = Rendezes.activeForeground;
        }
        #endregion
        

        public static void ListaMegjelenites()
        {
            // Listázás sorszámmal
            

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

        
    }
}