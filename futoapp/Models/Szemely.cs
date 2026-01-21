using futoapp.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace futoapp.Models
{
    internal class Szemely
    {
        int magassag;
        int testtomeg;
        int nyugalmiPulzus;
        DateTime cel;

        public int Magassag { get { return magassag; } set { magassag = value; } }
        public int Testtomeg { get { return testtomeg; } set { testtomeg = value; } }
        public int NyugalmiPulzus { get { return nyugalmiPulzus; } set { nyugalmiPulzus = value; } }
        public DateTime Cel { get { return cel; } set { cel = value; } }

        public Szemely(int magassag, int testtomeg, int nyugalmiPulzus, DateTime cel)
        {
            this.Magassag = magassag;
            this.Testtomeg = testtomeg;
            this.NyugalmiPulzus = nyugalmiPulzus;
            this.Cel = cel;
        }

        public Szemely() { }

        public Szemely(string sor)
        {
            string[] bontas = sor.Split(',');
            Magassag = int.Parse(bontas[0]);
            Testtomeg = int.Parse(bontas[1]);
            NyugalmiPulzus = int.Parse(bontas[2]);
            Cel = DateTime.Parse(bontas[3]);
        }

        public void KiirTxt(string szemely)
        {
            File.WriteAllText(szemely, ToString() + Environment.NewLine);
        }

        public override string ToString()
        {
            return $"{Magassag},{Testtomeg},{NyugalmiPulzus},{Cel}";
        }



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



        public int OsszTav(string futasok)
        {
            int osszTav = 0;
            foreach (var f in Futas.Futasok)
            {
                osszTav += f.Tav;
            }
            return osszTav;
        }


    }
}
