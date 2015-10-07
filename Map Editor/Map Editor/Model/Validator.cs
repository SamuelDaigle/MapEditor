using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map_Editor.GameData
{
    /// <summary>
    /// Validates the scene.
    /// </summary>
    public class Validator
    {
        private Scene sceneToValidate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Validator"/> class.
        /// </summary>
        /// <param name="_scene">The _scene.</param>
        public Validator(Scene _scene)
        {
            sceneToValidate = _scene;
        }

        /// <summary>
        /// Validates the map.
        /// </summary>
        /// <returns></returns>
        public bool ValidateMap()
        {
            if (!validateFirstFloorNotEmpty()) return false;
            if (!validateObjects()) return false;
            if (!validateSlopes()) return false;
            if (!validateTeleporters()) return false;
            if (!validateTowers()) return false;

            return true;
        }

        /// <summary>
        /// Validates the first floor not empty.
        /// </summary>
        /// <returns></returns>
        private bool validateFirstFloorNotEmpty()
        {
            foreach (Tile T in sceneToValidate.floors[0].Tiles)
            {
                if (T.Type == Tile.TileType.Empty)
                {
                    MessageBox.Show("Aucune tuile vide sur le plancher 1", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Validates the teleporters.
        /// </summary>
        /// <returns></returns>
        private bool validateTeleporters()
        {
            foreach (Floor F in sceneToValidate.floors)
            {
                foreach (Tile T in F.Tiles)
                {
                    if (T.Type == Tile.TileType.Teleport)
                    {
                        if (F.GetTile(T.teleportPoint.X, T.teleportPoint.Y).Type != Tile.TileType.Teleport)
                        {
                            MessageBox.Show("Téléporteur doit pointer vers un téléporteur.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Validates the slopes.
        /// </summary>
        /// <returns></returns>
        private bool validateSlopes()
        {
            foreach (Floor F in sceneToValidate.floors)
            {
                foreach (Tile T in F.Tiles)
                {
                    if (T.Type == Tile.TileType.Slope)
                    {
                        if (sceneToValidate.floors[sceneToValidate.floors.IndexOf(F) + 1].GetTile(T.position.X,
                                T.position.Y).Type != Tile.TileType.Empty)
                        {
                            MessageBox.Show("Tuile doit être vide par dessus une pente.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return false;
                        }

                        Tile.Orientation orientation = T.orientation;
                        int noSlopesNear = 0;
                        List<Tile> tilesAroundList = new List<Tile>();
                        
                        tilesAroundList.Add(F.GetTile(T.position.X - 1, T.position.Y));
                        tilesAroundList.Add(F.GetTile(T.position.X + 1, T.position.Y));
                        tilesAroundList.Add(F.GetTile(T.position.X, T.position.Y - 1));
                        tilesAroundList.Add(F.GetTile(T.position.X, T.position.Y + 1));

                        foreach (Tile tile in tilesAroundList)
                        {
                            if (tile.Type == Tile.TileType.Slope)
                            {
                                noSlopesNear++;
                                if (tile.orientation != orientation)
                                {
                                    MessageBox.Show("Les pentes doivent être dans la même direction", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    return false;
                                }
                            }
                        }

                        if (noSlopesNear == 1 || noSlopesNear == 2)
                        {
                            return true;
                        }
                        MessageBox.Show("La pente est trop longue", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Validates the towers.
        /// </summary>
        /// <returns></returns>
        private bool validateTowers()
        {
            foreach (Floor F in sceneToValidate.floors)
            {
                foreach (Tile T in F.Tiles)
                {
                    if (T.Type == Tile.TileType.Tower)
                    {
                        foreach (Floor floor in sceneToValidate.floors)
                        {
                            if (floor.GetTile(T.position.X, T.position.Y).Type != Tile.TileType.Tower)
                            {
                                MessageBox.Show("La tour ne se rend pas jusqu'au plus haut plancher.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Validates the objects.
        /// </summary>
        /// <returns></returns>
        private bool validateObjects()
        {
            int numberOfSpawns = 0;
            int numberOfBalls = 0;
            int numberOfGoal = 0;

            foreach (Floor F in sceneToValidate.floors)
            {
                foreach (Tile T in F.Tiles)
                {
                    if (T.objectOnTile.utilType == GameObject.UtilType.Goal)
                    {
                        numberOfGoal++;
                    }
                    if (T.objectOnTile.utilType == GameObject.UtilType.Spawn)
                    {
                        numberOfSpawns++;
                    }
                    if (T.objectOnTile.utilType == GameObject.UtilType.Ball)
                    {
                        numberOfBalls++;
                    }
                }
            }
            if (numberOfGoal == 1 && numberOfBalls >= 1 && numberOfSpawns == 4)
            {
                return true;
            }
            MessageBox.Show("Le nombre d'objet n'est pas valide \n==1 but \n>=1 balle(s) \n==4 spawns", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            return false;
        }
    }
}
