using UnityEngine;

public class ProgramObjectsFolder : MonoBehaviour
{
    public Transform container; // Reference to the container where the objects will be placed

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("program"))
        {
            Destroy(obj);
        }
    }
}
