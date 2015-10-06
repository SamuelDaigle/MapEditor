using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map_Editor.GameData
{
    class Validator
    {
        private Scene sceneToValidate;

        public Validator(Scene _scene)
        {
            sceneToValidate = _scene;
        }

        public bool ValidateMap()
        {
            bool mapValid;

            mapValid = validateFirstFloorNotEmpty();
            if (mapValid)
            {
                mapValid = validateObjects();
            }
            if (mapValid)
            {
                mapValid = validateSlopes();
            }
            if (mapValid)
            {
                mapValid = validateTeleporters();
            }
            if (mapValid)
            {
                mapValid = validateTowers();
            }
            return mapValid;
        }

        private bool validateFirstFloorNotEmpty()
        {
            foreach (Tile T in sceneToValidate.floors[0].Tiles)
            {
                if (T.Type == Tile.TileType.Empty)
                {
                    return false;
                }
            }
            return true;
        }

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
                            return false;
                        }
                    }
                }
            }
            return true;
        }

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
                                    return false;
                                }
                            }
                        }

                        if (noSlopesNear == 1 || noSlopesNear == 2)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            return true;
        }

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
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

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
            return false;
        }
    }
}
