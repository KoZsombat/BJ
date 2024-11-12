using System;
using System.Collections.Concurrent;
using System.Formats.Tar;
using System.Reflection.Metadata;

namespace ConsoleApp8
{

    internal class Program
    {
        static void Main(string[] args)
        {
            int penz = 1000;
            Console.WriteLine($"Egyenleg: {penz}");

            while (penz != 0)
            {
                Console.Clear();
                Console.WriteLine("Adj meg egy tétet: ");
                int tet = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Mit szeretnél játszani? (Black Jack, Piros vagy Fekete)");

                string jatek = Convert.ToString(Console.ReadLine());

                switch (jatek)
                {
                    case "Black Jack":
                        if (tet > penz)
                        {
                            Console.WriteLine("Nincs elég pénzed");
                            return;
                        }
                        else
                        {
                            penz -= tet;

                            Console.WriteLine($"Egyenleg: {penz}");

                            HuzLap(penz, tet);

                        }
                        break;
                    case "Piros vagy Fekete":
                        if (tet > penz)
                        {
                            Console.WriteLine("Nincs elég pénzed");
                            return;
                        }
                        else
                        {
                            penz -= tet;

                            Console.WriteLine($"Egyenleg: {penz}");

                            PirosVFekete(penz, tet);

                        }
                        break;
                    default:
                        Console.WriteLine("Nem létezik ilyen lehetőség");
                        break;
                }
            }
        }

        static void HuzLap(int penz, int tet)
        {
            Random r = new Random();
            string[] lap = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            List<string> lapok = new List<string>(lap);
            int[] ertek = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
            List<string> oszto = new List<string>();
            List<string> jatekos = new List<string>();

            int jatekosertek = 0;
            int osztoertek = 0;

            for (int i = 0; i < 2; i++)
            {
                jatekos.Add(lapok[r.Next(0, lapok.Count - 1)]);
                oszto.Add(lapok[r.Next(0, lapok.Count - 1)]);
            }
            Console.WriteLine($"Osztó lapjai: {string.Join(", ", oszto)}\nJátékos lapjai: {string.Join(", ", jatekos)}");

            string valasz = "";

            do
            {
                Console.WriteLine("Kérsz lapot? (Igen/Nem)");
                valasz = Convert.ToString(Console.ReadLine());

                switch (valasz)
                {
                    case "Igen":
                        jatekos.Add(lapok[r.Next(0, lapok.Count - 1)]);
                        Console.WriteLine($"Osztó lapjai: {string.Join(", ", oszto)}\nJátékos lapjai: {string.Join(", ", jatekos)}");
                        break;
                    case "Nem":
                        for (int i = 0; i < jatekos.Count; i++)
                        {
                            jatekosertek += ertek[lapok.IndexOf(jatekos[i])];
                        }
                        for (int i = 0; i < oszto.Count; i++)
                        {
                            osztoertek += ertek[lapok.IndexOf(oszto[i])];
                        }
                        if (jatekosertek > osztoertek && jatekosertek < 21)
                        {
                            while (osztoertek < 17)
                            {
                                oszto.Add(lapok[r.Next(0, lapok.Count - 1)]);
                                osztoertek += ertek[lapok.IndexOf(oszto[oszto.Count - 1])];
                                if (osztoertek > jatekosertek)
                                {
                                    break;
                                }
                            }
                        }
                        Console.WriteLine($"Osztó lapjai: {string.Join(", ", oszto)}\nJátékos lapjai: {string.Join(", ", jatekos)}");

                        int oossz = 0;
                        int jossz = 0;
                        if (osztoertek > 21)
                        {
                            if (!oszto.Contains("A"))
                            {
                                oossz = osztoertek;
                            }
                            else
                            {
                                oossz = osztoertek - 10;
                            }
                        }
                        else
                        {
                            oossz = osztoertek;
                        }

                        if (jatekosertek > 21)
                        {
                            if (!jatekos.Contains("A"))
                            {
                                jossz = jatekosertek;
                            }
                            else
                            {
                                jossz = jatekosertek - 10;
                            }
                        }
                        else
                        {
                            jossz = jatekosertek;
                        }

                        if (jossz > 21 && oossz > 21)
                        {
                            Console.WriteLine("Döntetlen");
                            Console.WriteLine($"Egyenleg: {penz}");
                            penz += tet;
                            return;
                        }
                        else if (jossz > 21)
                        {
                            Console.WriteLine("Vesztettél");
                            Console.WriteLine($"Egyenleg: {penz}");
                            return;
                        }
                        else if (oossz > 21)
                        {
                            Console.WriteLine("Nyertél");
                            penz += tet * 2;
                            Console.WriteLine($"Egyenleg: {penz}");
                            return;
                        }
                        else
                        {

                            if (jossz < oossz)
                            {
                                Console.WriteLine("Vesztettél");
                                Console.WriteLine($"Egyenleg: {penz}");
                                return;
                            }
                            else if (jossz > oossz)
                            {
                                Console.WriteLine("Nyertél");
                                penz += tet * 2;
                                Console.WriteLine($"Egyenleg: {penz}");
                                return;
                            }
                            else if (jossz == oossz)
                            {
                                Console.WriteLine("Döntetlen");
                                penz += tet;
                                Console.WriteLine($"Egyenleg: {penz}");
                                return;
                            };

                        }
                        break;

                    default:
                        Console.WriteLine("Rossz bemenet");
                        break;
                }
            }
            while (valasz != "Nem");

        }

        static void PirosVFekete(int penz, int tet)
        {
            Random r = new Random();

            string[] lehetoseg = { "Piros", "Fekete" };

            Console.WriteLine("Piros vagy Fekete?");

            string valasz = Convert.ToString(Console.ReadLine());

            string nyero = lehetoseg[r.Next(0,1)];

            if (nyero == valasz)
            {
                Console.WriteLine("Nyertél!");
                penz += tet * 2;
                Console.WriteLine($"Egyenleg: {penz}");
            }
            else
            {
                Console.WriteLine("Vesztettél!");
            }

        }
    }
}