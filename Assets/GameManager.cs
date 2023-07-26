using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float lowestTime = Mathf.Infinity;

    private void Start()
    {

        lowestTime = PlayerPrefs.GetFloat("LowestTime", Mathf.Infinity);
    }

    public float GetLowestTime()
    {
        return lowestTime;
    }

    public void UpdateLowestTime(float time)
    {
        if (time < lowestTime)
        {
            lowestTime = time;


            PlayerPrefs.SetFloat("LowestTime", lowestTime);
        }
    }

    public void LoadCleanScene()
    {
        SceneManager.LoadScene("CleanScene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
