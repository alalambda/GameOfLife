using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Loggers
{
    interface ILogger<T>
    {
        void SaveLogFile(T t);
        T RestoreLast();
    }
}
