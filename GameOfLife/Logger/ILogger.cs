using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Loggers
{
    interface ILogger<T>
    {
        void SaveGameToLogFile(T t);
        T RestoreLastGameFromLogFile();
    }
}
