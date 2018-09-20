using GameOfLife.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Model
{
    public class Data
    {
        public MatrixField MatrixField { get; set; }
        public int Iterations { get; set; }
        public int LiveCells { get; set; }

        public Data(MatrixField matrixField, int iterations, int liveCells)
        {
            this.MatrixField = matrixField;
            this.Iterations = iterations;
            this.LiveCells = liveCells;
        }
    }
}
