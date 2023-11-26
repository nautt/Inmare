using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PirateMap
{
    public class MapVisualizer : MonoBehaviour
    {
        private Transform parent;
        public Color startColor;
        public GameObject player, storeIsland, fortIsland, treasureIsland, keyIsland, Enemy, BossEnemy, HealArea;
        public Vector3 puntoInicio;

        private void Awake()
        {
            parent = this.transform;
        }

        public void VisualizeMap(MapGrid grid, MapData data, bool visualizeUsingPrefabs)
        {
            if (visualizeUsingPrefabs)
            {
                VisualizeUsingPrefabs(grid, data);
            }
            else
            {
                VisualizeUsingPrimitives(grid, data);
            }
        }

        private void VisualizeUsingPrimitives(MapGrid grid, MapData data)
        {
            for (int col = 0; col < grid.Width; col++)
            {
                for (int row = 0; row < grid.Length; row++)
                {
                    var cell = grid.GetMapCell(col,row);
                    // Debug.Log(cell.CellType + "("+ col +", " + row + ")");
                    var position = new Vector3(cell.X, 0, cell.Z);
                    var index = grid.CalculateIndexFromCoordinates(position.x, position.z);

                    switch (cell.CellType)
                    {
                        case CellType.Empty:
                            break;
                        case CellType.Start:
                            PlaceObject(position, Color.green, PrimitiveType.Sphere);
                            PlaceObject(position, player);
                            break;
                        case CellType.Store:
                            PlaceObject(position, Color.blue, PrimitiveType.Cube);
                            break;
                        case CellType.Fort:
                            PlaceObject(position, Color.black, PrimitiveType.Cube);
                            break;
                        case CellType.Treasure:
                            PlaceObject(position, Color.yellow, PrimitiveType.Cube);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void VisualizeUsingPrefabs(MapGrid grid, MapData data)
        {
            for (int col = 0; col < grid.Width; col++)
            {
                for (int row = 0; row < grid.Length; row++)
                {
                    var cell = grid.GetMapCell(col,row);
                    // Debug.Log(cell.CellType + "("+ col +", " + row + ")");
                    var position = new Vector3(cell.X, 0, cell.Z);
                    Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                    var index = grid.CalculateIndexFromCoordinates(position.x, position.z);

                    switch (cell.CellType)
                    {
                        case CellType.Empty:
                            break;
                        case CellType.Start:
                            PlaceObject(position, player, randomRotation);
                            puntoInicio = position;
                            PlaceObject(position, HealArea, randomRotation);
                            break;
                        case CellType.Store:
                            PlaceObject(position, storeIsland, randomRotation);
                            break;
                        case CellType.Fort:
                            PlaceObject(position, fortIsland, randomRotation);
                            break;
                        case CellType.Treasure:
                            PlaceObject(position, treasureIsland, randomRotation);
                            Vector3 offset = new Vector3(1f, 0f, 1f);
                            int randomNumber = Mathf.RoundToInt(Random.Range(1, 4));
                            for (int i = 0; i < randomNumber; i++)
                            {
                                Vector3 Enemy_position = position + offset;
                                PlaceObject(Enemy_position, Enemy, randomRotation);
                                if (i == 2)
                                {
                                    offset.z -= 1f;
                                } else
                                {
                                    offset.x -= 1f;
                                }
                            }
                            break;
                        case CellType.KeyIsland:
                            PlaceObject(position, keyIsland, randomRotation);

                            // offset = new Vector3(1f, 0f, 1f);
                            // Enemy_position = position + offset;
                            PlaceObject(position + new Vector3(1f, 0f, 1f), BossEnemy, randomRotation);
                            
                            // offset = new Vector3(1f, 0f, -1f);
                            // Enemy_position = position + offset;
                            PlaceObject(position + new Vector3(1f, 0f, -1f), BossEnemy, randomRotation);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void PlaceObject(Vector3 position, Color color, PrimitiveType shape)
        {
            var element = GameObject.CreatePrimitive(shape);
            element.transform.position = position + new Vector3(.5f, .5f, .5f);
            element.transform.parent = parent;
            var renderer = element.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", color);
        }

        private void PlaceObject(Vector3 position, GameObject prefab, Quaternion rotation = new Quaternion())
        {
            position += new Vector3(.5f, 0f, .5f);
            var element = Instantiate(prefab, position, rotation);
            element.transform.parent = parent;
        }
    }
}
