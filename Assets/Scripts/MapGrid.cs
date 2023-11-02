using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PirateMap
{
    public class MapGrid : MonoBehaviour
    {
       private int width, length;
       private MapCell[,] cellGrid;

       public int Width {get => width;}
       public int Length {get => length;}

       public int Size {get => width*length;}

       public MapGrid(int width, int length)
       {
            this.width = width;
            this.length = length;
            CreateGrid();
       }

       private void CreateGrid()
       {
            cellGrid = new MapCell[length, width];
            for (int row=0; row < length; row++)
            {
                for (int col=0; col < width; col++)
                {
                    cellGrid[row, col] = new MapCell(col,row);
                }
            }
       }
       public void SetCell(int x, int z, CellType cellType, bool isTaken = false)
       {
            cellGrid[z,x].CellType = cellType;
            cellGrid[z,x].IsTaken = isTaken;
       }

        public void SetCell(float x, float z, CellType cellType, bool isTaken = false)
       {
            SetCell((int)x, (int)z, cellType, isTaken);
       }

       public bool IsCellTaken(int x, int z)
       {
            return cellGrid[z, x].IsTaken;
       }

       public bool IsCellTaken(float x, float z)
       {
            return cellGrid[(int)z, (int)x].IsTaken;
       }

       public bool IsCellValid(float x, float z)
       {
        if (x >= width || x < 0 || z>= length || z < 0)
        {
            return false;
        }
        return true;
       }

       public MapCell GetMapCell(int x, int z)
       {
            if (IsCellValid(x, z) == false)
            {
                return null;
            }
            return cellGrid[x, z];
       }

       public MapCell GetMapCell(float x, float z)
       {
            if (IsCellValid((int)x, (int)z) == false)
            {
                return null;
            }
            return cellGrid[(int)x, (int)z];
       }

       public int CalculateIndexFromCoordinates(int x, int z)
       {
            return x + z*width;
       }

       public int CalculateIndexFromCoordinates(float x, float z)
       {
            return (int)x + (int)z*width;
       }

       public Vector3 CalculateCoordinatesFromIndex(int index)
       {
            int x = index % width;
            int z = index / width;
            return new Vector3(x, 0, z);
        }

       public void CheckCoordinates()
       {
            for (int i=0; i < cellGrid.GetLength(0); i++)
            {
                StringBuilder b = new StringBuilder();
                for (int j=0; j < cellGrid.GetLength(1); j++)
                {
                    b.Append(j+","+i+" ");
                }
                Debug.Log(b.ToString());
            }
        }
    }
}
