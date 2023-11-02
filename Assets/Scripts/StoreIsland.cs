using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateMap
{
    public class StoreIsland
    {
        private Vector3 position;
        public Vector3 Position {get => position; set => position = value;}

        public StoreIsland(Vector3 position)
        {
            this.position = position;
        }
    }
}
