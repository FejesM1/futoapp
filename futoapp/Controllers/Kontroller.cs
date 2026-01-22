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
    }
}
