using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface ILogger<T>
    {
        void SaveGameToLogFile(T t);
        T RestoreLastGameFromLogFile();
    }
}
