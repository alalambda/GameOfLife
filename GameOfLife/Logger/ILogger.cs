using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    interface ILogger<T>
    {
        void SaveLogFile(T t);
        T RestoreLast();
    }
}
