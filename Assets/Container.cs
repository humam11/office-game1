using UnityEngine;

public class Container : MonoBehaviour
{
    private CleanScene cleanScene;

    private void Start()
    {
        cleanScene = FindObjectOfType<CleanScene>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NecessaryPurpose"))
        {
            GameObject purpose = collision.gameObject;
            cleanScene.MoveToContainer(purpose);
        }
    }
}