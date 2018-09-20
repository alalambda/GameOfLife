using System;
using GameOfLife.Logic;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            int dimX;
            int dimY;

            Console.WriteLine("Enter field size");
            Console.WriteLine("X = ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out dimX)){
                Console.WriteLine("Invalid input.");
                Console.WriteLine("X = ");
                input = Console.ReadLine();
            }

            Console.WriteLine("Y = ");
            input = Console.ReadLine();
            while (!int.TryParse(input, out dimY))
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Y = ");
                input = Console.ReadLine();
            }

            Console.WriteLine($"X = {dimX}, Y = {dimY}");
            Console.ReadLine();

            var Game = new Game(dimX, dimY);
            Game.Run();
            Console.ReadLine();
        }
    }
}
