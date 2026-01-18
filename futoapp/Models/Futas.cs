using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futoapp.Models
{
    internal class Futas
    {
        DateTime datum;
        int tav;
        string idotart;
        string maxpulz;
        private static readonly List<Futas> Futasok = new List<Futas>();

        public Futas(DateTime datum, int tav, string idotart, string maxpulz)
        {
            this.Datum = datum;
            this.Tav = tav;
            this.Idotart = idotart;
            this.Maxpulz = maxpulz;
        }

        public DateTime Datum { get => datum; set => datum = value; }
        public int Tav { get => tav; set => tav = value; }
        public string Idotart { get => idotart; set => idotart = value; }
        public string Maxpulz { get => maxpulz; set => maxpulz = value; }

        public Futas() { }

        public Futas(string sor)
        {
            string[] bontas = sor.Split(',');
            Datum = DateTime.Parse(bontas[0]);
            Tav = int.Parse(bontas[1]);
            Idotart = bontas[2];
            Maxpulz = bontas[3];
        }

        public static void Add(Futas f)
        {
            if (f == null) return;
            Futasok.Add(f);
        }

        public string ToTxt()
        {
            return string.Format("{0:yyyy-MM-dd},{1},{2},{3}", Datum, Tav, Idotart, Maxpulz);
        }

        static void Kiiras()
        {

            StreamWriter sw = new StreamWriter("futasok.txt", true, Encoding.UTF8);
                sw.WriteLine("Datum,Tav,Idotart,Maxpulz");

                foreach (var f in Futasok)
                {
                    sw.WriteLine(f.ToTxt());
                }
            

            Console.WriteLine($"Fájl sikeresen kiírva");
        }

    }
}
