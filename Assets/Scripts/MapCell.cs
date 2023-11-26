using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateMap
{
    public class MapCell : MonoBehaviour
    {
        private int x, z;
        private bool isTaken;
        private CellType cellType;

        public int X {get => x;}
        public int Z {get => z;}

        public bool IsTaken {get => isTaken; set => isTaken = value;}
        public CellType CellType {get => cellType; set => cellType = value;}

        public MapCell(int x, int z)
        {
            this.x = x;
            this.z = z;
            this.cellType = CellType.Water;
            isTaken = false;
        }

    }

    public enum CellType
    {
        Water,
        Empty,
        Start,
        Store,
        Treasure,
        Fort,
        KeyIsland
    }
}
