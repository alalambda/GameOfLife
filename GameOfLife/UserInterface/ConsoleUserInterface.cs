using GameOfLife.Interfaces;
using GameOfLife.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.UserInterface
{
    public class ConsoleUserInterface : IUserInterface
    {
        private const string DimensionXName = "X";
        private const string DimensionYName = "Y";

        public int DimensionXInput()
        {
            int x = GetInput(DimensionXName, out x);
            return x;
        }

        public int DimensionYInput()
        {
            int y = GetInput(DimensionYName, out y);
            return y;
        }

        private int GetInput(string dimensionName, out int dimension)
        {
            Console.WriteLine($"{dimensionName} = ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out dimension))
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine($"{dimensionName} = ");
                input = Console.ReadLine();
            }
            return dimension;
        }

        public void LiveCellsOutput()
        {

        }

        public void IterationsOutput()
        {

        }
    }
}
