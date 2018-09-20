using GameOfLife.Logger;
using GameOfLife.Logic;
using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface IGame
    {
        void Start();
        bool IsContinueGame(MatrixField MatrixField);
        void SaveGame();
        Data RestoreGame();
    }
}
