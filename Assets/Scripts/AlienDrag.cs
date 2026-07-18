using UnityEngine;

public class AlienDrag : MonoBehaviour
{
    private bool placedSuccessfully = false;
    private Vector3 offset;
    private Vector3 startPosition;

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

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        offset = transform.position - mousePosition;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        transform.position = mousePosition + offset;
    }

    private void OnMouseUp()
    {
        placedSuccessfully = false;

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

        if (nearestCell != null)
        {
            placedSuccessfully = nearestCell.TryPlaceAlien(alien);
        }

        if (!placedSuccessfully)
        {
            transform.position = startPosition;
        }
    }
}