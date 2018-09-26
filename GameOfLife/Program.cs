using System;
using System.Threading;
using System.Threading.Tasks;
using GameOfLife.Runner;

namespace GameOfLife
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //new GameRunner().Start(false);

            int maxGames;
            Console.WriteLine("Choose game count to run: ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out maxGames) || maxGames > 1000)
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Choose game count to run: ");
                input = Console.ReadLine();
            }

            int?[] taskIds = new int?[maxGames];

            Parallel.For(0, maxGames, i =>
            {
                Task game = new Task(() => new GameRunner().Start(false));
                taskIds[i] = game.Id;
                game.Start();
            });

            for (int i = 0; i < maxGames; i++)
            {
                Console.WriteLine($"{i + 1} game process id = {taskIds[i]}");
            }

            Console.WriteLine("Enter game no. to show: ");

            int gameNo;
            while (!int.TryParse(input, out gameNo))
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Enter game no. to show: ");
                input = Console.ReadLine();
            }

            Console.ReadLine();
        }
    }
}
