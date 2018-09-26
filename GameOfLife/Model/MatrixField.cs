using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Model
{
    public class MatrixField
    {
        public int DimX { get; set; }
        public int DimY { get; set; }
        public Cell[,] Cells { get; set; }
        public int LiveCells { get; set; }
        public int Iterations { get; set; }

        public MatrixField(int x, int y)
        {
            this.DimX = x;
            this.DimY = y;
            this.Cells = new Cell[x, y];
        }
    }
}
