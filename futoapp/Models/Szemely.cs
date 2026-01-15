using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futoapp.Models
{
    internal class Szemely
    {
        int magassag;
        int testtomeg;
        int nyugalmiPulzus;
        int[] cel;

        public int Magassag { get { return magassag; } set { magassag = value; } }
        public int Testtomeg { get { return testtomeg; } set { testtomeg = value; } }
        public int NyugalmiPulzus { get { return nyugalmiPulzus; } set { nyugalmiPulzus = value; } }
        public int[] Cel { get { return cel; } set { cel = value; } }

        public Szemely(int magassag, int testtomeg, int nyugalmiPulzus, int[] cel)
        {
            this.Magassag = magassag;
            this.Testtomeg = testtomeg;
            this.NyugalmiPulzus = nyugalmiPulzus;
            this.Cel = cel;
        }

        public Szemely() { }


    }
}
