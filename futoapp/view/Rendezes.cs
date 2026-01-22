using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace futoapp.View
{
    
    internal class Rendezes
    {
        public static ConsoleColor activeBackground = ConsoleColor.Black;
        public static ConsoleColor activeForeground = ConsoleColor.White;
        public static ConsoleColor altTitle = ConsoleColor.Magenta;
        public static ConsoleColor activeTitle = ConsoleColor.Yellow;
        public static ConsoleColor activeBack = ConsoleColor.Red;

        #region Középrehelyezés
        public static void WriteCentered(string text)
        {
            string StripAnsi(string Rtext)
            {
                return System.Text.RegularExpressions.Regex.Replace(Rtext, @"\u001b\[[0-9;]*m", "");
            }
            string temptext = StripAnsi(text);

            int width = Console.WindowWidth;
            int leftPadding = (width - temptext.Length) / 2;
            if (leftPadding < 0) leftPadding = 0;
            Console.WriteLine(new string(' ', leftPadding) + text);
        }
        #endregion
        #region Középrehelyezés Szöveg
         public static void WriteCenteredText(string text)
        {
            string StripAnsi(string Rtext)
            {
                return System.Text.RegularExpressions.Regex.Replace(Rtext, @"\u001b\[[0-9;]*m", "");
            }
            string temptext = StripAnsi(text);

            int width = Console.WindowWidth;
            int leftPadding = (width - temptext.Length) / 2 - 2;
            if (leftPadding < 0) leftPadding = 0;
            Console.Write(new string(' ', leftPadding) + text);
        }
        #endregion
        #region Téma színek
        public static void ApplyTheme()
        {
            Console.BackgroundColor = activeBackground;
            Console.ForegroundColor = activeForeground;
        }
        #endregion
        #region Alcím
        public static void CurrentaltTitle()
        {
            Console.ForegroundColor = altTitle;
        }
        #endregion
        #region Cím
        public static void CurrentTitle()
        {
            Console.ForegroundColor = activeTitle;
        }
        #endregion
        #region Jelenlegi választás
        public static void OptionColor()
        {
            if (activeBackground == ConsoleColor.DarkGreen)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else
                Console.ForegroundColor = ConsoleColor.Green;
        }
        #endregion
    }
}
