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

        public Game(int x, int y)
        {
            MatrixField = new MatrixField(x, y);
        }

        public bool IsContinueGame(MatrixField matrixField)
        {
            //return matrixField.Field.Cells
            //    .Cast<Cell[]>()
            //    .SelectMany(cell => cell)
            //    .Any(cell => cell.State == State.Alive);

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

        public void Run()
        {
            MatrixField.ConfigureFirstGeneration();
            Console.Clear();
            MatrixField.DrawField(null);

            while (IsGameRunning && !Console.KeyAvailable)
            {
                Console.Clear();

                var nextGenField = MatrixField.ConfigureNextGeneration();
                MatrixField.DrawField(nextGenField);
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
            JsonLogger.SaveLogFile(Data);
        }

        public Data RestoreGame()
        {
            Data Data = JsonLogger.RestoreLast();
            return Data;
        }
    }
}
