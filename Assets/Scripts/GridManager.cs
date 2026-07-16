using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private GameObject cellPrefab;

    [SerializeField] private int width = 5;
    [SerializeField] private int height = 5;

    [SerializeField] private float cellSize = 1f;

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

            Vector3 position = new Vector3(
            (x - (width - 1) / 2f) * cellSize,
            ((height - 1) / 2f - y) * cellSize,
            0f);

                Instantiate(cellPrefab, position, Quaternion.identity, transform);
            }
        }
    }
    public GridCell GetRandomEmptyCell()
    {
        List<GridCell> emptyCells = new List<GridCell>();

        foreach (GridCell cell in cells)
        {
            if (cell.currentAlien == null)
            {
            emptyCells.Add(cell);
            }
        }

        if (emptyCells.Count == 0)
        {
        return null;
        }

        return emptyCells[Random.Range(0, emptyCells.Count)];
    }
}