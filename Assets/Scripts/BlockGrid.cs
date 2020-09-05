using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGrid : MonoBehaviour
{
    // Fields
    [SerializeField]
    public int GridXSize = 1;
    public int GridYSize = 1;

    // Members
    private static int GridXStaticSize;
    private static int GridYStaticSize;
    private static bool[,] m_GridFillMatrix;

    // Singleton instance
    public static BlockGrid BlockGridSingleton = null;

    private void Awake()
    {
        if (BlockGridSingleton != null)
        {
            Destroy(GetComponent<BlockGrid>());
            return;
        }

        BlockGridSingleton = this;

        GridXStaticSize = GridXSize;
        GridYStaticSize = GridYSize;
        m_GridFillMatrix = new bool[GridXStaticSize, GridYStaticSize];
    }

    // Interface

    public static bool IsFilled(Tuple<int, int> coord)
    {
        return IsFilled(coord.Item1, coord.Item2);
    }
    public static bool IsFilled(int x, int y)
    {
        if (x < 0 || x >= GridXStaticSize ||
            y < 0 || y >= GridYStaticSize)
        {
            return true;
        }
        else
        {
            return m_GridFillMatrix[x, y];
        }
    }
    public static bool ChangeBlockStatus(bool status, Tuple<int, int> coord)
    {
        return ChangeBlockStatus(status, coord.Item1, coord.Item2);
    }
    public static bool ChangeBlockStatus(bool status, int x, int y)
    {
        if (x < 0 || x >= GridXStaticSize ||
            y < 0 || y >= GridYStaticSize)
        {
            return false;
        }
        else
        {
            m_GridFillMatrix[x, y] = status;
            return true;
        }
    }

    public static int GetXSize() { return GridXStaticSize; }
    public static int GetYSize() { return GridYStaticSize; }

    public static Vector3 GridCoordToWorldPosition(Tuple<int, int> coord)
    {
        return GridCoordToWorldPosition(coord.Item1, coord.Item2);
    }
    public static Vector3 GridCoordToWorldPosition(int x, int y)
    {
        float xPosition = x - (GridXStaticSize / 2);
        float yPosition = y + 1.5f;

        return new Vector3(xPosition, yPosition, 0f);
    }
}
