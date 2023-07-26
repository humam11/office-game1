using UnityEngine;

public class Purpose : MonoBehaviour
{
    public bool IsNecessary;

    private CleanScene cleanScene;
    private bool isBeingDragged;
    private bool isTriggered;

    public bool IsTriggered
    {
        get { return isTriggered; }
    }


    public void SetTriggered(bool triggered)
    {
        isTriggered = triggered;
    }

    public void Initialize(CleanScene scene)
    {
        cleanScene = scene;
    }

    private void OnMouseDrag()
    {
        isBeingDragged = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
    }

    private void OnMouseUp()
    {
        if (isBeingDragged)
        {
            isBeingDragged = false;
            StoreInBox();
        }
    }

    private void StoreInBox()
    {
        if (!isTriggered)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("NecessaryPurposesBox") && IsNecessary)
                {
                    cleanScene.StoreNecessaryPurpose(gameObject);
                    break;
                }
                else if (collider.CompareTag("UnnecessaryPurposesBox") && !IsNecessary)
                {
                    cleanScene.StoreNecessaryPurpose(gameObject);
                    break;
                }
            }
        }
    }



}
