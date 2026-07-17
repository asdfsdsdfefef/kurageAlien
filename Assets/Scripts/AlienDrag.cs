using UnityEngine;

public class AlienDrag : MonoBehaviour
{
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
        transform.position = startPosition;
    }
}