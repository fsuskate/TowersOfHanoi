using System;
using System.Collections.Generic;

namespace TowersOfHanoi
{
    class Tower
    {
        private Stack<int> Disks = new Stack<int>();

        public string Name { get; set; }

        public bool Verbose { get; set; }

        public int Count => Disks.Count;

        public int Add(int disk)
        {
            // Ensure this object is less than the disk it will be stacked on
            if (Disks.Count > 0 && Disks.Peek() < disk)
            {
                throw new ArgumentException("New disk cannot be greater than last disk");
            }

            Disks.Push(disk);
            return Disks.Count;
        }

        public void MoveTopToTower(Tower dest)
        {
            int move = Disks.Pop();
            dest.Add(move);
        }

        public void MoveDisks(int n, Tower dest, Tower buffer)
        {
            if (n > 0)
            {
                MoveDisks(n - 1, buffer, dest);
                MoveTopToTower(dest);
                buffer.MoveDisks(n - 1, dest, this);

                if (Verbose)
                {
                    PrintTower();
                    dest.PrintTower();
                    buffer.PrintTower();
                }
            }
        }

        public void PrintTower()
        {
            Console.WriteLine("///////////////////////////////");
            Console.WriteLine($"{Name}");
            if (Disks.Count == 0)
            {
                Console.WriteLine("IS EMPTY");
                return;
            }

            var tempColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            int[] disks = new int[Disks.Count];
            Disks.CopyTo(disks, 0);
            foreach (var disk in disks)
            {
                for (int i = 0; i < disk; i++)
                {
                    Console.Write("X");
                }
                Console.Write("\n");
            }
            Console.ForegroundColor = tempColor;
            Console.WriteLine("///////////////////////////////\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var t1 = new Tower { Name = "Tower 1", Verbose = true };
            var t2 = new Tower { Name = "Tower 2", Verbose = true };
            var t3 = new Tower { Name = "Tower 3", Verbose = true };

            t1.Add(6);
            t1.Add(5);
            t1.Add(4);
            t1.Add(3);
            t1.Add(2);
            t1.Add(1);

            Console.WriteLine("BEFORE GAME");
            t1.PrintTower();
            t2.PrintTower();
            t3.PrintTower();

            Console.WriteLine("MOVING DISKS");
            t1.MoveDisks(t1.Count, dest: t2, buffer: t3);

            Console.WriteLine("AFTER GAME");
            t1.PrintTower();
            t2.PrintTower();
            t3.PrintTower();

            Console.WriteLine("MOVING DISKS BACK TO ORIGINAL");
            t2.MoveDisks(t2.Count, dest: t1, buffer: t3);

            Console.WriteLine("AFTER GAME");
            t1.PrintTower();
            t2.PrintTower();
            t3.PrintTower();
        }
    }
}
