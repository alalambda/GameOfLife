using GameOfLife.Interfaces;
using GameOfLife.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.UserInterface
{
    public class ConsoleUserInterface : IUserInterface
    {
        public int DimensionXInput()
        {
            int dimX;
            Console.WriteLine("Enter field size");
            Console.WriteLine("X = ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out dimX))
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("X = ");
                input = Console.ReadLine();
            }
            return dimX;
        }

        public int DimensionYInput()
        {
            int dimY;
            Console.WriteLine("Y = ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out dimY))
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Y = ");
                input = Console.ReadLine();
            }
            return dimY;
        }

        public void LiveCellsOutput()
        {

        }

        public void IterationsOutput()
        {

        }
    }
}
