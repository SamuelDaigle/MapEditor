using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Map_Editor.GameData
{
    public class Scene
    {
        public string name;
        public Terrain terrain;

        [field: NonSerialized]
        public event EventHandler SceneChanged;

        public Scene()
        {
            terrain = new Terrain();
            terrain.TerrainChanged += OnTerrainChanged;
        }

        // Received the tile by the terrain.
        private void OnTerrainChanged(object sender, EventArgs e)
        {
            OnSceneChanged(sender, e);
        }


        // Notify the view.
        private void OnSceneChanged(object sender, EventArgs e)
        {
            if (SceneChanged != null)
            {
                SceneChanged(sender, e);
            }
        }
    }
}
