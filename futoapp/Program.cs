using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using futoapp.Models;
using futoapp.View;
using System.IO;
using futoapp.Controllers;

namespace futoapp
{
    internal class Program
    {
        

        #region Main
        static void Main(string[] args)
        {
            string utvonal = "futasok.txt";

            // Ellenőrzés
            if (!File.Exists(utvonal))
            {
                
                File.Create(utvonal).Close();
                
            }
            else
            {
                Console.WriteLine("Már létezik.");
            }
            Futas.Beolvasas();
            Rendezes.ApplyTheme(); //színek biztosítása
            Rendezes.Fomenu();
        }
        #endregion
       
    }
}