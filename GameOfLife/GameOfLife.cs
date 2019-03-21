using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    class GameOfLife
    {
        private int Count;
        public Cell[,] Plane;
        private List<Cell> AliveCells;
        private List<Cell> DeadCells;
        private List<Cell> NewCells;

        public GameOfLife(int count)
        {
            Count = count;
            Plane = new Cell[Count, Count];
            AliveCells = new List<Cell>(Count * Count);
            DeadCells = new List<Cell>(Count * Count);
            NewCells = new List<Cell>(Count * Count);

            for (int j = 0; j < Count; j++)
            {
                for (int i = 0; i < Count; i++)
                {
                    Cell Cell = new Cell(i, j);
                    Plane[i, j] = Cell;
                }
            }

            for (int y = 0; y < Count; y++)
            {
                for (int x = 0; x < Count; x++)
                {
                    int m = x;
                    int n = y;
                    int m_incr = m + 1;
                    int m_decr = m - 1;
                    int n_incr = n + 1;
                    int n_decr = n - 1;

                    if (m_decr < 0) m_decr = Count - 1;

                    if (m_incr >= Count) m_incr = 0;

                    if (n_decr < 0) n_decr = Count - 1;

                    if (n_incr >= Count) n_incr = 0;

                    Cell Cell = Plane[x, y];

                    Cell.Neighbours.Add(Plane[m_decr, n_decr]);
                    Cell.Neighbours.Add(Plane[m_decr, n]);
                    Cell.Neighbours.Add(Plane[m_decr, n_incr]);
                    Cell.Neighbours.Add(Plane[m, n_decr]);
                    Cell.Neighbours.Add(Plane[m, n_incr]);
                    Cell.Neighbours.Add(Plane[m_incr, n_decr]);
                    Cell.Neighbours.Add(Plane[m_incr, n]);
                    Cell.Neighbours.Add(Plane[m_incr, n_incr]);
                }
            }
        }

        private void FindDeadCells()
        {
            foreach (Cell cell in AliveCells)
            {
                int count = cell.Neighbours.Count(el => el.IsAlive);

                if (count > 3 || count < 2)
                {
                    DeadCells.Add(cell);
                }             
            }
        }

        private void FindNewCells()
        {
            foreach (Cell cell in AliveCells)
            {
                foreach (Cell neigbour in cell.Neighbours)
                {
                    if (!neigbour.IsAlive)
                    {
                        int count = neigbour.Neighbours.Count(el => el.IsAlive);

                        if (count == 3)
                        {
                            if (!NewCells.Contains(neigbour))
                            {
                                NewCells.Add(neigbour);
                            }
                        }
                    }
                }
            }
        }

        public void Plane_Update()
        {
            NewCells.ForEach(el => el.IsAlive = true);
            AliveCells.AddRange(NewCells);
            NewCells.Clear();

            DeadCells.ForEach(el => el.IsAlive = false);
            AliveCells.RemoveAll(el => DeadCells.Contains(el));
            DeadCells.Clear();
        }

        public void Cell_Add(int x, int y)
        {
            Cell cell = Plane[x, y];
            cell.IsAlive = true;
            AliveCells.Add(cell);
        }

        public void Cell_Remove(int x, int y)
        {
            Cell cell = Plane[x, y];
            cell.IsAlive = false;
            AliveCells.Remove(cell);
        }

        public List<Cell> OneLifeStep()
        {
            FindDeadCells();
            FindNewCells();
            Plane_Update();

            return AliveCells;
        }
    }
}

