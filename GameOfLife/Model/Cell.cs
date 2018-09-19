using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class Cell
    {
        public State State { get; set; }
        public int AliveNeighbors { get; set; }
    }
}
