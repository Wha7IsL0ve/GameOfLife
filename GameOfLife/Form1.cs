using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        public int Count = 30;
        private int ButtonCellWidth = 20;
        CellButton[,] PlaneButtons;
        GameOfLife GameOfLife;
        List<CellButton> AliveCellsButtons;

        public Form1()
        {
            InitializeComponent();

            GameOfLife = new GameOfLife(Count);

            PlaneButtons = new CellButton[Count, Count];
            AliveCellsButtons = new List<CellButton>(Count);

            panel1.Size = new Size(Count * ButtonCellWidth, Count * ButtonCellWidth);

            for (int y = 0; y < Count; y++)
            {
                for (int x = 0; x < Count; x++)
                {
                    string id = x.ToString() + y.ToString();

                    CellButton cellbutton = new CellButton(x, y, id, ButtonCellWidth);
                    PlaneButtons[x, y] = cellbutton;

                    cellbutton.MouseClick += new MouseEventHandler(CellButton_Click);
                    panel1.Controls.Add(cellbutton);                   
                }
            }
        }

        public void OneStep()
        {
            AliveCellsButtons.ForEach(cellbutton => cellbutton.ColorChange());
            AliveCellsButtons.Clear();
            List<Cell> AliveCells = GameOfLife.OneLifeStep();

            foreach (Cell cell in AliveCells)
            {
                AliveCellsButtons.Add(PlaneButtons[cell.X, cell.Y]);
            }
            AliveCellsButtons.ForEach(cellbutton => cellbutton.ColorChange());
        }


        private void CellButton_Click(object sender, MouseEventArgs e)
        {
            CellButton cellbutton = (sender as CellButton);

            if (AliveCellsButtons.Contains(cellbutton))
            {
                GameOfLife.Cell_Remove(cellbutton.X, cellbutton.Y);
                AliveCellsButtons.Remove(cellbutton);
            }
            else
            {
                GameOfLife.Cell_Add(cellbutton.X, cellbutton.Y);
                AliveCellsButtons.Add(cellbutton);
            }

            cellbutton.ColorChange();                       
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OneStep();
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button_OneStep_Click(object sender, EventArgs e)
        {
            OneStep();
        }

        private void button_Timer_Stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}