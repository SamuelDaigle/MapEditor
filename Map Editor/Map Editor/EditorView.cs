using Map_Editor.GameData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_Editor
{
    public partial class Editor : Form
    {
        private Scene scene;
        private PictureBox[][] pictureBoxes;
        private const int PICTURE_BOX_SIZE = 52;

        #region TILES_PATH
        private const string PATH_TILE_EMPTY = "../../Resources/Tile/Empty.png";
        private const string PATH_TILE_BAD = "../../Resources/Tile/Bad.png";
        private const string PATH_TILE_BREAKING = "../../Resources/Tile/Breaking.png";
        private const string PATH_TILE_DOOR = "../../Resources/Tile/Door.png";
        private const string PATH_TILE_FLOOR = "../../Resources/Tile/Floor.png";
        private const string PATH_TILE_ONE_BY_ONE = "../../Resources/Tile/OneByOne.png";
        private const string PATH_TILE_SLOW = "../../Resources/Tile/Slow.png";
        private const string PATH_TILE_TELEPORT = "../../Resources/Tile/Teleport.png";
        private const string PATH_TILE_TOWER = "../../Resources/Tile/Tower.png";
        private const string PATH_TILE_WALL = "../../Resources/Tile/Wall.png";
        private const string PATH_TILE_SLOPE = "../../Resources/Tile/Slope.png";
        #endregion

        public Editor()
        {
            InitializeComponent();
            InitializeControls();
            picTileEmpty.Image = Image.FromFile(PATH_TILE_EMPTY);
            picTileEmpty.ImageLocation = PATH_TILE_EMPTY;
            picTileBad.Image = Image.FromFile(PATH_TILE_BAD);
            picTileBad.ImageLocation = PATH_TILE_BAD;
            picTileBreakPass.Image = Image.FromFile(PATH_TILE_BREAKING);
            picTileBreakPass.ImageLocation = PATH_TILE_BREAKING;
            picTileDoor.Image = Image.FromFile(PATH_TILE_DOOR);
            picTileDoor.ImageLocation = PATH_TILE_DOOR;
            picTileFloor.Image = Image.FromFile(PATH_TILE_FLOOR);
            picTileFloor.ImageLocation = PATH_TILE_FLOOR;
            picTileOneByOne.Image = Image.FromFile(PATH_TILE_ONE_BY_ONE);
            picTileOneByOne.ImageLocation = PATH_TILE_ONE_BY_ONE;
            picTileSlow.Image = Image.FromFile(PATH_TILE_SLOW);
            picTileSlow.ImageLocation = PATH_TILE_SLOW;
            picTileTeleport.Image = Image.FromFile(PATH_TILE_TELEPORT);
            picTileTeleport.ImageLocation = PATH_TILE_TELEPORT;
            picTileTower.Image = Image.FromFile(PATH_TILE_TOWER);
            picTileTower.ImageLocation = PATH_TILE_TOWER;
            picTileWall.Image = Image.FromFile(PATH_TILE_WALL);
            picTileWall.ImageLocation = PATH_TILE_WALL;
            picTileSlope.Image = Image.FromFile(PATH_TILE_SLOPE);
            picTileSlope.ImageLocation = PATH_TILE_SLOPE;
        }
        
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
                    pictureBoxes[y][x].BackgroundImageLayout = ImageLayout.Center;
                    pictureBoxes[y][x].MouseMove += picModify_Move;
                    pictureBoxes[y][x].MouseDown += picModify_Down;
                    pictureBoxes[y][x].MouseUp += picModify_Up;
                    pnlDraw.Controls.Add(pictureBoxes[y][x]);
                }
            }
            pnlDraw.AutoScroll = true;
        }

        // Received the modified tile.
        private void OnSceneChanged(object sender, EventArgs e)
        {
            Tile tile = (Tile)sender;
            pictureBoxes[tile.y][tile.x].Image = GetImage(tile);
        }

        private Image GetImage(Tile _tile)
        {
            string path;
            switch (_tile.Type)
            {
                case Tile.TileType.Bad:
                    path = PATH_TILE_BAD;
                    break;

                case Tile.TileType.BreakPass:
                    path = PATH_TILE_BREAKING;
                    break;

                case Tile.TileType.Door:
                    path = PATH_TILE_DOOR;
                    break;

                case Tile.TileType.Empty:
                    path = PATH_TILE_EMPTY;
                    break;

                case Tile.TileType.Floor:
                    path = PATH_TILE_FLOOR;
                    break;

                case Tile.TileType.OneByOne:
                    path = PATH_TILE_ONE_BY_ONE;
                    break;

                case Tile.TileType.Slope:
                    path = PATH_TILE_SLOPE;
                    break;

                case Tile.TileType.Slow:
                    path = PATH_TILE_SLOW;
                    break;

                case Tile.TileType.Teleport:
                    path = PATH_TILE_TELEPORT;
                    break;

                case Tile.TileType.Tower:
                    path = PATH_TILE_TOWER;
                    break;

                case Tile.TileType.Wall:
                    path = PATH_TILE_WALL;
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
