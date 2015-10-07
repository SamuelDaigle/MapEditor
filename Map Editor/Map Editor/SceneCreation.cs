using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Map_Editor.GameData;

namespace Map_Editor
{
    /// <summary>
    /// Form called when creating a new scene.
    /// </summary>
    public partial class SceneCreation : Form
    {
        // Values returned after the form is closed.
        public string SceneName { get; set; }
        public int TerrainWidth { get; set; }
        public int TerrainHeight { get; set; }

        public Tile.TileType TileType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneCreation"/> class.
        /// </summary>
        public SceneCreation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {

            SceneName = txtSceneName.Text;
            TerrainWidth = Convert.ToInt32(numWidth.Value);
            TerrainHeight = Convert.ToInt32(numHeight.Value);

            foreach (Tile.TileType type in Enum.GetValues(typeof(Tile.TileType)))
            {
                if (type.ToString() == dropDefaultTile.SelectedItem.ToString())
                {
                    TileType = type;
                }
            }

            if (SceneName == "")
            {
                lblError.Text = "Scene name must be set";
            }
            else
            {
                btnSubmit.DialogResult = DialogResult.OK;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        /// <summary>
        /// Handles the Load event of the SceneCreation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SceneCreation_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtSceneName;

            string[] val = Enum.GetNames(typeof(Tile.TileType));

            foreach (string st in val)
                dropDefaultTile.Items.Add(st);

            dropDefaultTile.SelectedItem = dropDefaultTile.Items[0];
        }
    }
}
