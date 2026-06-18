using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ussimang
{
    public class Uss
    {
        private readonly List<Punkt> keha = new List<Punkt>();
        public Suund PraeguneSuund { get; set; }
        public int Pikkus => keha.Count;

        private static readonly ConsoleColor[] kehaVärvid = {
        ConsoleColor.Green, ConsoleColor.DarkGreen,
        ConsoleColor.Cyan,  ConsoleColor.DarkCyan
    };

        public Uss(int algX, int algY, int pikkus)
        {
            PraeguneSuund = Suund.Paremale;

            for (int i = 0; i < pikkus; i++)
            {
                ConsoleColor värv = i == 0 ? ConsoleColor.Cyan : kehaVärvid[i % kehaVärvid.Length];
                char sümbol = i == 0 ? 'O' : 'o';
                var p = new Punkt(algX - i, algY, sümbol, värv);
                keha.Add(p);
                p.Joonista();
            }
        }

        public void Liigu()
        {
            Punkt pea = keha[0];
            Punkt uusPea = new Punkt(pea.X, pea.Y, 'O', ConsoleColor.Cyan);

            switch (PraeguneSuund)
            {
                case Suund.Paremale: uusPea.X++; break;
                case Suund.Vasakule: uusPea.X--; break;
                case Suund.Alla: uusPea.Y++; break;
                case Suund.Üles: uusPea.Y--; break;
            }

            keha[0].Sümbol = 'o';
            keha[0].Värv = kehaVärvid[1 % kehaVärvid.Length];
            keha[0].Joonista();

            keha.Insert(0, uusPea);
            uusPea.Joonista();

            Punkt saba = keha[^1];
            saba.Kustuta();
            keha.RemoveAt(keha.Count - 1);
        }

        public Punkt HangiPea() => keha[0];

        public bool OnEndasesKehast()
        {
            var pea = keha[0];
            return keha.Skip(1).Any(p => p.X == pea.X && p.Y == pea.Y);
        }

        public void Kasva()
        {
            var viimane = keha[^1];
            keha.Add(new Punkt(viimane.X, viimane.Y, 'o',
                kehaVärvid[keha.Count % kehaVärvid.Length]));
        }
    }

    public class Toit
    {
        private static readonly Random rnd = new Random();
        private readonly int laius;
        private readonly int kõrgus;

        public Punkt Asukoht { get; private set; } = null!;
        public ToitTüüp Tüüp { get; private set; }
        public int Väärtus => Tüüp == ToitTüüp.Kuldne ? 30 : 10;

        public Toit(int laius, int kõrgus)
        {
            this.laius = laius;
            this.kõrgus = kõrgus;
            LooUusToit();
        }

        public void LooUusToit()
        {
            Tüüp = rnd.Next(0, 5) == 0 ? ToitTüüp.Kuldne : ToitTüüp.Tavaline;
            char sümbol = Tüüp == ToitTüüp.Kuldne ? '$' : '@';
            ConsoleColor värv = Tüüp == ToitTüüp.Kuldne ? ConsoleColor.Yellow : ConsoleColor.Red;

            int x = rnd.Next(2, laius - 2);
            int y = rnd.Next(2, kõrgus - 2);
            Asukoht = new Punkt(x, y, sümbol, värv);
            Asukoht.Joonista();
        }
    }
}
