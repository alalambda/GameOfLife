using GameOfLife.FieldDrawer;
using GameOfLife.Interfaces;
using GameOfLife.Logger;
using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameOfLife.Logic
{
    public class Game : IGame
    {
        private int Iterations = 0;
        private int LiveCells = 0;
        private bool IsGameRunning = true;
        private MatrixField MatrixField;
        private readonly JsonLogger<Data> JsonLogger = new JsonLogger<Data>();
        private ConsoleFieldDrawer ConsoleFieldDrawer = new ConsoleFieldDrawer();

        //public Game(int x, int y)
        //{
        //    MatrixField = new MatrixField(x, y);
        //}
       
        public void Start()
        {
            int dimX;
            int dimY;

            Console.WriteLine("Enter field size");
            Console.WriteLine("X = ");
            string input = Console.ReadLine();
            while (!int.TryParse(input, out dimX))
            {
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

            Init();
            Run();
            Console.ReadLine();
        }

        private void Init()
        {
            Console.Clear();
            MatrixField.ConfigureFirstGeneration();
            ConsoleFieldDrawer.DrawField(MatrixField);
        }

        private void Run()
        { 
            while (IsGameRunning && !Console.KeyAvailable)
            {
                Console.Clear();

                var nextGenField = MatrixField.ConfigureNextGeneration();
                var nextGenMatrixField = new MatrixField(MatrixField.DimX, MatrixField.DimY) { Field = nextGenField };
                ConsoleFieldDrawer.DrawField(nextGenMatrixField);
                MatrixField.Field = nextGenField;

                LiveCells = MatrixField.CountLiveCells();
                Console.WriteLine($"Currently live cells count is {LiveCells}");

                Iterations++;

                Console.WriteLine("Press any key to stop the game");

                IsGameRunning = IsContinueGame(MatrixField);

                Thread.Sleep(1000);
            }
            Console.WriteLine("Game Over");
            Console.WriteLine($"Total iterations: {Iterations}");

            if (LiveCells != 0)
            {
                Console.WriteLine("Save Progress? y/n");
                var key = Console.ReadKey().Key.ToString().ToLower();
                if (key.Equals("y"))
                {
                    SaveGame();
                }
            }
        }

        public void SaveGame()
        {
            var Data = new Data(MatrixField, Iterations, LiveCells);
            JsonLogger.SaveGameToLogFile(Data);
        }

        public Data RestoreGame()
        {
            Data Data = JsonLogger.RestoreLastGameFromLogFile();
            return Data;
        }

        public bool IsContinueGame(MatrixField matrixField)
        {
            for (int i = 0; i < matrixField.DimY; i++)
            {
                for (int j = 0; j < matrixField.DimX; j++)
                {
                    if (State.Alive == matrixField.Field.Cells[j, i].State)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
