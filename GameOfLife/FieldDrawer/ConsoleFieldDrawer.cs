using GameOfLife.Interfaces;
using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameOfLife.FieldDrawer
{
     public class ConsoleFieldDrawer : IFieldDrawer<MatrixField>
    {
        public void DrawField(MatrixField MatrixField)
        {
            Console.Clear();
            for (int x = 0; x < MatrixField.DimX; x++)
            {
                for (int y = 0; y < MatrixField.DimY; y++)
                {
                    Console.Write((int) MatrixField.Field.Cells[x, y].State);
                }
                Console.WriteLine();
            }
        }

        public void NormalizeFrame()
        {
            Thread.Sleep(1000);
        }
    }
}
