using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ussimang
{
    public enum Suund
    {
        Üles, Alla, Vasakule, Paremale
    }

    public enum ToitTüüp
    {
        Tavaline, Kuldne
    }

    public class Punkt
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Sümbol { get; set; }
        public ConsoleColor Värv { get; set; }

        public Punkt(int x, int y, char sümbol, ConsoleColor värv = ConsoleColor.Green)
        {
            X = x;
            Y = y;
            Sümbol = sümbol;
            Värv = värv;
        }

        public void Joonista()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = Värv;
            Console.Write(Sümbol);
            Console.ResetColor();
        }

        public void Kustuta()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
        }
    }
}
