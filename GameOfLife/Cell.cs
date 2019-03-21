using System.Collections.Generic;

namespace GameOfLife
{
    public class Cell
    {
        public int X;
        public int Y;

        public List<Cell> Neighbours = new List<Cell>(8);

        public bool IsAlive;

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            IsAlive = false;
        }
    }
}
