using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    interface IGame
    {
        void Run();
        bool IsContinueGame(MatrixField MatrixField);
        void SaveGame();
        Data RestoreGame();
    }
}
