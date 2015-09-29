using Map_Editor.GameData;
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
        private Scene scene;

        public Scene Scene
        {
            get
            {
                return scene;
            }
            set
            {
                if (scene != null)
                {
                    scene.SceneChanged -= OnSceneChanged;
                }

                scene = value;

                if (scene != null)
                {
                    scene.SceneChanged += OnSceneChanged;
                }
            }
        }

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


        // Received the modified tile.
        private void OnSceneChanged(object sender, EventArgs e)
        {
            Tile tile = (Tile)sender;
            pictureBoxes[tile.y][tile.x].BackgroundImage = GetImage(tile);
        }

        private Image GetImage(Tile _tile)
        {
            string path;
            switch (_tile.Type)
            {
                case Tile.TileType.Bad:
                    path = "lolol";
                    break;

                case Tile.TileType.BreakPass:
                    path = "lolol";
                    break;

                case Tile.TileType.Door:
                    path = "lolol";
                    break;

                case Tile.TileType.Empty:
                    path = "..\\..\\Resources\\brick.png";
                    break;

                case Tile.TileType.Floor:
                    path = "lolol";
                    break;

                case Tile.TileType.OneByOne:
                    path = "lolol";
                    break;

                case Tile.TileType.Slope:
                    path = "lolol";
                    break;

                case Tile.TileType.Slow:
                    path = "lolol";
                    break;

                case Tile.TileType.Teleport:
                    path = "lolol";
                    break;

                case Tile.TileType.Tower:
                    path = "lolol";
                    break;

                case Tile.TileType.Wall:
                    path = "lolol";
                    break;

                default:
                    path = "pas lololol";
                    break;
            }
            return Image.FromFile(path);
        }

        private Image GetImage(Bonus _bonus)
        {
            string path;
            switch (_bonus.type)
            {
                case Bonus.BonusType.Dash:
                    path = "lolol";
                    break;

                case Bonus.BonusType.Decoy:
                    path = "lolol";
                    break;

                case Bonus.BonusType.Double:
                    path = "lolol";
                    break;

                case Bonus.BonusType.EMP:
                    path = "lolol";
                    break;

                case Bonus.BonusType.Implosion:
                    path = "lolol";
                    break;

                case Bonus.BonusType.Missile:
                    path = "lolol";
                    break;

                case Bonus.BonusType.Power:
                    path = "lolol";
                    break;

                case Bonus.BonusType.Ricochet:
                    path = "lolol";
                    break;

                case Bonus.BonusType.Shield:
                    path = "lolol";
                    break;

                case Bonus.BonusType.Speed:
                    path = "lolol";
                    break;

                default:
                    path = "pas lololol";
                    break;
            }
            return Image.FromFile(path);
        }

        private Image GetImage(Trap _trap)
        {
            string path;
            switch (_trap.type)
            {
                case Trap.TrapType.Drop:
                    path = "lolol";
                    break;

                case Trap.TrapType.Fire:
                    path = "lolol";
                    break;

                case Trap.TrapType.Freeze:
                    path = "lolol";
                    break;

                case Trap.TrapType.Spike:
                    path = "lolol";
                    break;

                case Trap.TrapType.Turret:
                    path = "lolol";
                    break;

                default:
                    path = "pas lololol";
                    break;
            }
            return Image.FromFile(path);
        }

        private Image GetImage(Utilities _util)
        {
            string path;
            switch (_util.type)
            {
                case Utilities.UtilType.Goal:
                    path = "lolol";
                    break;

                case Utilities.UtilType.Jump:
                    path = "lolol";
                    break;

                case Utilities.UtilType.Spawn:
                    path = "lolol";
                    break;

                case Utilities.UtilType.Trigger:
                    path = "lolol";
                    break;

                default:
                    path = "pas lololol";
                    break;
            }
            return Image.FromFile(path);
        }
    }
}
