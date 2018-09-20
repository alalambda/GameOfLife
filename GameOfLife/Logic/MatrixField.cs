using GameOfLife.Helpers;
using GameOfLife.Interfaces;
using GameOfLife.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife.Logic
{
    public class MatrixField : IField<MatrixFieldUnit>
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

        public void ConfigureFirstGeneration()
        {
            for (int i = 0; i < DimY; i++)
            {
                for (int j = 0; j < DimX; j++)
                {
                    Cell cell = new Cell() { State = StateMethods.GetRandomState<State>() };
                    Field.Cells[j, i] = cell;
                }
            }
        }

        public MatrixFieldUnit ConfigureNextGeneration()
        {
            var NextField = new MatrixFieldUnit(DimX, DimY);
            for (int i = 0; i < DimY; i++)
            {
                for (int j = 0; j < DimX; j++)
                {
                    var aliveNeighbours = GetAliveNeighboursForCell(j, i);
                    var oldState = Field.Cells[j, i].State;
                    Cell cell = new Cell() { State = DecideStateForCell(aliveNeighbours, oldState) };
                    NextField.Cells[j, i] = cell;
                }
            }
            return NextField;
        }

        public State DecideStateForCell(int aliveNeighbors, State state)
        {
            if (aliveNeighbors < 2 || aliveNeighbors > 3) return State.Dead;
            else if (state.Equals(State.Alive) && aliveNeighbors == 2 || aliveNeighbors == 3) return State.Alive;
            else if (state.Equals(State.Dead) && aliveNeighbors == 3) return State.Alive;
            return state;
        }

        public int GetAliveNeighboursForCell(int x, int y)
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
                            width = DimY - 1;
                        }
                        else if (width > DimY - 1)
                        {
                            width = 0;
                        }

                        if (height < 0)
                        {
                            height = DimX - 1;
                        }
                        else if (height > DimX - 1)
                        {
                            height = 0;
                        }

                        if (State.Alive.Equals(Field.Cells[height, width].State)) alive++;
                    }
                }
            }

            return alive;
        }

        public int CountLiveCells()
        {
            int liveCells = 0;
            for (int i = 0; i < DimY; i++)
            {
                for (int j = 0; j < DimX; j++)
                {
                    if (State.Alive.Equals(Field.Cells[j, i].State))
                    {
                        liveCells++;
                    }
                }
            }
            return liveCells;
        }
    }
}
