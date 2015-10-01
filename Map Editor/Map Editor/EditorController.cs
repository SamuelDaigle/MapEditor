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
        private PictureBox selectedPictureBox;
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
            selectedPictureBox = picTileEmpty;
            picTile_Click(selectedPictureBox, EventArgs.Empty);
            
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

                    Scene.SetEvents();
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

        private void selectFloor_Click(object sender, EventArgs e)
        {
            Button button = (Button) sender;
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
            if (selectedPictureBox != null)
            {
                selectedPictureBox.BackColor = Color.Transparent;
            }
            selectedPictureBox = (PictureBox)sender;
            selectedPictureBox.BackColor = Color.Yellow;
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
                    if (selectedPictureBox != null)
                    {
                        selectedTile = scene.selectedTerrain.GetTile(pictureBoxX, pictureBoxY);
                        if (e.Button == MouseButtons.Left)
                        {
                            selectedTile.Type = GetTileType(selectedPictureBox.ImageLocation);
                            if (selectedPictureBox.ImageLocation == null)
                            {
                                selectedPictureBox.ImageLocation = selectedTile.path;
                            }
                            selectedTile.path = selectedPictureBox.ImageLocation;
                        }
                        if (e.Button == MouseButtons.Right)
                        {
                            lblTileName.Text = selectedTile.Type.ToString();
                            properties.SelectedObject = selectedTile;
                            grbProperties.Visible = true;
                        }
                    }
                }

            }
        }

        private void picModify_Down(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            picModify_Move(sender, e);
        }

        private void picModify_Up(object sender, EventArgs e)
        {
            isMouseDown = false;
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
            InitializeView(scene.selectedTerrain.width, scene.selectedTerrain.height);
            for (int y = 0; y < scene.selectedTerrain.height; y++)
            {
                for (int x = 0; x < scene.selectedTerrain.width; x++)
                {
                    pictureBoxes[y][x].Image = GetImage(scene.selectedTerrain.GetTile(x, y));
                }
            }
            Scene.SetEvents();
            Scene.selectedTerrain.SetEvents();
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
