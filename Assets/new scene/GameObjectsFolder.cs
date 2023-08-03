using UnityEngine;

public class GameObjectsFolder : MonoBehaviour
{
    public Transform container; // Reference to the container where the objects will be placed

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("game"))
        {
            // Move the object to the container and set it as a child
            Destroy(obj);
        }
    }
}
