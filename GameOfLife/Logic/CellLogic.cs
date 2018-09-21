using GameOfLife.Interfaces;
using GameOfLife.Model;

namespace GameOfLife.Logic
{
    public class CellLogic : ICellLogic
    {
        public State DecideState(int aliveNeighbours, State oldState)
        {
            if (aliveNeighbours < 2 || aliveNeighbours > 3) return State.Dead;
            else if (oldState.Equals(State.Alive) && aliveNeighbours == 2 || aliveNeighbours == 3) return State.Alive;
            else if (oldState.Equals(State.Dead) && aliveNeighbours == 3) return State.Alive;
            return oldState;
        }

        public int GetAliveNeighbours(int x, int y, MatrixField matrixField)
        {
            int alive = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        int width = x + j;
                        int height = y + i;


                        if (width < 0)
                        {
                            width = matrixField.DimY - 1;
                        }
                        else if (width > matrixField.DimY - 1)
                        {
                            width = 0;
                        }

                        if (height < 0)
                        {
                            height = matrixField.DimX - 1;
                        }
                        else if (height > matrixField.DimX - 1)
                        {
                            height = 0;
                        }

                        if (State.Alive.Equals(matrixField.Cells[height, width].State)) alive++;
                    }
                }
            }

            return alive;
        }

        public int CountLiveCells(MatrixField matrixField)
        {
            int liveCells = 0;
            for (int i = 0; i < matrixField.DimY; i++)
            {
                for (int j = 0; j < matrixField.DimX; j++)
                {
                    if (State.Alive.Equals(matrixField.Cells[j, i].State))
                    {
                        liveCells++;
                    }
                }
            }
            return liveCells;
        }
    }
}
