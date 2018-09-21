using GameOfLife.Interfaces;
using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Logic
{
    public class MatrixFieldLogic : IMatrixFieldLogic
    {
        private readonly IFieldGenerationLogic _fieldGenerationLogic;

        public MatrixFieldLogic()
        {
            _fieldGenerationLogic = new FieldGenerationLogic();
        }

        public Cell[,] GetFirstGeneration(int dimX, int dimY)
        {
            return _fieldGenerationLogic.GenerateRandomField(dimX, dimY);
        }
    }
}
