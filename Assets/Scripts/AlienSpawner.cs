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

        GameObject alien =
            Instantiate(alienPrefab,
                        cell.transform.position,
                        Quaternion.identity);

        cell.currentAlien = alien;
    }
}