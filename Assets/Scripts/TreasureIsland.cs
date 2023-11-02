using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateMap
{
    public class TreasureIsland
    {
        private Vector3 position;
        public int remainingLoot;
        public Vector3 Position {get => position; set => position = value;}
        //public int RemainingLoot {get => remainingLoot; set => remainingLoot = value;}

        public TreasureIsland(Vector3 position)
        {
            this.position = position;
            this.remainingLoot = Random.Range(1, 10);
        }
    }
}