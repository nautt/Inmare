using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Random = UnityEngine.Random;

namespace PirateMap
{

    //Coloca las islas en el mapa
    public class CandidateMap
    {
        public MapGrid grid;
        private int numberOfStoreIslands = 0;
        private int numberOfFortIslands = 0;
        private int numberOfTreasureIslands = 0;
        private bool[] islandInIndex = null;
        private Vector3 startPoint;
        private List<StoreIsland> storeIslandList;
        private List<FortIsland> fortIslandList;
        private List<TreasureIsland> treasureIslandList;
        
        public MapGrid Grid {get => grid;}
        public bool[] IslandInIndex {get => islandInIndex;}

        public CandidateMap(MapGrid grid, int nStores, int nForts, int nTreasures)
        {
            this.grid = grid;
            numberOfStoreIslands = nStores;
            numberOfFortIslands = nForts;
            numberOfTreasureIslands = nTreasures;
            
        }

        public void CreateMap(Vector3 startPoint)
        {
            this.startPoint = startPoint;
            islandInIndex = new bool[grid.Width*grid.Length];

            this.storeIslandList = new List<StoreIsland>();
            this.fortIslandList = new List<FortIsland>();
            this.treasureIslandList = new List<TreasureIsland>();

            PlaceRandomIslands(CellType.Store, numberOfStoreIslands);
            PlaceRandomIslands(CellType.Fort, numberOfFortIslands);
            PlaceRandomIslands(CellType.Treasure, numberOfTreasureIslands);
            PlaceRandomIslands(CellType.KeyIsland, 1);
        }

        private bool CheckIfPositionCanBeObstacle(Vector3 position)
        {
            if (position == startPoint) return false;
            int index = grid.CalculateIndexFromCoordinates(position.x, position.z);
            return islandInIndex[index] == false;
        }

        public static void SetRandomStartPoint(MapGrid grid, ref Vector3 startPoint)
        {
            startPoint = new Vector3(Random.Range(0, grid.Width), 0, Random.Range(0, grid.Length));
            grid.SetCell(startPoint.x, startPoint.y, CellType.Start);
        }

        private void PlaceRandomIslands(CellType type, int numberOfPieces)
        {
            var count = numberOfPieces;
            var tryLimit = 100; //Por si no quedan celdas libres para colocar la isla.
            while (count > 0 && tryLimit > 0)
            {
                var randomIndex = Random.Range(0, grid.Size);
                var coordinates = grid.CalculateCoordinatesFromIndex(randomIndex);
                if (!grid.IsCellTaken(coordinates.x, coordinates.z)) //free space
                {
                    switch (type)
                    {
                        case CellType.Store:
                            grid.SetCell(coordinates.x, coordinates.z, CellType.Store);
                            storeIslandList.Add(new StoreIsland(coordinates));
                            break;
                        case CellType.Fort:
                            grid.SetCell(coordinates.x, coordinates.z, CellType.Fort);
                            fortIslandList.Add(new FortIsland(coordinates));
                            break;
                        case CellType.Treasure:
                            grid.SetCell(coordinates.x, coordinates.z, CellType.Treasure);
                            treasureIslandList.Add(new TreasureIsland(coordinates));
                            break;
                        case CellType.KeyIsland:
                            grid.SetCell(coordinates.x, coordinates.z, CellType.KeyIsland);
                            // treasureIslandList.Add(new TreasureIsland(coordinates));
                            break;
                        default:
                            Debug.Log("Error al colocar isla tipo " + type + " en mapa.");
                            break;
                    }

                    islandInIndex[randomIndex] = true;
                    count--;
                }
                tryLimit--;
            }

            // if (type == CellType.Treasure)
            // {
            //     SetKeyOnRandomIsland();
            // }
        }

        // public void SetKeyOnRandomIsland()
        // {
        //     var index = Random.Range(0, numberOfTreasureIslands);
        //     Debug.Log("Tienda numero " + index + " en coordenadas " + treasureIslandList[index].Position);
        //     treasureIslandList[index].hasKey = true;
        // }

        public MapData ReturnMapData()
        {
            return new MapData
            {
                obstacleArray = this.islandInIndex,
                StoreIslandList = this.storeIslandList,
                FortIslandList = this.fortIslandList,
                TreasureIslandList = this.treasureIslandList,
                startPoint = startPoint
            };
        }
    }
}
