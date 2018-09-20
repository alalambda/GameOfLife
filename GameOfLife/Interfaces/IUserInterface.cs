﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface IUserInterface
    {
        int GetDimensionInput(string dimensionName);
        void LiveCellsOutput(int liveCells);
        void IterationsOutput(int iterations);
        bool IsAnyKeyPressed();
        void GameOverOutput();
        void AskForTerminateGame()
        bool IsGameSaveRequired();
    }
}
