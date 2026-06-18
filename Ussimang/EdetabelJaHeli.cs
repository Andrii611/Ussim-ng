using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ussimang
{
    public static class Edetabel
    {
        private static readonly string failiTee = "skoorid.txt";

        public static void Salvesta(string nimi, int skoor, string tase)
        {
            File.AppendAllLines(failiTee, new[] { $"{nimi};{skoor};{tase}" });
        }

        public static void KuvaEdetabel()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║           TOP-5  EDETABEL            ║");
            Console.WriteLine("╠══════════════════════════════════════╣");
            Console.ResetColor();

            if (!File.Exists(failiTee) || new FileInfo(failiTee).Length == 0)
            {
                Console.WriteLine("║  (veel pole tulemusi)                ║");
            }
            else
            {
                var skoorid = File.ReadAllLines(failiTee)
                    .Select(rida => rida.Split(';'))
                    .Where(osad => osad.Length >= 2)
                    .Select(osad => new
                    {
                        Nimi = osad[0],
                        Punktid = int.TryParse(osad[1], out int p) ? p : 0,
                        Tase = osad.Length > 2 ? osad[2] : "?"
                    })
                    .OrderByDescending(x => x.Punktid)
                    .Take(5)
                    .ToList();

                int koht = 1;
                foreach (var s in skoorid)
                {
                    Console.ForegroundColor = koht == 1 ? ConsoleColor.Yellow :
                                              koht == 2 ? ConsoleColor.Gray :
                                                          ConsoleColor.DarkYellow;
                    Console.WriteLine($"║  {koht}. {s.Nimi,-12} {s.Punktid,6} pt  [{s.Tase}]");
                    Console.ResetColor();
                    koht++;
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
        }
    }

    public static class Heliefektid
    {
        public static void MängiSöömist()
        {
            Task.Run(() => Console.Beep(880, 80));
        }

        public static void MängiKuldset()
        {
            Task.Run(() =>
            {
                Console.Beep(1047, 80);
                Console.Beep(1319, 80);
                Console.Beep(1568, 120);
            });
        }

        public static void MängiKaotust()
        {
            Task.Run(() =>
            {
                Console.Beep(400, 180);
                Console.Beep(300, 180);
                Console.Beep(200, 350);
            });
        }
    }
}
