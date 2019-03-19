using System;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace GameOfLife
{
    class CellButton : Button
    {
        public bool IsAlive = false;

        public string ID;

        public List<CellButton> Neighbours = new List<CellButton>(8);

        public CellButton(int x, int y, bool IsAlive, string id, int Width)
        {
            Location = new Point(x, y);
            ForeColor = Color.White;
            BackColor = Color.Gray;
            Size = new Size(Width, Width);
            FlatStyle = FlatStyle.Flat;
            IsAlive = false;
            ID = id;
        }

        public void ColorUpdate()
        {
            if (IsAlive)
            {
                BackColor = Color.White;
            }
            else
            {
                BackColor = Color.Gray;
            }
        }
    }
}
