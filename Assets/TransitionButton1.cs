using UnityEngine;

public class TransitionButton1 : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        gameManager.LoadFirstScene();
    }
}
