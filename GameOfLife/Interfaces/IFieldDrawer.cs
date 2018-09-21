using GameOfLife.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface IFieldDrawer<T>
    {
        void DrawField(T field);
        void NormalizeFrame();
    }
}
