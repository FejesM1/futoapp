using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futoapp.Models
{
    internal class Edzes
    {
        int[] datum;
        int tavolsag;
        int[] ido;
        int maxPulzus;

        public Edzes(int[] datum, int tavolsag, int[] ido, int maxPulzus)
        {
            this.Datum = datum;
            this.Tavolsag = tavolsag;
            this.Ido = ido;
            this.MaxPulzus = maxPulzus;
        }

        public int[] Datum { get { return datum; } set { datum = value; } }
        public int Tavolsag { get { return tavolsag; } set { tavolsag = value; } }
        public int[] Ido { get { return ido; } set { ido = value; } }
        public int MaxPulzus { get { return maxPulzus; } set { maxPulzus = value; } }
    }
}
