using UnityEngine;

public class GridCell : MonoBehaviour
{
    public GameObject currentAlien;
}
    public bool IsOccupied { get; private set; } = false;

    public void SetOccupied(bool occupied)
    {
        IsOccupied = occupied;
    }
}