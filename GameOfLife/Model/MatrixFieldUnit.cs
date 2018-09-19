using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class MatrixFieldUnit
    {
        public Cell[,] Cells { get; set; }

        public MatrixFieldUnit(int x, int y)
        {
            this.Cells = new Cell[x, y];
        }
    }
}
