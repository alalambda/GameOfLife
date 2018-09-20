using GameOfLife.Interfaces;
using GameOfLife.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.UserInterface
{
    public class ConsoleUserInterface : IUserInterface
    {

        public int GetDimensionInput(string dimensionName)
        {
            int dimension;
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

        public void LiveCellsOutput(int liveCells)
        {
            Console.WriteLine($"Live cells count is {liveCells}");
        }

        public void IterationsOutput(int iterations)
        {
            Console.WriteLine($"Total iterations: {iterations}");
        }

        public bool IsAnyKeyPressed()
        {
            return Console.KeyAvailable;
        }

        public void GameOverOutput()
        {
            Console.WriteLine("Game Over");
            Console.Read();
        }

        public void AskForTerminateGame()
        {
            Console.WriteLine("Press any key to stop the game");
        }

        public void AskForSaveGame()
        {
            Console.WriteLine("Save Game? y/n");
        }

        public bool IsGameSaveRequired()
        {
            var key = Console.ReadKey().Key.ToString().ToLower();
            return key.Equals("y") ? true : false;
        }
    }
}
