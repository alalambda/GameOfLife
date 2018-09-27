using GameOfLife.Interfaces;
using GameOfLife.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Runner
{
    public class ParallelRunner
    {
        private readonly IUserInterface _consoleUserInterface;

        public ParallelRunner()
        {
            _consoleUserInterface = new ConsoleUserInterface();
        }

        public void Run()
        {
            int maxGames;
            Console.WriteLine("Choose game count to run: ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out maxGames) || maxGames > 1000)
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Choose game count to run: ");
                input = Console.ReadLine();
            }

            int x = _consoleUserInterface.GetDimensionInput("x");
            int y = _consoleUserInterface.GetDimensionInput("y");

            var gameTasks = new Dictionary<int, Task>(maxGames);
            var gameDrawerTasks = new Dictionary<int, Task>(maxGames);
            
            Parallel.For(0, maxGames, i =>
            {
                var gameRunner = new GameRunner();
                gameTasks.Add(i, new Task(() => gameRunner.Start(x, y)));
                gameDrawerTasks.Add(i, new Task(() => gameRunner.Show()));
            });

            foreach (KeyValuePair<int, Task> entry in gameTasks)
            {
                Console.WriteLine($"{entry.Key + 1}. game process: {entry.Value.Id}");
            }

            Console.WriteLine("Enter game numbers to show. If you wish to show less than 8 games, end with 0.");

            int gameNo;
            for (int i = 0; i < 8; i++)
            {
                input = Console.ReadLine();
                while (!int.TryParse(input, out gameNo))
                {
                    Console.WriteLine("Invalid input.");
                    Console.WriteLine("Enter game no. to show: ");
                    input = Console.ReadLine();
                }
                if (gameNo == 0) break;

                var gameTask = gameTasks[gameNo - 1];
                gameTask.Start();
                gameTask.Wait();
                gameDrawerTasks[gameNo - 1].Start();
                gameTask.Wait();
            }
        }
    }
}
