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
    public partial class View
    {
        public class Controller
        {
            private readonly View view;

            private Scene model;

            public Scene CurrentModel
            {
                get
                {
                    return model;
                }
                set
                {
                    if (model != null)
                    {
                        model.FloorAdded -= OnFloorAdded;
                        model.SceneChanged -= OnSceneChanged;
                    }

                    model = value;

                    if (model != null)
                    {
                        model.FloorAdded += OnFloorAdded;
                        model.SceneChanged += OnSceneChanged;
                    }
                }
            }

            private PictureBox selectedTilePictureBox;
            private PictureBox selectedObjectPictureBox;
            private Tile draggedTiles;
            private Tile selectedTile;
            private bool isMouseDown = false;

            public Controller(View view)
            {
                this.view = view;

                view.newSceneToolStripMenuItem.Click += new System.EventHandler(this.newSceneToolStripMenuItem_Click);
                view.picTileBreakPass.Click += new System.EventHandler(this.picTile_Click);
                view.picTileOneByOne.Click += new System.EventHandler(this.picTile_Click);
                view.picTileSlow.Click += new System.EventHandler(this.picTile_Click);
                view.picTileDoor.Click += new System.EventHandler(this.picTile_Click);
                view.picTileWall.Click += new System.EventHandler(this.picTile_Click);
                view.picTileEmpty.Click += new System.EventHandler(this.picTile_Click);
                view.picTileFloor.Click += new System.EventHandler(this.picTile_Click);
                view.picTileSlope.Click += new System.EventHandler(this.picTile_Click);
                view.picTileTeleport.Click += new System.EventHandler(this.picTile_Click);
                view.picTileTower.Click += new System.EventHandler(this.picTile_Click);
                view.picTileBad.Click += new System.EventHandler(this.picTile_Click);
                // TRAPS
                view.picTrapDrop.Click += new System.EventHandler(this.picObject_Click);
                view.picTrapFire.Click += new System.EventHandler(this.picObject_Click);
                view.picTrapFreeze.Click += new System.EventHandler(this.picObject_Click);
                view.picTrapSpike.Click += new System.EventHandler(this.picObject_Click);
                view.picTrapTurret.Click += new System.EventHandler(this.picObject_Click);

                // Bonus
                view.picBonusDash.Click += new System.EventHandler(this.picObject_Click);
                view.picBonusDecoy.Click += new System.EventHandler(this.picObject_Click);
                view.picBonusDouble.Click += new System.EventHandler(this.picObject_Click);
                view.picBonusEMP.Click += new System.EventHandler(this.picObject_Click);
                view.picBonusImplosion.Click += new System.EventHandler(this.picObject_Click);
                view.picBonusMissile.Click += new System.EventHandler(this.picObject_Click);
                view.picBonusPower.Click += new System.EventHandler(this.picObject_Click);
                view.picBonusRicochet.Click += new System.EventHandler(this.picObject_Click);
                view.picBonusShield.Click += new System.EventHandler(this.picObject_Click);
                view.picBonusSpeed.Click += new System.EventHandler(this.picObject_Click);

                // Utilities
                view.picUtilGoal.Click += new System.EventHandler(this.picObject_Click);
                view.picUtilJump.Click += new System.EventHandler(this.picObject_Click);
                view.picUtilSpawn.Click += new System.EventHandler(this.picObject_Click);
                view.picUtilBall.Click += new System.EventHandler(this.picObject_Click);

                // Others
                view.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
                view.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
                view.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
                view.saveCloseToolStripMenuItem.Click += new System.EventHandler(this.saveCloseToolStripMenuItem_Click);
                view.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
                view.infoToolMenu.Click += new System.EventHandler(this.infoToolStripMenuItem1_Click);
                view.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
                view.btnTopView.Click += new System.EventHandler(this.btnTopView_Click);
                view.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
                view.btnDeleteTile.Click += new System.EventHandler(this.btnDeleteTile_Click);
                view.btnDeleteObject.Click += new System.EventHandler(this.btnDeleteObject_Click);

                selectedTilePictureBox = view.picTileEmpty;
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

                        view.InitializeView(terrainWidth, terrainHeight);

                        CurrentModel = new Scene();
                        CurrentModel.name = form.SceneName;
                        CurrentModel.floorWidth = terrainWidth;
                        CurrentModel.floorHeight = terrainHeight;
                        CurrentModel.LoadFromFile(CurrentModel.name + ".xml");

                        SetAllTileViewEvents();
                    }
                }
            }

            private void SetAllTileViewEvents()
            {
                for (int y = 0; y < CurrentModel.floorHeight; y++)
                {
                    for (int x = 0; x < CurrentModel.floorWidth; x++)
                    {
                        view.pictureBoxes[y][x].MouseMove += picModify_Move;
                        view.pictureBoxes[y][x].MouseDown += picModify_Down;
                        view.pictureBoxes[y][x].MouseUp += picModify_Up;

                        PictureBox objectBox = (PictureBox)view.pictureBoxes[y][x].Controls[0];
                        objectBox.MouseMove += picModify_Move;
                        objectBox.MouseDown += picModify_Down;
                        objectBox.MouseUp += picModify_Up;
                    }
                }
            }

            private void btnAdd_Click(object sender, EventArgs e)
            {
                CurrentModel.AddFloor();

                selectFloor_Click((Button)sender, EventArgs.Empty);

                if (CurrentModel.floors.Count >= 5)
                {
                    view.btnAdd.Enabled = false;
                }
            }

            private void btnTopView_Click(object sender, EventArgs e)
            {
                CurrentModel.SelectTopFloor();
            }

            private void selectFloor_Click(object sender, EventArgs e)
            {
                Button button = (Button)sender;
                int floorID = Convert.ToInt32(button.Text);



                // Remove handles from the old floor
                CurrentModel.UnsetEvents();

                CurrentModel.SelectFloor(floorID - 1);

                // Add handles to the new floor.
                CurrentModel.SetEvents();
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
                    Point position = view.pnlDraw.PointToClient(Cursor.Position);
                    int pictureBoxX = (position.X - view.pnlDraw.AutoScrollPosition.X) / PICTURE_BOX_SIZE;
                    int pictureBoxY = (position.Y - view.pnlDraw.AutoScrollPosition.Y) / PICTURE_BOX_SIZE;
                    view.label1.Text = position.ToString();
                    if (pictureBoxX >= 0 && pictureBoxX < CurrentModel.selectedFloor.width &&
                        pictureBoxY >= 0 && pictureBoxY < CurrentModel.selectedFloor.height)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            selectedTile = CurrentModel.selectedFloor.GetTile(pictureBoxX, pictureBoxY);
                            if (selectedTilePictureBox != null)
                            {
                                selectedTile.SetTile(selectedTilePictureBox.ImageLocation);
                            }
                            if (selectedObjectPictureBox != null)
                            {
                                selectedTile.objectOnTile.SetObject(selectedObjectPictureBox.ImageLocation);
                            }
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            view.draggedPictureBox.Visible = true;
                            if (pictureBox.Parent != null && pictureBox.Parent is PictureBox)
                            {
                                view.draggedPictureBox.Image = (pictureBox.Parent as PictureBox).Image;
                            }
                            else
                            {
                                view.draggedPictureBox.Image = pictureBox.Image;
                            }
                            view.draggedPictureBox.Location = view.pnlDraw.PointToClient(Control.MousePosition);
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
                    Point position = view.pnlDraw.PointToClient(Cursor.Position);
                    int pictureBoxX = (position.X - view.pnlDraw.AutoScrollPosition.X) / PICTURE_BOX_SIZE;
                    int pictureBoxY = (position.Y - view.pnlDraw.AutoScrollPosition.Y) / PICTURE_BOX_SIZE;
                    selectedTile = CurrentModel.selectedFloor.GetTile(pictureBoxX, pictureBoxY);
                    if (draggedTiles == null)
                    {
                        draggedTiles = selectedTile;
                    }
                }

                PictureBox pictureBox = (PictureBox)sender;
                view.SelectPictureBox(pictureBox);

                if (view.tabControl.SelectedIndex == 0)
                {
                    view.lblTileName.Text = selectedTile.Type.ToString();
                    view.properties.SelectedObject = selectedTile;
                }
                else
                {
                    view.lblObjectName.Text = selectedTile.Type.ToString();
                    view.propertiesObject.SelectedObject = selectedTile.objectOnTile;
                }

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

            private void picModify_Up(object sender, EventArgs e)
            {
                isMouseDown = false;
                if (view.draggedPictureBox.Visible == true)
                {
                    PictureBox pictureBox = (PictureBox)sender;
                    Point position = view.pnlDraw.PointToClient(Cursor.Position);
                    int pictureBoxX = (position.X - view.pnlDraw.AutoScrollPosition.X) / PICTURE_BOX_SIZE;
                    int pictureBoxY = (position.Y - view.pnlDraw.AutoScrollPosition.Y) / PICTURE_BOX_SIZE;
                    selectedTile = CurrentModel.selectedFloor.GetTile(pictureBoxX, pictureBoxY);

                    DialogResult dialogResult;
                    if (selectedTile.Type == Tile.TileType.Empty)
                    {
                        dialogResult = DialogResult.Yes;
                    }
                    else
                    {
                        dialogResult = MessageBox.Show("Voulez-vous vraiment écraser cette tuile?", "Drag", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                    }

                    if (dialogResult == DialogResult.Yes)
                    {
                        selectedTile.Type = draggedTiles.Type;
                        draggedTiles.Type = Tile.TileType.Empty;
                        draggedTiles = null;
                    }

                    view.draggedPictureBox.Visible = false;
                }
            }



            private void saveToolStripMenuItem_Click(object sender, EventArgs e)
            {
                XmlCustomSerializer<Scene> sceneXML = new XmlCustomSerializer<Scene>("scene.xml");
                sceneXML.Save(CurrentModel);
            }

            private void loadToolStripMenuItem_Click(object sender, EventArgs e)
            {
                
            }

            private void btnValidate_Click(object sender, EventArgs e)
            {
                view.lblValidate.Text = "Map Existe pas";
                if (CurrentModel != null)
                {
                    view.lblValidate.Text = "Map Non Valide";
                    if (CurrentModel.ValidateMap())
                    {
                        view.lblValidate.Text = "Valide Yay!";
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
                    view.CloseScene();
                }
            }

            private void saveCloseToolStripMenuItem_Click(object sender, EventArgs e)
            {
                saveToolStripMenuItem_Click(sender, e);
                view.CloseScene();
            }

            private void exitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (CurrentModel != null)
                {
                    closeToolStripMenuItem_Click(sender, e);
                }
                Application.Exit();
            }

            private void infoToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Notre équipe: \nVincent Montminy \nSamuel Daigle", "Notre équipe", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }

            // Received the added floor.
            private void OnFloorAdded(object sender, EventArgs e)
            {
                // Add it to the view.
                Button btnFloor = new Button();
                btnFloor.Text = CurrentModel.floors.Count.ToString();
                btnFloor.Size = new Size(view.btnUp.Size.Width, view.btnUp.Size.Height);
                btnFloor.Click += selectFloor_Click;
                view.pnlGroupFloors.Controls.Add(btnFloor);
            }

            // Received the modified tile.
            private void OnSceneChanged(object sender, EventArgs e)
            {
                if (sender is Tile) // Only update one tile.
                {
                    view.RefreshObject(sender);
                }
                else if (sender is Scene)
                {
                    for (int y = 0; y < CurrentModel.floorHeight; y++)
                    {
                        for (int x = 0; x < CurrentModel.floorWidth; x++)
                        {
                            // Using CurrentModel prevents the casting of sender as they are the same.
                            view.RefreshObject(CurrentModel.selectedFloor.GetTile(x, y));
                        }
                    }
                }
            }
        }
    }
}
