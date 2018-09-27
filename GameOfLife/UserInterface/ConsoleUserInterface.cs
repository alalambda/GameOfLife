using GameOfLife.Interfaces;
using GameOfLife.Model;
using System;
using System.Threading;

namespace GameOfLife.UserInterface
{
    public class ConsoleUserInterface : IUserInterface
    {

        public void InvalidInputMessage()
        {
            Console.WriteLine("Invalid input.");
        }

        public int GetUserInput(string paramName)
        {
            int dimension;
            Console.WriteLine($"{paramName} = ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out dimension))
            {
                InvalidInputMessage();
                Console.WriteLine($"{paramName} = ");
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
        }

        public void AskForTerminateGame()
        {
            Console.WriteLine("Press any key to stop the game");
        }

        public bool IsGameRestoreRequired()
        {
            Console.WriteLine("Restore last saved game? 'y' or any key");
            return IsYesOptionSelected(); 
        }

        public bool IsGameSaveRequired()
        {
            Console.WriteLine("Save Game? 'y' or any key");
            return IsYesOptionSelected();
        }

        private bool IsYesOptionSelected()
        {
            var key = Console.ReadKey().Key.ToString().ToLower();
            return key.Equals("y");
        }

        public void OutputField(MatrixField MatrixField)
        {
            Console.WriteLine();
            for (int x = 0; x < MatrixField.DimX; x++)
            {
                for (int y = 0; y < MatrixField.DimY; y++)
                {
                    Console.Write((int)MatrixField.Cells[x, y].State);
                }
                Console.WriteLine();
            }
        }

        public void NormalizeOutputRate()
        {
            Thread.Sleep(1000);
        }
            
        public void AskForGameCountToRun()
        {
            Console.WriteLine("Choose game count to run: ");
        }

        public void AskForSelectedGames()
        {
            Console.WriteLine("Enter game numbers to show. If you wish to show less than 8 games, end with 0.");
        }
    }
}
