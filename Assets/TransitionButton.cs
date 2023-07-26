using UnityEngine;

public class TransitionButton : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        gameManager.LoadCleanScene();
    }
}
