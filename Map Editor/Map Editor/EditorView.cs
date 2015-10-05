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
        private List<PictureBox> objectsPictureBox;
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
            objectsPictureBox = new List<PictureBox>();
            InitializeComponent();
            InitializeControls();
            InitializeTiles();
            InitializeObjects();
        }

        private void InitializeTiles()
        {
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

        private void InitializeObjects()
        {
            // TRAPS
            picTrapDrop.ImageLocation = GameObject.ToDescriptionString(GameObject.TrapType.Drop);
            picTrapDrop.Image = Image.FromFile(picTrapDrop.ImageLocation);
            picTrapFire.ImageLocation = GameObject.ToDescriptionString(GameObject.TrapType.Fire);
            picTrapFire.Image = Image.FromFile(picTrapFire.ImageLocation);
            picTrapFreeze.ImageLocation = GameObject.ToDescriptionString(GameObject.TrapType.Freeze);
            picTrapFreeze.Image = Image.FromFile(picTrapFreeze.ImageLocation);
            picTrapSpike.ImageLocation = GameObject.ToDescriptionString(GameObject.TrapType.Spike);
            picTrapSpike.Image = Image.FromFile(picTrapSpike.ImageLocation);
            picTrapTurret.ImageLocation = GameObject.ToDescriptionString(GameObject.TrapType.Turret);
            picTrapTurret.Image = Image.FromFile(picTrapTurret.ImageLocation);

            // Bonus
            picBonusDash.ImageLocation = GameObject.ToDescriptionString(GameObject.BonusType.Dash);
            picBonusDash.Image = Image.FromFile(picBonusDash.ImageLocation);
            picBonusDecoy.ImageLocation = GameObject.ToDescriptionString(GameObject.BonusType.Decoy);
            picBonusDecoy.Image = Image.FromFile(picBonusDecoy.ImageLocation);
            picBonusDouble.ImageLocation = GameObject.ToDescriptionString(GameObject.BonusType.Double);
            picBonusDouble.Image = Image.FromFile(picBonusDouble.ImageLocation);
            picBonusEMP.ImageLocation = GameObject.ToDescriptionString(GameObject.BonusType.EMP);
            picBonusEMP.Image = Image.FromFile(picBonusEMP.ImageLocation);
            picBonusImplosion.ImageLocation = GameObject.ToDescriptionString(GameObject.BonusType.Implosion);
            picBonusImplosion.Image = Image.FromFile(picBonusImplosion.ImageLocation);
            picBonusMissile.ImageLocation = GameObject.ToDescriptionString(GameObject.BonusType.Missile);
            picBonusMissile.Image = Image.FromFile(picBonusMissile.ImageLocation);
            picBonusPower.ImageLocation = GameObject.ToDescriptionString(GameObject.BonusType.Power);
            picBonusPower.Image = Image.FromFile(picBonusPower.ImageLocation);
            picBonusRicochet.ImageLocation = GameObject.ToDescriptionString(GameObject.BonusType.Ricochet);
            picBonusRicochet.Image = Image.FromFile(picBonusRicochet.ImageLocation);
            picBonusShield.ImageLocation = GameObject.ToDescriptionString(GameObject.BonusType.Shield);
            picBonusShield.Image = Image.FromFile(picBonusShield.ImageLocation);
            picBonusSpeed.ImageLocation = GameObject.ToDescriptionString(GameObject.BonusType.Speed);
            picBonusSpeed.Image = Image.FromFile(picBonusSpeed.ImageLocation);

            // Utilities
            picUtilGoal.ImageLocation = GameObject.ToDescriptionString(GameObject.UtilType.Goal);
            picUtilGoal.Image = Image.FromFile(picUtilGoal.ImageLocation);
            picUtilJump.ImageLocation = GameObject.ToDescriptionString(GameObject.UtilType.Jump);
            picUtilJump.Image = Image.FromFile(picUtilJump.ImageLocation);
            picUtilSpawn.ImageLocation = GameObject.ToDescriptionString(GameObject.UtilType.Spawn);
            picUtilSpawn.Image = Image.FromFile(picUtilSpawn.ImageLocation);
            picUtilBall.ImageLocation = GameObject.ToDescriptionString(GameObject.UtilType.Ball);
            picUtilBall.Image = Image.FromFile(picUtilBall.ImageLocation);
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
            draggedPictureBox = new PictureBox();
            draggedPictureBox.Size = new System.Drawing.Size(PICTURE_BOX_SIZE, PICTURE_BOX_SIZE);
            draggedPictureBox.BackColor = Color.Transparent;
            draggedPictureBox.Visible = false;
            pnlDraw.Controls.Add(draggedPictureBox);
            pictureBoxes = new PictureBox[height][];
            for (int y = 0; y < height; y++)
            {
                pictureBoxes[y] = new PictureBox[width];
                for (int x = 0; x < width; x++)
                {
                    pictureBoxes[y][x] = new PictureBox();
                    pictureBoxes[y][x].SetBounds(x * PICTURE_BOX_SIZE, y * PICTURE_BOX_SIZE, PICTURE_BOX_SIZE, PICTURE_BOX_SIZE);
                    pictureBoxes[y][x].BackgroundImageLayout = ImageLayout.Center;
                    pictureBoxes[y][x].SizeMode = PictureBoxSizeMode.CenterImage;
                    pictureBoxes[y][x].MouseMove += picModify_Move;
                    pictureBoxes[y][x].MouseDown += picModify_Down;
                    pictureBoxes[y][x].MouseUp += picModify_Up;
                    pictureBoxes[y][x].BackColor = Color.Transparent;

                    PictureBox objectBox = new PictureBox();
                    objectBox.Parent = pictureBoxes[y][x];
                    objectBox.BackColor = Color.Transparent;
                    objectBox.MouseMove += picModify_Move;
                    objectBox.MouseDown += picModify_Down;
                    objectBox.MouseUp += picModify_Up;
                    objectBox.Size = new System.Drawing.Size(32, 32);
                    objectBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    objectBox.Location = new Point((pictureBoxes[y][x].Size.Width - objectBox.Size.Width) / 2, (pictureBoxes[y][x].Size.Height - objectBox.Size.Height) / 2);
                    pictureBoxes[y][x].Controls.Add(objectBox);

                    pnlDraw.Controls.Add(pictureBoxes[y][x]);
                }
            }
            pnlDraw.AutoScroll = true;
        }

        public void CloseScene()
        {
            scene = null;
            for (int i = pnlDraw.Controls.Count - 1; i >= 0; i--)
            {
                pnlDraw.Controls.Remove(pnlDraw.Controls[i]);
            }
        }

        private void UpdatePictureBoxes()
        {
            Tile[] result = Scene.selectedTerrain.Tiles;
            for (int y = 0; y < floorHeight; y++)
            {
                for (int x = 0; x < floorWidth; x++)
                {
                    pictureBoxes[y][x].ImageLocation = GetImagePath(result[y * floorWidth + x]);
                    pictureBoxes[y][x].Image = Image.FromFile(pictureBoxes[y][x].ImageLocation);
                }
            }
        }

        private void ViewTop()
        {
            for (int y = 0; y < floorHeight; y++)
            {
                for (int x = 0; x < floorWidth; x++)
                {
                    Tile tile = scene.GetTopTile(x, y);
                    pictureBoxes[y][x].ImageLocation = GetImagePath(tile);
                    pictureBoxes[y][x].Image = Image.FromFile(pictureBoxes[y][x].ImageLocation);
                }
            }
        }

        // Received the modified tile.
        private void OnSceneChanged(object sender, EventArgs e)
        {
            if (sender is Tile)
            {
                Tile tile = (Tile)sender;
                pictureBoxes[tile.position.Y][tile.position.X].ImageLocation = GetImagePath(tile);
                pictureBoxes[tile.position.Y][tile.position.X].Image = Image.FromFile(pictureBoxes[tile.position.Y][tile.position.X].ImageLocation);
               // Lololololol pictureBoxes[tile.position.y][tile.position.x].Image.RotateFlip(RotateFlipType.Rotate90FlipNone);

                if (pictureBoxes[tile.position.Y][tile.position.X].HasChildren)
                {
                    PictureBox objectPictureBox = (PictureBox)pictureBoxes[tile.position.Y][tile.position.X].Controls[0];
                    string path = tile.objectOnTile.ToString();
                    if (path != "")
                    {
                        objectPictureBox.ImageLocation = path;
                        objectPictureBox.Image = Image.FromFile(path);
                    }
                }
            }
        }

        private string GetImagePath(Tile _tile)
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
            return path;
        }

        private void btnDeleteTile_Click(object sender, EventArgs e)
        {
            selectedTile.Type = Tile.TileType.Empty;
        }

        private void btnDeleteObject_Click(object sender, EventArgs e)
        {
            selectedTile.objectOnTile.utilType = GameObject.UtilType.None;
            selectedTile.objectOnTile.bonusType = GameObject.BonusType.None;
            selectedTile.objectOnTile.trapType = GameObject.TrapType.None;
        }


    }
}
