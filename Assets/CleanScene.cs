using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;

public class CleanScene : MonoBehaviour
{
    public Container container; 
    
    public Transform containTr;
    public Transform necessaryPurposesBox;
    public Transform unnecessaryPurposesBox;
    public Text timerText;
    public Text lowestTimeText;
    public GameObject listNecessaryTxtPrefab;
    public GameObject[] purposePrefabs;
    public int numberOfPurposesToSpawn = 10;



    public Transform[] spawnPositions; 

    private Timer timer;
    private List<Vector3> usedSpawnPositions;
    private int sortedPurposeCount;


    private CameraController cameraController;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();

        timer = FindObjectOfType<Timer>();
        usedSpawnPositions = new List<Vector3>();
        sortedPurposeCount = 0;



        // Generate purposes
        for (int i = 0; i < numberOfPurposesToSpawn; i++)
        {
            GeneratePurpose();
        }
    }
    public void MoveToContainer(GameObject purpose)
    {
        purpose.transform.SetParent(container.transform);
        purpose.GetComponent<Rigidbody2D>().simulated = false;
    }

    private void Update()
    {
        if (IsAllPurposesSorted())
        {
            TransitionCameraAndBox();

        }

        if (AreAllPurposesInContainer())
        {
            timer.StopTimer();
        }

        float currentTime = timer.GetElapsedTime();
        timerText.text = "Time: " + currentTime.ToString("F2");



        if (Camera.main.transform.position.x >= 17)
        {
            print(Camera.main.transform.position.x);

            ResetPurposeRigidbodies();
            necessaryPurposesBox.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            necessaryPurposesBox.GetComponent<BoxCollider2D>().enabled = true;
        }


        GameManager gameManager = FindObjectOfType<GameManager>();
        float lowestTime = gameManager.GetLowestTime();
        lowestTimeText.text = "Lowest Time: " + lowestTime.ToString("F2");



    }
    private void TransitionCameraAndBox()
    {
        cameraController.SetAllPurposesSorted(true);


        if (necessaryPurposesBox != null)
        {
            Vector3 targetBoxPosition = new Vector3(cameraController.targetXPosition, necessaryPurposesBox.localPosition.y, necessaryPurposesBox.localPosition.z);
            necessaryPurposesBox.localPosition = Vector3.Lerp(necessaryPurposesBox.localPosition, targetBoxPosition, cameraController.transitionSpeed * Time.deltaTime);
        }
    }

    private void GeneratePurpose()
    {
        int randomIndex = Random.Range(0, purposePrefabs.Length);
        GameObject purposePrefab = purposePrefabs[randomIndex];

        Vector3 spawnPosition;
        if (spawnPositions.Length > 0)
        {
            int spawnIndex = GetRandomUnusedSpawnIndex();
            spawnPosition = spawnPositions[spawnIndex].position;
            usedSpawnPositions.Add(spawnPosition);
        }
        else
        {
            spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0f);
        }

        GameObject purpose = Instantiate(purposePrefab, spawnPosition, Quaternion.identity);
        purpose.GetComponent<Purpose>().Initialize(this);

        Purpose purposeComponent = purpose.GetComponent<Purpose>();
        if (purposeComponent != null && purposeComponent.IsNecessary)
        {
            GameObject listNecessaryTxt = Instantiate(listNecessaryTxtPrefab, listNecessaryTxtPrefab.transform.parent);
            TextMeshProUGUI necessaryTxt = listNecessaryTxt.GetComponent<TextMeshProUGUI>();
            necessaryTxt.text = purposePrefab.name;
            necessaryTxt.gameObject.SetActive(true);

            RectTransform necessaryTxtRectTransform = necessaryTxt.GetComponent<RectTransform>();
            necessaryTxtRectTransform.anchoredPosition += new Vector2(0f, -20f * necessaryTxt.transform.GetSiblingIndex());

            listNecessaryTxt.transform.SetParent(listNecessaryTxtPrefab.transform.parent);
        }
    }

    private void ResetPurposeRigidbodies()
    {
        for (int i = 0; i < necessaryPurposesBox.childCount; i++)
        {
            GameObject purpose = necessaryPurposesBox.GetChild(i).gameObject;
            Rigidbody2D rb = purpose.GetComponent<Rigidbody2D>();
            rb.simulated = true;
        }

        for (int i = 0; i < unnecessaryPurposesBox.childCount; i++)
        {
            GameObject purpose = unnecessaryPurposesBox.GetChild(i).gameObject;
            Rigidbody2D rb = purpose.GetComponent<Rigidbody2D>();
            rb.simulated = true;
        }
    }

    private int GetRandomUnusedSpawnIndex()
    {
        if (usedSpawnPositions.Count >= spawnPositions.Length)
        {
   
            usedSpawnPositions.Clear();
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, spawnPositions.Length);
        } while (usedSpawnPositions.Contains(spawnPositions[randomIndex].position));

        return randomIndex;
    }

    public void StoreNecessaryPurpose(GameObject purpose)
    {
        Purpose purposeComponent = purpose.GetComponent<Purpose>();
        if (purposeComponent != null && purposeComponent.IsNecessary)
        {
            purpose.transform.SetParent(necessaryPurposesBox);
            purpose.GetComponent<Rigidbody2D>().simulated = false; 


            purposeComponent.SetTriggered(true);

            sortedPurposeCount++;
        }
        else
        {
            purpose.transform.SetParent(unnecessaryPurposesBox);
            purpose.GetComponent<Rigidbody2D>().simulated = false;
        }


    }

    public void TransitionToMainScene()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.UpdateLowestTime(timer.GetElapsedTime());
        SceneManager.LoadScene("MainScene");
    }


    private bool IsAllPurposesSorted()
    {
        int necessaryCount = necessaryPurposesBox.childCount;
        int unnecessaryCount = unnecessaryPurposesBox.childCount;
        int totalPurposes = necessaryCount + unnecessaryCount;

        bool allPurposesSorted = totalPurposes >= numberOfPurposesToSpawn;

        cameraController.SetAllPurposesSorted(allPurposesSorted);

        return allPurposesSorted;
    }

    private bool AreAllPurposesInContainer()
    {
        int unnecessaryCount = unnecessaryPurposesBox.childCount;
        int containerCount = containTr.transform.childCount;
        int totalPurposes = containerCount + unnecessaryCount;


        return totalPurposes >= numberOfPurposesToSpawn;
    }
}
