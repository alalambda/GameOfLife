using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface IGameLogic
    {
        void SaveGame(MatrixField matrixField);
        Data RestoreGame();
    }
}
