using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        int Count = 30;
        int ButtonCellWidth = 20;
        private List<CellButton> AliveCells;
        private List<CellButton> DeadCells;
        private List<CellButton> NewCells;
        private CellButton[,] Plane;

        public Form1()
        {
            InitializeComponent();

            Plane = new CellButton[Count, Count];
            AliveCells = new List<CellButton>(Count * Count);
            DeadCells = new List<CellButton>(Count * Count);
            NewCells = new List<CellButton>(Count * Count);
            panel1.Size = new Size(Count * ButtonCellWidth, Count * ButtonCellWidth);

            for (int j = 0; j < Count; j++)
            {
                for (int i = 0; i < Count; i++)
                {
                    int x = i * ButtonCellWidth;
                    int y = j * ButtonCellWidth;
                    string id = i.ToString() + j.ToString();

                    CellButton cellbutton = new CellButton(y, x, false, id, ButtonCellWidth);
                    Plane[j, i] = cellbutton;

                    cellbutton.MouseClick += new MouseEventHandler(CellButton_Click);
                    panel1.Controls.Add(cellbutton);
                }
            }
            FindNeighbours();
        }

        public void FindNeighbours()
        {
            for (int j = 0; j < Count; j++)
            {
                for (int i = 0; i < Count; i++)
                {
                    int m = i;
                    int n = j;
                    int m_incr = m + 1;
                    int m_decr = m - 1;
                    int n_incr = n + 1;
                    int n_decr = n - 1;

                    if (m_decr < 0)
                    {
                        m_decr = Count - 1;
                    }

                    if (m_incr >= Count)
                    {
                        m_incr = 0;
                    }

                    if (n_decr < 0)
                    {
                        n_decr = Count - 1;
                    }

                    if (n_incr >= Count)
                    {
                        n_incr = 0;
                    }

                    CellButton cell = Plane[j, i];

                    cell.Neighbours.Add(Plane[n_decr, m_decr]);
                    cell.Neighbours.Add(Plane[n_decr, m]);
                    cell.Neighbours.Add(Plane[n_decr, m_incr]);
                    cell.Neighbours.Add(Plane[n, m_decr]);
                    cell.Neighbours.Add(Plane[n, m_incr]);
                    cell.Neighbours.Add(Plane[n_incr, m_decr]);
                    cell.Neighbours.Add(Plane[n_incr, m]);
                    cell.Neighbours.Add(Plane[n_incr, m_incr]);
                }
            }
        }

        private void CellButton_Click(object sender, MouseEventArgs e)
        {
            CellButton cellbutton = (sender as CellButton);
            if (cellbutton.IsAlive)
            {
                cellbutton.IsAlive = false;
                cellbutton.ColorUpdate();
                AliveCells.RemoveAll(x => x.ID == cellbutton.ID);
            }
            else
            {
                cellbutton.IsAlive = true;
                cellbutton.ColorUpdate();
                AliveCells.Add(cellbutton);
            }
        }

        private void FindDeadCells()
        {
            foreach (CellButton cellbutton in AliveCells)
            {
                int count = cellbutton.Neighbours.Count(cell => cell.IsAlive);

                if (count > 3 || count < 2)
                {
                    DeadCells.Add(cellbutton);
                }
            }
        }

        private void AddNewCells()
        {
            foreach (CellButton cellbutton in NewCells)
            {
                cellbutton.IsAlive = true;
                cellbutton.ColorUpdate();
            }
            AliveCells.AddRange(NewCells);
            NewCells.Clear();
        }

        private void DeleteDeadCells()
        {
            foreach (CellButton cellbutton in DeadCells)
            {
                cellbutton.IsAlive = false;
                AliveCells.RemoveAll(x => x.ID == cellbutton.ID);
                cellbutton.ColorUpdate();
                cellbutton.Update();
            }
            DeadCells.Clear();
        }

        private void FindNewCells()
        {
            int count = 0;
            foreach (CellButton cellbutton in AliveCells)
            {
                foreach (CellButton cell in cellbutton.Neighbours)
                {
                    if (!cell.IsAlive)
                    {
                        count = cell.Neighbours.Count(el => el.IsAlive);

                        if (count == 3)
                        {
                            if (!NewCells.Contains(cell))
                            {
                                NewCells.Add(cell);
                            }
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Stop();

            FindDeadCells();

            FindNewCells();

            AddNewCells();

            DeleteDeadCells();

            //timer1.Start();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button_OneStep_Click(object sender, EventArgs e)
        {
            FindDeadCells();

            FindNewCells();

            AddNewCells();

            DeleteDeadCells();
        }

        private void button_Timer_Stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}

