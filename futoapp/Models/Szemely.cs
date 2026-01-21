using futoapp.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace futoapp.Models
{
    internal class Szemely
    {
        // Új mező: mikor rögzítettük az adatot?
        public DateTime RogzitesIdeje { get; set; }

        int magassag;
        int testtomeg;
        int nyugalmiPulzus;
        DateTime cel;

        public int Magassag { get { return magassag; } set { magassag = value; } }
        public int Testtomeg { get { return testtomeg; } set { testtomeg = value; } }
        public int NyugalmiPulzus { get { return nyugalmiPulzus; } set { nyugalmiPulzus = value; } }
        public DateTime Cel { get { return cel; } set { cel = value; } }

        // Konstruktor az új adat felviteléhez (mostmár dátummal)
        public Szemely(int magassag, int testtomeg, int nyugalmiPulzus, DateTime cel)
        {
            this.RogzitesIdeje = DateTime.Now; // Automatikusan a mostani időt kapja
            this.Magassag = magassag;
            this.Testtomeg = testtomeg;
            this.NyugalmiPulzus = nyugalmiPulzus;
            this.Cel = cel;
        }

        public Szemely() { }

        // Konstruktor beolvasáshoz (bővítve a dátummal)
        public Szemely(string sor)
        {
            string[] bontas = sor.Split(',');
            // Feltételezzük, hogy az 5. elem a dátum, ha nincs, akkor "Most"
            if (bontas.Length > 4)
            {
                RogzitesIdeje = DateTime.Parse(bontas[4]);
            }
            else
            {
                RogzitesIdeje = DateTime.Now;
            }

            Magassag = int.Parse(bontas[0]);
            Testtomeg = int.Parse(bontas[1]);
            NyugalmiPulzus = int.Parse(bontas[2]);
            Cel = DateTime.Parse(bontas[3]);
        }

        // Módosítva: Hozzáfűzés (Append) a felülírás helyett
        public void KiirTxt(string fajlnev)
        {
            // Ha nem létezik a fájl, létrehozzuk, ha létezik, végére írunk
            File.AppendAllText(fajlnev, ToString() + Environment.NewLine);
        }

        public override string ToString()
        {
            // Hozzáadtuk a dátumot a végére
            return $"{Magassag},{Testtomeg},{NyugalmiPulzus},{Cel},{RogzitesIdeje}";
        }

        // ... A többi metódusod (HanyszorSikerultCelIdonBelul, OsszTav) maradhat változatlan ...
        public void HanyszorSikerultCelIdonBelul(string futasokFajlNev)
        {
            int db = 0;
            foreach (var f in Futas.Futasok)
            {
                TimeSpan futasIdo = f.Idotart;
                double tavKm = (double)f.Tav;

                if (futasIdo.TotalHours > 0 && tavKm > 0)
                {
                    double atlagSebesseg = tavKm / futasIdo.TotalHours;
                    TimeSpan ido5Km = TimeSpan.FromHours(5.0 / atlagSebesseg);

                    if (ido5Km <= Cel.TimeOfDay)
                    {
                        db++;
                    }
                }
            }
            Rendezes.WriteCentered($"{db} alkalommal sikerült a célt elérni.\n");
        }
    }
}