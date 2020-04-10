using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2012EmeltMajus
{
    public class Futar
    {
        public int Nap;
        public int fuvarSzam;
        public int Kilometer;

        public Futar(int elsoNap, int masodikNap, int kilometer)
        {
            this.Nap = elsoNap;
            this.fuvarSzam = masodikNap;
            Kilometer = kilometer;
        }
    }
    class Program
    {
        static List<Futar> Adat = new List<Futar>();
        static Futar futar;
        static int osszheti;
        static Dictionary<int, int> dijak = new Dictionary<int, int>()
        {
            { 1, 500 },
            { 3, 700 },
            { 6, 900 },
            { 11, 1400 },
            { 21, 2000 }
        };
        static void Main(string[] args)
        {
            ElsoFeladat();
            MasodikFeladat();
            HarmadikFeladat();
            NegyedikFeladat();
            OtodikFeladat();
            HatodikFeladat();
            HetedikFeladat();
            NyolcadikFeladat();
            KilencedikFeladat();

            Console.ReadKey();
        }

        private static void KilencedikFeladat()
        {
            Console.WriteLine($"\n9. feladat: Futár e-heti munkájáért {osszheti} Ft-ot kap díjazásként.");
        }

        private static void NyolcadikFeladat()
        {
            var rendezett = Adat.OrderBy(x => x.Nap).ThenBy(x => x.fuvarSzam).ToList();

            using (StreamWriter ki = new StreamWriter(@"dijazas.txt"))
            {
                for (int i = 0; i < rendezett.Count; i++)
                {
                    var fizetes = dijak.Where(x => x.Key <= rendezett[i].Kilometer).Last().Value;

                    osszheti += fizetes;

                    ki.WriteLine($"{rendezett[i].Nap}. nap {rendezett[i].fuvarSzam} út: {fizetes} Ft");
                    ki.Flush();
                }
            }  
        }

        private static void HetedikFeladat()
        {
            Console.WriteLine("\n7. feladat:");
            Console.Write("Távolság: ");
            int megadottTav = Convert.ToInt32(Console.ReadLine());

            var fizetes = dijak.Where(x => x.Key <= megadottTav).Last().Value;

            Console.WriteLine($"{fizetes} Ft-ot kapnál díjazásként.");
        }

        private static void HatodikFeladat()
        {
            Console.WriteLine($"\n6. feladat: ");

            //Munkaszüneti napok nélkül
            var szunetNapok = Adat.GroupBy(x => x.Nap).Select(x => new { Nap = x.Key, Km = x.Sum(s => s.Kilometer) }).ToList();

            for (int i = 1; i <= 7; i++)
            {
                Console.WriteLine($"{i}. nap: {Adat.Where(x => x.Nap == i).Sum(x => x.Kilometer)} km");
            }
        }

        private static void OtodikFeladat() => Console.WriteLine($"\n5. feladat: {Adat.OrderByDescending(x => x.fuvarSzam).Select(x => x.Nap).First()}. napon volt a legtöbb fuvar.");

        private static void NegyedikFeladat()
        {
            var rendezettNapok = Adat.OrderBy(x => x.Nap).Select(x => x.Nap).ToList();

            List<int> napok = new List<int>();

            for (int i = 1; i <= 7; i++)
            {
                napok.Add(i);
            }

            var szunetNapok = napok.Except(rendezettNapok).ToList();

            Console.WriteLine($"\n4. feladat: Ezeken a napokon nem dolgozik: {String.Join(", ", szunetNapok)}");
        }

        private static void HarmadikFeladat() => Console.WriteLine($"\n3. feladat: A hét utolsó útja kilométerben: {Adat.Select(x => x.Kilometer).Last()} km");

        private static void MasodikFeladat()
        {
            Console.WriteLine("2. feladat: Az első út távolsága: {0} km", Adat.OrderBy(x => x.Nap).ThenBy(x => x.fuvarSzam).First().Kilometer);
        }

        private static void ElsoFeladat()
        {
            using (StreamReader olvas = new StreamReader(@"tavok.txt"))
            {
                while (!olvas.EndOfStream)
                {
                    string[] split = olvas.ReadLine().Split(' ');
                    int nap = Convert.ToInt32(split[0]);
                    int fSzam = Convert.ToInt32(split[1]);
                    int km = Convert.ToInt32(split[2]);

                    futar = new Futar(nap, fSzam, km);

                    Adat.Add(futar);
                }
            }
        }
    }
}
