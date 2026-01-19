using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace futoapp.Models
{
    internal class Futas
    {
        // Adattagok
        public DateTime Datum { get; set; }
        public int Tav { get; set; }
        public string Idotart { get; set; }
        public string Maxpulz { get; set; }

        // Statikus lista, ami a memóriában tárolja az adatokat
        public static List<Futas> Futasok = new List<Futas>();
        private static string fajlNev = "futasok.txt";

        // Konstruktorok
        public Futas() { }

        public Futas(DateTime datum, int tav, string idotart, string maxpulz)
        {
            this.Datum = datum;
            this.Tav = tav;
            this.Idotart = idotart;
            this.Maxpulz = maxpulz;
        }

        public Futas(string sor)
        {
            string[] bontas = sor.Split(',');
            Datum = DateTime.Parse(bontas[0]);
            Tav = int.Parse(bontas[1]);
            Idotart = bontas[2];
            Maxpulz = bontas[3];
        }

        // CSV formázás fájlba íráshoz
        public string ToTxt()
        {
            return string.Format("{0:yyyy-MM-dd},{1},{2},{3}", Datum, Tav, Idotart, Maxpulz);
        }

        // Megjelenítéshez formázott string
        public override string ToString()
        {
            return $"{Datum:yyyy-MM-dd} | {Tav}km | Idő: {Idotart} | Pulzus: {Maxpulz}";
        }

        // --- FÁJLKEZELÉS ---

        // 1. Új elem hozzáadása és mentés
        public static void Hozzaad(Futas ujFutas)
        {
            Futasok.Add(ujFutas);
            Mentes();
        }

        // 2. Fájl beolvasása (Program induláskor kell meghívni)
        public static void Beolvasas()
        {
            Futasok.Clear();
            if (File.Exists(fajlNev))
            {
                string[] sorok = File.ReadAllLines(fajlNev, Encoding.UTF8);
                // Az első sor a fejléc, azt kihagyjuk (ciklus 1-től indul)
                for (int i = 1; i < sorok.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(sorok[i]))
                    {
                        Futasok.Add(new Futas(sorok[i]));
                    }
                }
            }
        }

        // 3. Fájl mentése (Felülírás) - Törlés és Módosítás után is ezt hívjuk
        public static void Mentes()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fajlNev, false, Encoding.UTF8)) // false = felülírás
                {
                    sw.WriteLine("Datum,Tav,Idotart,Maxpulz"); // Fejléc
                    foreach (var f in Futasok)
                    {
                        sw.WriteLine(f.ToTxt());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba a mentés során: " + ex.Message);
            }
        }

        // 4. Törlés index alapján
        public static void Torles(int index)
        {
            if (index >= 0 && index < Futasok.Count)
            {
                Futasok.RemoveAt(index);
                Mentes(); // Frissítjük a fájlt
            }
        }
    }
}