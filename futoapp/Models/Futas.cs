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
        public TimeSpan Idotart { get; set; }
        public string Maxpulz { get; set; }

        // Statikus lista, ami a memóriában tárolja az adatokat
        public static List<Futas> Futasok = new List<Futas>();
        

        // Konstruktorok
        public Futas() { }

        public Futas(DateTime datum, int tav, TimeSpan idotart, string maxpulz)
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
            Idotart = TimeSpan.Parse(bontas[2]);
            Maxpulz = bontas[3];
        }

        public string ToTxt()
        {
            return string.Format("{0:yyyy-MM-dd},{1},{2},{3}", Datum, Tav, Idotart, Maxpulz);
        }

        public override string ToString()
        {
            return $"{Datum:yyyy-MM-dd} | {Tav}km | Idő: {Idotart} | Pulzus: {Maxpulz}";
        }

        public static void Hozzaad(Futas ujFutas)
        {
            Futasok.Add(ujFutas);
            Mentes();
        }

        public static void Beolvasas()
        {
            Futasok.Clear();
                
                
                string[] sorok = File.ReadAllLines("futasok.txt", Encoding.UTF8);
                // Az első sor a fejléc, azt kihagyjuk (ciklus 1-től indul)
                for (int i = 1; i < sorok.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(sorok[i]))
                    {
                        Futasok.Add(new Futas(sorok[i]));
                    }
                }
            
        }

        public static void Mentes()
        {
            // A 'false' azt jelenti, hogy felülírjuk a fájlt (tehát tiszta lappal indulunk)
            using (StreamWriter sw = new StreamWriter("futasok.txt", false, Encoding.UTF8))
            {
                // Kiírjuk a fejlécet
                sw.WriteLine("Datum,Tav,Ido,Pulzus");

                foreach (var f in Futasok)
                {
                    // HIBA VOLT: File.AppendAllText(...) -> Ez újra megnyitná a fájlt, ami tilos ilyenkor.

                    // HELYES MEGOLDÁS: A már nyitott 'sw' változóval írunk bele:
                    sw.WriteLine(f.ToTxt());
                }
            } // Itt a 'using' blokk vége automatikusan lezárja a fájlt (Close/Dispose)
        }


        public static void Torles(int index)
        {
            if (index >= 0 && index < Futasok.Count)
            {
                Futasok.RemoveAt(index);
                Mentes(); 
            }
        }
        public static string OsszesitettIdo()
        {
            // A hibát okozó TimeSpan.TryParse hívás eltávolítva.
            // Idotart már TimeSpan típusú, így közvetlenül összeadható.
            TimeSpan osszIdo = TimeSpan.Zero;

            foreach (var futas in Futasok)
            {
                osszIdo += futas.Idotart;
            }

            return $"{osszIdo.Days} nap : {osszIdo.Hours} óra : {osszIdo.Minutes} perc : {osszIdo.Seconds} mp";
        }
    }
}