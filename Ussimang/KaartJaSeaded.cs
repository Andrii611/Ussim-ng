using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ussimang
{
    public class Kaart
    {
        public List<Punkt> Takistused { get; private set; } = new List<Punkt>();
        public int Laius { get; }
        public int Kõrgus { get; }

        public Kaart(int laius, int kõrgus)
        {
            Laius = laius;
            Kõrgus = kõrgus;

            for (int x = 0; x < laius; x++)
            {
                Takistused.Add(new Punkt(x, 0, '#', ConsoleColor.DarkCyan));
                Takistused.Add(new Punkt(x, kõrgus - 1, '#', ConsoleColor.DarkCyan));
            }
            for (int y = 1; y < kõrgus - 1; y++)
            {
                Takistused.Add(new Punkt(0, y, '#', ConsoleColor.DarkCyan));
                Takistused.Add(new Punkt(laius - 1, y, '#', ConsoleColor.DarkCyan));
            }
        }
    }

    public class MänguSeaded
    {
        public int Laius { get; set; }
        public int Kõrgus { get; set; }
        public int KiirusMS { get; set; }
        public string TaseNimi { get; set; }

        public MänguSeaded(int tase)
        {
            switch (tase)
            {
                case 1: KiirusMS = 200; Laius = 50; Kõrgus = 22; TaseNimi = "Lihtne"; break;
                case 2: KiirusMS = 110; Laius = 50; Kõrgus = 22; TaseNimi = "Keskmine"; break;
                case 3: KiirusMS = 55; Laius = 50; Kõrgus = 22; TaseNimi = "Raske"; break;
            }
        }
    }

    public static class HUD
    {
        public static void Uuenda(int skoor, int pikkus, string taseNimi)
        {
            Console.SetCursorPosition(1, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Skoor: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{skoor,5}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  |  Pikkus: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{pikkus,3}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  |  Tase: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{taseNimi}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  |  [ESC] väljuda");
            Console.ResetColor();
        }
    }
}
