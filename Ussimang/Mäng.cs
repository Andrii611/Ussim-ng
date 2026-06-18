using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ussimang
{
    public static class Mäng
    {
        public static void Käivita()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Snake / Uss / Змейка";
            Console.CursorVisible = false;

            while (true)
            {
                int tase = KuvaMenu();
                if (tase == 0) break;

                MänguSeaded seaded = new MänguSeaded(tase);

                try
                {
                    Console.SetWindowSize(seaded.Laius + 2, seaded.Kõrgus + 4);
                    Console.SetBufferSize(seaded.Laius + 2, seaded.Kõrgus + 4);
                }
                catch { }

                Console.Clear();

                int offsetY = 2;
                Kaart kaart = new Kaart(seaded.Laius, seaded.Kõrgus);
                Uss uss = new Uss(seaded.Laius / 2, seaded.Kõrgus / 2 + offsetY, 4);
                Toit toit = new Toit(seaded.Laius, seaded.Kõrgus);
                int skoor = 0;
                bool escape = false;

                KaartJoonista(kaart, offsetY);
                HUD.Uuenda(skoor, uss.Pikkus, seaded.TaseNimi);

                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo klahv = Console.ReadKey(true);
                        switch (klahv.Key)
                        {
                            case ConsoleKey.UpArrow:
                            case ConsoleKey.W:
                                if (uss.PraeguneSuund != Suund.Alla)
                                    uss.PraeguneSuund = Suund.Üles;
                                break;
                            case ConsoleKey.DownArrow:
                            case ConsoleKey.S:
                                if (uss.PraeguneSuund != Suund.Üles)
                                    uss.PraeguneSuund = Suund.Alla;
                                break;
                            case ConsoleKey.LeftArrow:
                            case ConsoleKey.A:
                                if (uss.PraeguneSuund != Suund.Paremale)
                                    uss.PraeguneSuund = Suund.Vasakule;
                                break;
                            case ConsoleKey.RightArrow:
                            case ConsoleKey.D:
                                if (uss.PraeguneSuund != Suund.Vasakule)
                                    uss.PraeguneSuund = Suund.Paremale;
                                break;
                            case ConsoleKey.Escape:
                                escape = true;
                                break;
                        }
                    }

                    if (escape) break;

                    uss.Liigu();
                    Punkt pea = uss.HangiPea();

                    if (pea.X <= 0 || pea.X >= seaded.Laius - 1 ||
                        pea.Y <= offsetY || pea.Y >= seaded.Kõrgus + offsetY - 1)
                    {
                        Heliefektid.MängiKaotust();
                        break;
                    }

                    if (uss.OnEndasesKehast())
                    {
                        Heliefektid.MängiKaotust();
                        break;
                    }

                    if (pea.X == toit.Asukoht.X && pea.Y == toit.Asukoht.Y)
                    {
                        skoor += toit.Väärtus;
                        uss.Kasva();

                        if (toit.Tüüp == ToitTüüp.Kuldne)
                            Heliefektid.MängiKuldset();
                        else
                            Heliefektid.MängiSöömist();

                        if (seaded.KiirusMS > 40)
                            seaded.KiirusMS = Math.Max(40, seaded.KiirusMS - (skoor / 50));

                        toit.LooUusToit();
                        HUD.Uuenda(skoor, uss.Pikkus, seaded.TaseNimi);
                    }

                    Thread.Sleep(seaded.KiirusMS);
                }

                if (!escape)
                {
                    KuvaGameOver(skoor);
                    Console.Write("Sisesta oma nimi: ");
                    Console.CursorVisible = true;
                    string nimi = Console.ReadLine() ?? "Anonüümne";
                    Console.CursorVisible = false;
                    if (string.IsNullOrWhiteSpace(nimi)) nimi = "Anonüümne";

                    Edetabel.Salvesta(nimi, skoor, seaded.TaseNimi);
                    Edetabel.KuvaEdetabel();
                    Console.WriteLine("\nVajuta ENTER, et jätkata...");
                    Console.ReadLine();
                }
            }

            Console.Clear();
            Console.WriteLine("Nägemist!");
        }

        private static int KuvaMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n  *** SNAKE / USS / ЗМЕЙКА ***\n");
            Console.ResetColor();
            Console.WriteLine("  @ = tavaline toit (+10)   $ = kuldne toit (+30)\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  [1] Lihtne   — 200ms");
            Console.WriteLine("  [2] Keskmine — 110ms");
            Console.WriteLine("  [3] Raske    —  55ms");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  [4] Edetabel");
            Console.WriteLine("  [0] Välju");
            Console.ResetColor();
            Console.Write("\n  Vali: ");

            while (true)
            {
                var k = Console.ReadKey(true);
                if (k.KeyChar == '1') return 1;
                if (k.KeyChar == '2') return 2;
                if (k.KeyChar == '3') return 3;
                if (k.KeyChar == '4')
                {
                    Edetabel.KuvaEdetabel();
                    Console.WriteLine("\nVajuta ENTER...");
                    Console.ReadLine();
                    return KuvaMenu();
                }
                if (k.KeyChar == '0') return 0;
            }
        }

        private static void KuvaGameOver(int skoor)
        {
            Console.SetCursorPosition(10, 8);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╔══════════════════════╗");
            Console.SetCursorPosition(10, 9);
            Console.WriteLine("║      MÄNG LÄBI!      ║");
            Console.SetCursorPosition(10, 10);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"║   Skoor: {skoor,5} punkti  ║");
            Console.SetCursorPosition(10, 11);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╚══════════════════════╝");
            Console.ResetColor();
            Console.SetCursorPosition(0, 13);
        }

        private static void KaartJoonista(Kaart kaart, int offsetY)
        {
            foreach (var p in kaart.Takistused)
            {
                Console.SetCursorPosition(p.X, p.Y + offsetY);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write('#');
                Console.ResetColor();
            }
        }
    }
}
