using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Interfaces
{
    interface IUserInterface
    {
        int DimensionXInput();
        int DimensionYInput();
        void LiveCellsOutput();
        void IterationsOutput();

    }
}
