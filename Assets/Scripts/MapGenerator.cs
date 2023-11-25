using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateMap 
{
    public class MapGenerator : MonoBehaviour
    {
        public GridVisualizer gridVisualizer;
        public MapVisualizer mapVisualizer;
        private Vector3 startPoint;
        [Range(0,50)] public int numberOfStoreIslands;
        [Range(0,50)] public int numberOfFortIslands;
        [Range(0,50)] public int numberOfTreasureIslands;

        [Range(10,100)] public int width, lenght = 11;
        private MapGrid grid;

        public bool visualizeUsingPrebafs = false;
        private void Start()
        {
            grid = new MapGrid(width, lenght);
            gridVisualizer.VisualizeGrid(width, lenght);
            CandidateMap.SetRandomStartPoint(grid, ref startPoint);
            CandidateMap map = new CandidateMap(grid, numberOfStoreIslands, numberOfFortIslands, numberOfTreasureIslands);
            map.CreateMap(startPoint);
            mapVisualizer.VisualizeMap(grid, map.ReturnMapData(), visualizeUsingPrebafs);
            //Debug.Log(startPoint);
            //grid.CheckCoordinates(); //Printea todas las coordenadas del grid.
        }
    }
}
