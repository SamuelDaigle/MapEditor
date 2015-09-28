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
    public partial class SceneCreation : Form
    {
        // Values returned after the form is closed.
        public string SceneName { get; set; }
        public int TerrainWidth { get; set; }
        public int TerrainHeight { get; set; }

        public SceneCreation()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SceneName = txtSceneName.Text;
            TerrainWidth = Convert.ToInt32(numWidth.Value);
            TerrainHeight = Convert.ToInt32(numHeight.Value);
            this.Close();
        }
    }
}
