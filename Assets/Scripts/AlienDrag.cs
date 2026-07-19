using UnityEngine;

public class AlienDrag : MonoBehaviour
{
    private bool placedSuccessfully = false;
    private Vector3 offset;
    private Vector3 startPosition;
    private Vector3 mouseDownPosition;
    // この距離以上マウスが動いたらドラッグとみなす
    [SerializeField]
    private float dragThreshold = 0.1f;

    private Alien alien;
    private Camera mainCamera;

    private void Awake()
    {
        alien = GetComponent<Alien>();
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        startPosition = transform.position;
        mouseDownPosition = GetMouseWorldPosition();

        Vector3 mousePosition = GetMouseWorldPosition();

        offset = transform.position - mousePosition;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        transform.position = mousePosition + offset;
    }

    private void OnMouseUp()
    {
        if (!IsDragOperation())
        {
            transform.position = startPosition;
            return;
        }       

        TryPlaceAtNearestCell();
    }
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        return mousePosition;
    }
    private GridCell FindNearestCell()
    {
        GridCell[] cells = FindObjectsByType<GridCell>();

        GridCell nearestCell = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GridCell cell in cells)
        {
            float distance = Vector2.Distance(transform.position, cell.transform.position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestCell = cell;
            }
        }

        return nearestCell;
    }
    private void TryPlaceAtNearestCell()
    {
        placedSuccessfully = false;

        GridCell nearestCell = FindNearestCell();

        if (nearestCell != null)
        {
            placedSuccessfully = nearestCell.TryPlaceAlien(alien);
        }

        if (!placedSuccessfully)
        {
            transform.position = startPosition;
        }
    }
    private bool IsDragOperation()
    {
        Vector3 mouseUpPosition = GetMouseWorldPosition();

        return Vector3.Distance(mouseDownPosition, mouseUpPosition) >= dragThreshold;
    }   
}