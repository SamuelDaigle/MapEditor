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

        public Editor()
        {
            InitializeComponent();
        }

        private void newSceneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SceneCreation form = new SceneCreation())
            {
                DialogResult dialogResult = form.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    scene = new Scene();
                    scene.name = form.SceneName;
                    int terrainWidth = form.TerrainWidth;
                    int terrainHeight = form.TerrainHeight;

                    Terrain terrain = new Terrain();

                    for (int y = 0; y < terrainHeight; y++)
                    {
                        for (int x = 0; x < terrainWidth; x++)
                        {
                            Tile tile = new Tile(Tile.TileType.Empty);
                            terrain.tiles.Add(tile);
                        }
                    }

                    InitializeView(terrainWidth, terrainHeight);
                }
            }
        }
    }
}
