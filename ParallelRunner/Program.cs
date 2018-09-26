using System;
using System.Threading.Tasks;

namespace ParallelRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.For(0, 1000, i =>
            {
                Task game = new Task(() => GameOfLife.Program.Main(args));
                game.Start();
            });

            //Task[] taskArray = new Task[10];
            //for (int i = 0; i < taskArray.Length; i++)
            //{
            //    taskArray[i] = Task.Factory.StartNew(() => GameOfLife.Program.Main(args));
            //}
            //Task.WaitAll(taskArray);
        }
    }
}
