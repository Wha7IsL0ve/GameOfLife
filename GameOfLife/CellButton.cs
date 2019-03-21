using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace GameOfLife
{
    class CellButton : Button
    {
        public int X;
        public int Y;

        private Color LiveColor = Color.White;
        private Color DeadColor = Color.Gray;

        public string ID;

        public List<CellButton> Neighbours = new List<CellButton>(8);

        public CellButton(int x, int y, string id, int Width)
        {
            X = x;
            Y = y;
            Location = new Point(X * Width, Y * Width);

            ForeColor = Color.White;
            BackColor = DeadColor;
            Size = new Size(Width, Width);
            FlatStyle = FlatStyle.Flat;
            ID = id;
        }

        public void ColorChange()
        {
            BackColor = BackColor == LiveColor ? DeadColor : LiveColor;
        }
    }
}
