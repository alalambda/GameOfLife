using GameOfLife.Interfaces;
using GameOfLife.Logic;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
