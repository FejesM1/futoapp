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

        

    public int HanyszorSikerultCelIdonBelul(string futasok)
        {
            int db = 0;

            string[] sorok = File.ReadAllLines(futasok);

        
            for (int i = 1; i < sorok.Length; i++)
            {
                Futas f = new Futas(sorok[i]);

           
                TimeSpan futasIdo = TimeSpan.Parse(f.Idotart);

            
                double tavKm = f.Tav / 1000.0;

            
                double atlagSebesseg = tavKm / futasIdo.TotalHours;

            
                TimeSpan ido5Km = TimeSpan.FromHours(5.0 / atlagSebesseg);

            
                if (ido5Km <= Cel.TimeOfDay)
                {
                    db++;
                }
            }

            return db;
        }

      

    public int OsszTav(string futasok)
        {
            int osszTav = 0;

            string[] sorok = File.ReadAllLines(futasok);

            for (int i = 1; i < sorok.Length; i++)
            {
                Futas f = new Futas(sorok[i]);
                osszTav += f.Tav;
            }

            return osszTav;
        }

}
}
