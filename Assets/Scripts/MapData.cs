using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateMap
{
    public struct MapData
    {
        public bool[] obstacleArray;
        public List<StoreIsland> StoreIslandList;
        public List<FortIsland> FortIslandList;
        public List<TreasureIsland> TreasureIslandList;
        public Vector3 startPoint;
    }
}
