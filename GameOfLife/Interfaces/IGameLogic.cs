﻿using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface IGameLogic
    {
        MatrixField EvolveGeneration(MatrixField matrixField);
        void SaveGame(MatrixField matrixField, int iterations, int liveCells);
        Data RestoreGame();
        bool IsTerminateGame();

    }
}