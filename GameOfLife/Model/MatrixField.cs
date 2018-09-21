using GameOfLife.Helpers;
using GameOfLife.Interfaces;
using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Model
{
    public class MatrixField
    {
        public int DimX { get; }
        public int DimY { get; }
        public MatrixFieldUnit Field { get; set; }

        public MatrixField(int x, int y)
        {
            this.DimX = x;
            this.DimY = y;
            this.Field = new MatrixFieldUnit(x, y);
        }
    }
}
