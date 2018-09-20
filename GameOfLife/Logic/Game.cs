using GameOfLife.FieldDrawer;
using GameOfLife.Interfaces;
using GameOfLife.Logger;
using GameOfLife.Model;
using GameOfLife.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameOfLife.Logic
{
    public class Game
    {
        private readonly ILogger<Data> JsonLogger = new JsonLogger<Data>();
        private readonly IFieldDrawer<MatrixField> ConsoleFieldDrawer = new ConsoleFieldDrawer();
        private readonly IUserInterface ConsoleUserInterface = new ConsoleUserInterface();

        private MatrixField MatrixField;

        private int Iterations = 0;
        private int LiveCells = 0;
        private bool IsGameRunning = true;

        private Game()
        {
            InitField();
            InitGame();
            Run();
        }

        public static Game Start()
        {
            return new Game();
        }

        private void InitField()
        {
            int x = ConsoleUserInterface.GetDimensionInput("x");
            int y = ConsoleUserInterface.GetDimensionInput("y");

            MatrixField = new MatrixField(x, y);
        }

        private void InitGame()
        {
            MatrixField.ConfigureFirstGeneration();
            ConsoleFieldDrawer.DrawField(MatrixField);
        }

        private void Run()
        { 
            while (IsGameRunning && !IsTerminateGame())
            {
                EvolveGeneration();

                ConsoleUserInterface.AskForTerminateGame();

                IsGameRunning = IsContinueGame(MatrixField);

                LiveCells = MatrixField.CountLiveCells();
                ConsoleUserInterface.LiveCellsOutput(LiveCells);

                Iterations++;
                ConsoleUserInterface.IterationsOutput(Iterations);

                NormalizeFrame();
            }
            ConsoleUserInterface.GameOverOutput();
            AskForSaveGame();
        }

        private void EvolveGeneration()
        {
            var nextGenField = MatrixField.ConfigureNextGeneration();
            MatrixField.Field = nextGenField;
            ConsoleFieldDrawer.DrawField(MatrixField);
        }

        private void AskForSaveGame()
        {
            if (LiveCells != 0)
            {
                if (ConsoleUserInterface.IsGameSaveRequired())
                {
                    SaveGame();
                }
            }
        }

        private void SaveGame()
        {
            var Data = new Data(MatrixField, Iterations, LiveCells);
            JsonLogger.SaveGameToLogFile(Data);
        }

        private Data RestoreGame()
        {
            Data Data = JsonLogger.RestoreLastGameFromLogFile();
            return Data;
        }

        private bool IsContinueGame(MatrixField matrixField)
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

        private bool IsTerminateGame()
        {
            return ConsoleUserInterface.IsAnyKeyPressed();
        }

        private void NormalizeFrame()
        {
            Thread.Sleep(1000);
        }
    }
}
