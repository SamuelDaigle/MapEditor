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
                    int terrainWidth = form.TerrainWidth;
                    int terrainHeight = form.TerrainHeight;

                    InitializeView(terrainWidth, terrainHeight);

                    Scene = new Scene();
                    Scene.name = form.SceneName;

                    Scene.terrain.Initialize(terrainWidth, terrainHeight);
                    
                }
            }
        }
    }
}
