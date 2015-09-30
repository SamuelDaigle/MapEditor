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

        public void InitializeControls()
        {
            newSceneToolStripMenuItem.Click += new System.EventHandler(this.newSceneToolStripMenuItem_Click);
            picTileBreakPass.Click += new System.EventHandler(this.picTile_Click);
            picTileOneByOne.Click += new System.EventHandler(this.picTile_Click);
            picTileSlow.Click += new System.EventHandler(this.picTile_Click);
            picTileDoor.Click += new System.EventHandler(this.picTile_Click);
            selectedPictureBox = picTileEmpty;
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

                    InitializeView(terrainWidth, terrainHeight);

                    Scene = new Scene();
                    Scene.name = form.SceneName;

                    Scene.terrain.Initialize(terrainWidth, terrainHeight);

                }
            }
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
                if (pictureBoxX >= 0 && pictureBoxX < scene.terrain.width &&
                    pictureBoxY >= 0 && pictureBoxY < scene.terrain.height)
                {
                    if (selectedPictureBox != null)
                    {
                        selectedTile = scene.terrain.GetTile(pictureBoxX, pictureBoxY);
                        selectedTile.Type = GetTileType(selectedPictureBox.ImageLocation);
                        selectedTile.path = selectedPictureBox.ImageLocation;
                        if (e.Button == MouseButtons.Right)
                        {
                            selectedPictureBox = (PictureBox)sender;
                            lblTileName.Text = selectedTile.path.Substring(selectedTile.path.LastIndexOf('/') + 1);
                            lblTileName.Text = lblTileName.Text.Substring(0, lblTileName.Text.LastIndexOf('.'));
                            grbProperties.Visible = true;
                            properties.SelectedObject = selectedTile;
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
            scene = sceneXML.Load();
            InitializeView(scene.terrain.width, scene.terrain.height);
            for (int y = 0; y < scene.terrain.height; y++)
            {
                for (int x = 0; x < scene.terrain.width; x++)
                {
                    pictureBoxes[y][x].Image = GetImage(scene.terrain.GetTile(x, y));
                }
            }
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
