using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateMap
{
    public class GridVisualizer : MonoBehaviour
    {
        public GameObject emptyCellPrefab;
        public void VisualizeGrid(int width, int length)
        {
            Vector3 position = new(width/2f, 0, length/2f);
            Quaternion rotation = Quaternion.Euler(90, 0, 0);
            var board = Instantiate(emptyCellPrefab, position, rotation);
            board.transform.localScale = new Vector3(width, length, 1); //lenght en posicion y porque es local y esta rotado.
        }
    }
}
