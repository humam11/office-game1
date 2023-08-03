using UnityEngine;

public class IconDisappearing : MonoBehaviour
{
    private bool isMouseDragging;
    private bool isMouseOverFolder;
    private bool deleteIcon;

    public string folderTag = "programFolder"; // Tag of the folder object

    private void OnMouseDrag()
    {
        isMouseDragging = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        transform.position = mousePosition;
    }

    private void OnMouseUpAsButton()
    {
        isMouseDragging = false;
        if (isMouseOverFolder)
        {
            deleteIcon = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(folderTag))
        {
            isMouseOverFolder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(folderTag))
        {
            isMouseOverFolder = false;
        }
    }

    private void Update()
    {
        if (deleteIcon)
        {
            Destroy(gameObject);
        }
    }
}
