using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_Editor
{
    public partial class Editor
    {
        private PictureBox[][] pictureBoxes;
        private const int PICTURE_BOX_SIZE = 50;

        public void InitializeView(int width, int height)
        {
            pictureBoxes = new PictureBox[height][];
            for (int y = 0; y < height; y++)
            {
                pictureBoxes[y] = new PictureBox[width];
                for (int x = 0; x < width; x++)
                {
                    pictureBoxes[y][x] = new PictureBox();
                    pictureBoxes[y][x].SetBounds(x * PICTURE_BOX_SIZE, y * PICTURE_BOX_SIZE, PICTURE_BOX_SIZE, PICTURE_BOX_SIZE);
                    pictureBoxes[y][x].BackgroundImage = Image.FromFile("..\\..\\Resources\\brick.png");
                    pictureBoxes[y][x].BorderStyle = BorderStyle.FixedSingle;
                    pictureBoxes[y][x].BackColor = System.Drawing.Color.Red;
                    pictureBoxes[y][x].Padding = new System.Windows.Forms.Padding(5);
                    pictureBoxes[y][x].MouseHover += OnHover;
                    pnlDraw.Controls.Add(pictureBoxes[y][x]);
                }
            }
        }

        public void OnHover(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            
        }
    }
}
