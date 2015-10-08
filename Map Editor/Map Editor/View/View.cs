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
    /// <summary>
    /// View, has all the graphics properties.
    /// </summary>
    public partial class View : Form
    {
        private Scene model;
        private PictureBox[][] pictureBoxes;
        private List<PictureBox> objectsPictureBox;
        private PictureBox draggedPictureBox;
        private PictureBox selectedModifiedPictureBox;
        private const int PICTURE_BOX_SIZE = 52;

        /// <summary>
        /// Initializes a new instance of the <see cref="View"/> class.
        /// </summary>
        public View()
        {
            objectsPictureBox = new List<PictureBox>();
            InitializeComponent();

            // Map editor toolbar
            InitializeTiles();
            InitializeObjects();

            new Controller(this);
        }

        /// <summary>
        /// Initializes the tiles.
        /// </summary>
        private void InitializeTiles()
        {
            picTileEmpty.ImageLocation = Tile.ToDescriptionString(Tile.TileType.Empty);
            picTileEmpty.Image = Image.FromFile(picTileEmpty.ImageLocation);
            picTileBad.ImageLocation = Tile.ToDescriptionString(Tile.TileType.Bad);
            picTileBad.Image = Image.FromFile(picTileBad.ImageLocation);
            picTileBreakPass.ImageLocation = Tile.ToDescriptionString(Tile.TileType.BreakPass);
            picTileBreakPass.Image = Image.FromFile(picTileBreakPass.ImageLocation);
            picTileDoor.ImageLocation = Tile.ToDescriptionString(Tile.TileType.Door);
            picTileDoor.Image = Image.FromFile(picTileDoor.ImageLocation);
            picTileFloor.ImageLocation = Tile.ToDescriptionString(Tile.TileType.Floor);
            picTileFloor.Image = Image.FromFile(picTileFloor.ImageLocation);
            picTileOneByOne.ImageLocation = Tile.ToDescriptionString(Tile.TileType.OneByOne);
            picTileOneByOne.Image = Image.FromFile(picTileOneByOne.ImageLocation);
            picTileSlow.ImageLocation = Tile.ToDescriptionString(Tile.TileType.Slow);
            picTileSlow.Image = Image.FromFile(picTileSlow.ImageLocation);
            picTileTeleport.ImageLocation = Tile.ToDescriptionString(Tile.TileType.Teleport);
            picTileTeleport.Image = Image.FromFile(picTileTeleport.ImageLocation);
            picTileTower.ImageLocation = Tile.ToDescriptionString(Tile.TileType.Tower);
            picTileTower.Image = Image.FromFile(picTileTower.ImageLocation);
            picTileWall.ImageLocation = Tile.ToDescriptionString(Tile.TileType.Wall);
            picTileWall.Image = Image.FromFile(picTileWall.ImageLocation);
            picTileSlope.ImageLocation = Tile.ToDescriptionString(Tile.TileType.Slope);
            picTileSlope.Image = Image.FromFile(picTileSlope.ImageLocation);
        }

        /// <summary>
        /// Initializes the objects.
        /// </summary>
        private void InitializeObjects()
        {
            // TRAPS
            picTrapDrop.ImageLocation = TileObject.ToDescriptionString(TileObject.TrapType.Drop);
            picTrapDrop.Image = Image.FromFile(picTrapDrop.ImageLocation);
            picTrapFire.ImageLocation = TileObject.ToDescriptionString(TileObject.TrapType.Fire);
            picTrapFire.Image = Image.FromFile(picTrapFire.ImageLocation);
            picTrapFreeze.ImageLocation = TileObject.ToDescriptionString(TileObject.TrapType.Freeze);
            picTrapFreeze.Image = Image.FromFile(picTrapFreeze.ImageLocation);
            picTrapSpike.ImageLocation = TileObject.ToDescriptionString(TileObject.TrapType.Spike);
            picTrapSpike.Image = Image.FromFile(picTrapSpike.ImageLocation);
            picTrapTurret.ImageLocation = TileObject.ToDescriptionString(TileObject.TrapType.Turret);
            picTrapTurret.Image = Image.FromFile(picTrapTurret.ImageLocation);

            // Bonus
            picBonusDash.ImageLocation = TileObject.ToDescriptionString(TileObject.BonusType.Dash);
            picBonusDash.Image = Image.FromFile(picBonusDash.ImageLocation);
            picBonusDecoy.ImageLocation = TileObject.ToDescriptionString(TileObject.BonusType.Decoy);
            picBonusDecoy.Image = Image.FromFile(picBonusDecoy.ImageLocation);
            picBonusDouble.ImageLocation = TileObject.ToDescriptionString(TileObject.BonusType.Double);
            picBonusDouble.Image = Image.FromFile(picBonusDouble.ImageLocation);
            picBonusEMP.ImageLocation = TileObject.ToDescriptionString(TileObject.BonusType.EMP);
            picBonusEMP.Image = Image.FromFile(picBonusEMP.ImageLocation);
            picBonusImplosion.ImageLocation = TileObject.ToDescriptionString(TileObject.BonusType.Implosion);
            picBonusImplosion.Image = Image.FromFile(picBonusImplosion.ImageLocation);
            picBonusMissile.ImageLocation = TileObject.ToDescriptionString(TileObject.BonusType.Missile);
            picBonusMissile.Image = Image.FromFile(picBonusMissile.ImageLocation);
            picBonusPower.ImageLocation = TileObject.ToDescriptionString(TileObject.BonusType.Power);
            picBonusPower.Image = Image.FromFile(picBonusPower.ImageLocation);
            picBonusRicochet.ImageLocation = TileObject.ToDescriptionString(TileObject.BonusType.Ricochet);
            picBonusRicochet.Image = Image.FromFile(picBonusRicochet.ImageLocation);
            picBonusShield.ImageLocation = TileObject.ToDescriptionString(TileObject.BonusType.Shield);
            picBonusShield.Image = Image.FromFile(picBonusShield.ImageLocation);
            picBonusSpeed.ImageLocation = TileObject.ToDescriptionString(TileObject.BonusType.Speed);
            picBonusSpeed.Image = Image.FromFile(picBonusSpeed.ImageLocation);

            // Utilities
            picUtilGoal.ImageLocation = TileObject.ToDescriptionString(TileObject.UtilType.Goal);
            picUtilGoal.Image = Image.FromFile(picUtilGoal.ImageLocation);
            picUtilJump.ImageLocation = TileObject.ToDescriptionString(TileObject.UtilType.Jump);
            picUtilJump.Image = Image.FromFile(picUtilJump.ImageLocation);
            picUtilSpawn.ImageLocation = TileObject.ToDescriptionString(TileObject.UtilType.Spawn);
            picUtilSpawn.Image = Image.FromFile(picUtilSpawn.ImageLocation);
            picUtilBall.ImageLocation = TileObject.ToDescriptionString(TileObject.UtilType.Ball);
            picUtilBall.Image = Image.FromFile(picUtilBall.ImageLocation);
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
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

                    PictureBox objectBox = new PictureBox();
                    objectBox.Parent = pictureBoxes[y][x];
                    objectBox.BackColor = Color.Transparent;
                    objectBox.Size = new System.Drawing.Size(32, 32);
                    objectBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    objectBox.Location = new Point((pictureBoxes[y][x].Size.Width - objectBox.Size.Width) / 2, (pictureBoxes[y][x].Size.Height - objectBox.Size.Height) / 2);
                    pictureBoxes[y][x].Controls.Add(objectBox);

                    pnlDraw.Controls.Add(pictureBoxes[y][x]);
                }
            }
            pnlDraw.AutoScroll = true;
        }

        /// <summary>
        /// Closes the scene.
        /// </summary>
        public void CloseScene()
        {
            model = null;
            for (int i = pnlDraw.Controls.Count - 1; i >= 0; i--)
            {
                pnlDraw.Controls.Remove(pnlDraw.Controls[i]);
            }
            for (int i = pnlGroupFloors.Controls.Count - 1; i >= 0; i--)
            {
                pnlGroupFloors.Controls.Remove(pnlGroupFloors.Controls[i]);
            }
        }

        /// <summary>
        /// Selects the picture box.
        /// </summary>
        /// <param name="pictureBox">The picture box.</param>
        private void SelectPictureBox(PictureBox pictureBox)
        {
            if (selectedModifiedPictureBox != null)
            {
                if (selectedModifiedPictureBox.Parent is PictureBox)
                {
                    (selectedModifiedPictureBox.Parent as PictureBox).BackColor = Color.Transparent;
                }
                else
                {
                    selectedModifiedPictureBox.BackColor = Color.Transparent;
                }
            }

            selectedModifiedPictureBox = pictureBox;
            if (selectedModifiedPictureBox.Parent is PictureBox)
            {
                (selectedModifiedPictureBox.Parent as PictureBox).BackColor = Color.DarkOrange;
            }
            else
            {
                selectedModifiedPictureBox.BackColor = Color.DarkOrange;
            }
        }

        /// <summary>
        /// Refreshes the object.
        /// </summary>
        /// <param name="sender">The sender.</param>
        private void RefreshObject(object sender)
        {
            if (sender is Tile)
            {
                Tile tile = (Tile)sender;
                if (pictureBoxes != null)
                {
                    pictureBoxes[tile.position.Y][tile.position.X].ImageLocation = Tile.ToDescriptionString(tile.Type);
                    pictureBoxes[tile.position.Y][tile.position.X].Image = Image.FromFile(pictureBoxes[tile.position.Y][tile.position.X].ImageLocation);

                    if (pictureBoxes[tile.position.Y][tile.position.X].HasChildren)
                    {
                        PictureBox objectPictureBox = (PictureBox)pictureBoxes[tile.position.Y][tile.position.X].Controls[0];
                        string path = tile.objectOnTile.ToString();
                        objectPictureBox.ImageLocation = path;
                        if (path != "")
                        {
                            objectPictureBox.Image = Image.FromFile(path);
                        }
                        else
                        {
                            objectPictureBox.Image = null;
                        }
                    }
                }
            }
        }
    }
}
