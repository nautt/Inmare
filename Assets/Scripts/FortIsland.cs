using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateMap
{
    public class FortIsland
    {
        private Vector3 position;
        public Vector3 Position {get => position; set => position = value;}

        public FortIsland(Vector3 position)
        {
            this.position = position;
        }
    }
}