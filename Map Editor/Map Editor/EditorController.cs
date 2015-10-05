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
        private PictureBox selectedTilePictureBox;
        private PictureBox selectedObjectPictureBox;
        private PictureBox draggedPictureBox;
        private Tile selectedTile;
        private bool isMouseDown = false;

        private int floorWidth;
        private int floorHeight;

        public void InitializeControls()
        {
            newSceneToolStripMenuItem.Click += new System.EventHandler(this.newSceneToolStripMenuItem_Click);
            picTileBreakPass.Click += new System.EventHandler(this.picTile_Click);
            picTileOneByOne.Click += new System.EventHandler(this.picTile_Click);
            picTileSlow.Click += new System.EventHandler(this.picTile_Click);
            picTileDoor.Click += new System.EventHandler(this.picTile_Click);
            picTileWall.Click += new System.EventHandler(this.picTile_Click);
            picTileEmpty.Click += new System.EventHandler(this.picTile_Click);
            picTileFloor.Click += new System.EventHandler(this.picTile_Click);
            picTileSlope.Click += new System.EventHandler(this.picTile_Click);
            picTileTeleport.Click += new System.EventHandler(this.picTile_Click);
            picTileTower.Click += new System.EventHandler(this.picTile_Click);
            picTileBad.Click += new System.EventHandler(this.picTile_Click);
            // TRAPS
            picTrapDrop.Click += new System.EventHandler(this.picObject_Click);
            picTrapFire.Click += new System.EventHandler(this.picObject_Click);
            picTrapFreeze.Click += new System.EventHandler(this.picObject_Click);
            picTrapSpike.Click += new System.EventHandler(this.picObject_Click);
            picTrapTurret.Click += new System.EventHandler(this.picObject_Click);

            // Bonus
            picBonusDash.Click += new System.EventHandler(this.picObject_Click);
            picBonusDecoy.Click += new System.EventHandler(this.picObject_Click);
            picBonusDouble.Click += new System.EventHandler(this.picObject_Click);
            picBonusEMP.Click += new System.EventHandler(this.picObject_Click);
            picBonusImplosion.Click += new System.EventHandler(this.picObject_Click);
            picBonusMissile.Click += new System.EventHandler(this.picObject_Click);
            picBonusPower.Click += new System.EventHandler(this.picObject_Click);
            picBonusRicochet.Click += new System.EventHandler(this.picObject_Click);
            picBonusShield.Click += new System.EventHandler(this.picObject_Click);
            picBonusSpeed.Click += new System.EventHandler(this.picObject_Click);

            // Utilities
            picUtilGoal.Click += new System.EventHandler(this.picObject_Click);
            picUtilJump.Click += new System.EventHandler(this.picObject_Click);
            picUtilSpawn.Click += new System.EventHandler(this.picObject_Click);
            picUtilBall.Click += new System.EventHandler(this.picObject_Click);

            selectedTilePictureBox = picTileEmpty;
            picTile_Click(selectedTilePictureBox, EventArgs.Empty);

        }

        private void newSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SceneCreation form = new SceneCreation())
            {
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    int terrainWidth = form.TerrainWidth;
                    int terrainHeight = form.TerrainHeight;
                    Tile.TileType type = form.TileType;

                    InitializeView(terrainWidth, terrainHeight);

                    Scene = new Scene();
                    Scene.name = form.SceneName;

                    floorWidth = terrainWidth;
                    floorHeight = terrainHeight;
                    btnAdd_Click(btnAdd, EventArgs.Empty);
                    SetAllTiles(type);

                    Scene.SetEvents();
                }
            }
        }

        private void SetAllTiles(Tile.TileType _type)
        {
            for (int y = 0; y < scene.selectedTerrain.height; y++)
            {
                for (int x = 0; x < scene.selectedTerrain.width; x++)
                {
                    scene.selectedTerrain.GetTile(x, y).Type = _type;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Floor floor = new Floor();
            floor.Initialize(floorWidth, floorHeight, Tile.TileType.Empty);
            Scene.floors.Add(floor);


            Control downButton = pnlGroupFloors.Controls[pnlGroupFloors.Controls.Count - 1];
            pnlGroupFloors.Controls.RemoveAt(pnlGroupFloors.Controls.Count - 1);

            Button btnFloor = new Button();
            btnFloor.Text = Scene.floors.Count.ToString();
            btnFloor.Size = new Size(btnUp.Size.Width, btnUp.Size.Height);
            btnFloor.Click += selectFloor_Click;
            pnlGroupFloors.Controls.Add(btnFloor);

            pnlGroupFloors.Controls.Add(downButton);
            selectFloor_Click(btnFloor, EventArgs.Empty);

            if (Scene.floors.Count >= 5)
            {
                btnAdd.Enabled = false;
            }
        }

        private void btnTopView_Click(object sender, EventArgs e)
        {
            ViewTop();
        }

        private void selectFloor_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int floorID = Convert.ToInt32(button.Text);

            // Remove handles from the old floor
            Scene.UnsetEvents();
            Scene.selectedTerrain.UnsetEvents();

            Scene.selectedTerrain = Scene.floors[floorID - 1];
            // Add handles to the new floor.
            Scene.SetEvents();
            Scene.selectedTerrain.SetEvents();

            UpdatePictureBoxes();
        }

        private void picTile_Click(object sender, EventArgs e)
        {
            selectedObjectPictureBox = null;
            if (selectedTilePictureBox != null)
            {
                selectedTilePictureBox.BackColor = Color.Transparent;
            }
            selectedTilePictureBox = (PictureBox)sender;
            selectedTilePictureBox.BackColor = Color.Yellow;
        }

        private void picObject_Click(object sender, EventArgs e)
        {
            selectedTilePictureBox = null;
            if (selectedObjectPictureBox != null)
            {
                selectedObjectPictureBox.BackColor = Color.Transparent;
            }
            selectedObjectPictureBox = (PictureBox)sender;
            selectedObjectPictureBox.BackColor = Color.Yellow;
        }

        public void picModify_Move(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                PictureBox pictureBox = (PictureBox)sender;
                Point position = pnlDraw.PointToClient(Cursor.Position);
                int pictureBoxX = (position.X - pnlDraw.AutoScrollPosition.X) / PICTURE_BOX_SIZE;
                int pictureBoxY = (position.Y - pnlDraw.AutoScrollPosition.Y) / PICTURE_BOX_SIZE;
                label1.Text = position.ToString();
                if (pictureBoxX >= 0 && pictureBoxX < scene.selectedTerrain.width &&
                    pictureBoxY >= 0 && pictureBoxY < scene.selectedTerrain.height)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        selectedTile = scene.selectedTerrain.GetTile(pictureBoxX, pictureBoxY);
                        if (selectedTilePictureBox != null)
                        {
                            selectedTile.Type = GetTileType(selectedTilePictureBox.ImageLocation);
                            selectedTile.path = selectedTilePictureBox.ImageLocation;
                        }
                        if (selectedObjectPictureBox != null)
                        {
                            selectedTile.objectOnTile.SetObject(selectedObjectPictureBox.ImageLocation);
                        }
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        draggedPictureBox.Visible = true;
                        if (pictureBox.Parent != null && pictureBox.Parent is PictureBox)
                        {
                            draggedPictureBox.Image = (pictureBox.Parent as PictureBox).Image;
                            draggedPictureBox.ImageLocation = (pictureBox.Parent as PictureBox).ImageLocation;
                        }
                        else
                        {
                            draggedPictureBox.Image = pictureBox.Image;
                            draggedPictureBox.ImageLocation = pictureBox.ImageLocation;
                        }
                        draggedPictureBox.Location = pnlDraw.PointToClient(Control.MousePosition);
                    }
                }

            }
        }

        private void picModify_Down(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            picModify_Move(sender, e);
            if (e.Button == MouseButtons.Right)
            {
                Point position = pnlDraw.PointToClient(Cursor.Position);
                int pictureBoxX = (position.X - pnlDraw.AutoScrollPosition.X) / PICTURE_BOX_SIZE;
                int pictureBoxY = (position.Y - pnlDraw.AutoScrollPosition.Y) / PICTURE_BOX_SIZE;
                selectedTile = scene.selectedTerrain.GetTile(pictureBoxX, pictureBoxY);
            }

            PictureBox pictureBox = (PictureBox)sender;

            if (tabControl.SelectedIndex == 0)
            {
                lblTileName.Text = selectedTile.Type.ToString();
                properties.SelectedObject = selectedTile;
            }
            else
            {
                lblObjectName.Text = selectedTile.Type.ToString();
                propertiesObject.SelectedObject = selectedTile.objectOnTile;
            }

        }

        private void picModify_Up(object sender, EventArgs e)
        {
            isMouseDown = false;
            if (draggedPictureBox.Visible == true)
            {
                PictureBox pictureBox = (PictureBox)sender;
                Point position = pnlDraw.PointToClient(Cursor.Position);
                int pictureBoxX = (position.X - pnlDraw.AutoScrollPosition.X) / PICTURE_BOX_SIZE;
                int pictureBoxY = (position.Y - pnlDraw.AutoScrollPosition.Y) / PICTURE_BOX_SIZE;
                selectedTile.Type = Tile.TileType.Empty;
                selectedTile = scene.selectedTerrain.GetTile(pictureBoxX, pictureBoxY);
                selectedTile.Type = GetTileType(draggedPictureBox.ImageLocation);

                draggedPictureBox.Visible = false;
            }
        }



        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlCustomSerializer<Scene> sceneXML = new XmlCustomSerializer<Scene>("scene.xml");
            sceneXML.Save(scene);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlCustomSerializer<Scene> sceneXML = new XmlCustomSerializer<Scene>("scene.xml");
            Scene = sceneXML.Load();
            if (Scene != null)
            {
                InitializeView(scene.selectedTerrain.width, scene.selectedTerrain.height);
                Scene.UnsetEvents();
                Scene.SetEvents();
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            lblValidate.Text = "Map Existe pas";
            if (scene != null)
            {
                lblValidate.Text = "Map Non Valide";
                if (scene.ValidateMap())
                {
                    lblValidate.Text = "Valide Yay!";
                }
            }


        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-vous sauvegarder avant de quitter?", "Quitter", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                saveCloseToolStripMenuItem_Click(sender, e);
            }
            if (result == DialogResult.No)
            {
                CloseScene();
            }
        }

        private void saveCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
            CloseScene();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (scene != null)
            {
                closeToolStripMenuItem_Click(sender, e);
            }
            Application.Exit();
        }

        private void infoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Notre équipe: \nVincent Montminy \nSamuel Daigle", "Notre équipe", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }

        private Tile.TileType GetTileType(string _path)
        {
            Tile.TileType type;
            switch (_path)
            {
                case PATH_TILE_BAD:
                    type = Tile.TileType.Bad;
                    break;

                case PATH_TILE_BREAKING:
                    type = Tile.TileType.BreakPass;
                    break;

                case PATH_TILE_DOOR:
                    type = Tile.TileType.Door;
                    break;

                case PATH_TILE_EMPTY:
                    type = Tile.TileType.Empty;
                    break;

                case PATH_TILE_FLOOR:
                    type = Tile.TileType.Floor;
                    break;

                case PATH_TILE_ONE_BY_ONE:
                    type = Tile.TileType.OneByOne;
                    break;

                case PATH_TILE_SLOPE:
                    type = Tile.TileType.Slope;
                    break;

                case PATH_TILE_SLOW:
                    type = Tile.TileType.Slow;
                    break;

                case PATH_TILE_TELEPORT:
                    type = Tile.TileType.Teleport;
                    break;

                case PATH_TILE_TOWER:
                    type = Tile.TileType.Tower;
                    break;

                case PATH_TILE_WALL:
                    type = Tile.TileType.Wall;
                    break;

                default:
                    type = Tile.TileType.Empty;
                    break;
            }
            return type;
        }

    }
}
