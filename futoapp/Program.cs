using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace futoapp
{
    internal class Program
    {
        static int cPoint = 0;
        static void Main(string[] args)
        {
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
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Enter:
                            selected = true;
                            break;
                        case ConsoleKey.UpArrow:
                            if (cPoint > 0)
                            {
                                cPoint -= 1;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (cPoint < 3)
                            {
                                cPoint += 1;
                            }
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
                        Edzesiadatfelvitel();

                        WriteCentered("Enterre tovább...");
                        Console.ReadLine();

                        break;

                    case 2: //Beállítások
                        Console.Clear();
                        Beallitasok();

                        break;


                    case 3: // Kilépés
                        Console.Clear();
                        WriteCentered("Biztosan kilép? (i/n): ");
                        if (Console.ReadKey().Key == ConsoleKey.I)
                        {
                            
                            Console.Clear();
                            break;
                            
                        }
                        else
                        {
                            cPoint = 0;
                        }
                        break;
                }

            } while (cPoint != 3);
            void ShowMenu1(int cPoint)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                WriteCentered("*** Futó App ***");
                Console.ForegroundColor = ConsoleColor.White;

                if (cPoint == 0)
                {
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                WriteCentered("Egyéni adatok");
                if (cPoint == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                WriteCentered("Edzési adatok");
                if (cPoint == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                WriteCentered("Beállítások");
                if (cPoint == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                WriteCentered("Kilépés");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Jelenlegi Cpoint: {cPoint}");
            }
        }
        #region Középrehelyezés
        static void WriteCentered(string text)
        {
            // Az ANSI kódok eltávolítása a hosszúság számításához, mert valamiért beleszámít.
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
        #region Középrehelyezés Balra
        static void WriteCenteredText(string text)
        {
            // Az ANSI kódok eltávolítása a hosszúság számításához, mert valamiért beleszámít.
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
        #region EgyeniAdatFelvitel
        public static void Egyeniadatfelvitel()
        {
            WriteCentered("*** ÚJ EGYÉNI ADAT FELVITELE ***\n");
            WriteCenteredText("Magasság(cm): ");
            int magassag = int.Parse(Console.ReadLine());
            WriteCenteredText("Testtömeg(kg): ");
            int testtomeg = int.Parse(Console.ReadLine());
            WriteCenteredText("Nyugalmi pulzus (példa: 60/perc): ");
            string nyugpulz = Console.ReadLine();
            WriteCenteredText("Kitűzött lefutás idejére cél (óó:pp:mm): ");
            string celido = Console.ReadLine();

        }
        #endregion
        # region EdzésiAdatfelvitel
        public static void Edzesiadatfelvitel()
        {
            WriteCentered("*** ÚJ EDZÉSI ADAT FELVITELE ***\n");
            WriteCenteredText("Dátum (éé-hh-nn): ");
            int datum = int.Parse(Console.ReadLine());
            WriteCenteredText("Távloság (km): ");
            int tavolsag = int.Parse(Console.ReadLine());
            WriteCenteredText("5km lefutási ideje (példa: óó:pp:mm): ");
            string ido = Console.ReadLine();
            WriteCenteredText("Maximális pulzus edzés során (példa: 180/perc): ");
            string maxpulz = Console.ReadLine();

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
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Enter:
                            selected = true;
                            break;
                        case ConsoleKey.UpArrow:
                            if (belcPoint > 0)
                            {
                                belcPoint -= 1;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            if (belcPoint < 3)
                            {
                                belcPoint += 1;
                            }
                            break;
                    }
                } while (!selected);
                    
                switch (belcPoint)
                {
                    case 0:  //Téma
                        Console.Clear();
                        //Tema(); -- Comment, mivel meg nincs megcsinalva

                        WriteCentered("Enterre tovább...");
                        Console.ReadLine();

                        break;

                    case 1: //Szövegelrendezés
                        Console.Clear();
                        //Szovegelrendezes(); -- Comment, mivel meg nincs megcsinalva

                        WriteCentered("Enterre tovább...");
                        Console.ReadLine();

                        break;

                    case 2: // Kilépés
                        Console.Clear();
                        WriteCentered("Biztosan kilép? (i/n): ");
                        if (Console.ReadKey().Key == ConsoleKey.I)
                        {

                            Console.Clear();
                            Fomenu();

                        }
                        else
                        {
                            belcPoint = 0;
                        }
                        break;
                }

            } while (belcPoint != 2);
            void AlShowMenu(int belcPoint)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                WriteCentered("--- Beállítások ---");
                Console.ForegroundColor = ConsoleColor.White;

                if (belcPoint == 0)
                {

                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                WriteCentered("Téma");
                if (belcPoint == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                WriteCentered("Szöveg elrendezés (kezdetleges)");
                if (belcPoint == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                WriteCentered("Kilépés");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Jelenlegi Cpoint: {belcPoint}");
            }
        }
        #endregion
    }
}
