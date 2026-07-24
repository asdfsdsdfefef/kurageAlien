using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public GameObject alienPrefab;

    public GridManager gridManager;

    public void SpawnAlien()
    {
        GridCell cell = gridManager.GetRandomEmptyCell();

        if (cell == null)
        {
            Debug.Log("空きマスがありません");
            return;
        }

        GameObject alien = Instantiate(alienPrefab, cell.transform.position, Quaternion.identity);

        Alien alienComponent = alien.GetComponent<Alien>();
        alienComponent.MoveToCell(cell);    
    }

    public void SpawnAlienAt(Vector2Int gridPosition, int level)
    {
        GridCell targetCell = gridManager.GetCell(gridPosition);

        if (targetCell == null)
        {
            Debug.LogWarning($"Gridcellが見つかりません : {gridPosition}");
            return;
        }

        GameObject alienObject = Instantiate(alienPrefab);

        Alien alien = alienObject.GetComponent<Alien>();

        alien.level = level;
        alien.MoveToCell(targetCell);

        Debug.Log($"復元 : Lv{level} ({gridPosition})");
    }
}