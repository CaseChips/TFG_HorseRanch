using UnityEngine;

public class DraggableFeed : MonoBehaviour
{
    public FeedType feedType;
    
    public enum FeedType { Hay, Grain, Treat }

    private Vector3 startPosition;
    private Camera mainCamera;
    private bool isDragging = false;

    void Start()
    {
        startPosition = transform.position;
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseDrag()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; 
        
        transform.position = mousePos;
    }

    void OnMouseUp()
    {
        isDragging = false;

        FeedTrough trough = FindObjectOfType<FeedTrough>();

        if (trough != null && trough.GetComponent<Collider2D>().OverlapPoint(transform.position))
        {
            //trough.AddFeed(feedType);
        }

        transform.position = startPosition;
    }
}