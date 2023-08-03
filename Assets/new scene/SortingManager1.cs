using UnityEngine;
using UnityEngine.SceneManagement;

public class SortingManager : MonoBehaviour
{
    public string[] tagsToCheck; // Tags of the objects to check for deletion
    public string nextSceneName; // Name of the next scene to load

    private void Update()
    {
        // Check if all objects with the specified tags are deleted
        bool allObjectsDeleted = true;
        foreach (string tag in tagsToCheck)
        {
            if (GameObject.FindGameObjectWithTag(tag) != null)
            {
                allObjectsDeleted = false;
                break;
            }
        }

        // If all objects are deleted, transfer to the next scene
        if (allObjectsDeleted)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
