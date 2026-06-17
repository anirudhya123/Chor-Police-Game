using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialManager : MonoBehaviour
{
    public int targetFrameRate = 60;
    public GameObject panelPrefab;

    private GameObject currentPanel;
    public static SpecialManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this object on scene change
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }

        Application.targetFrameRate = targetFrameRate;
    }

    void Start()
    {
        StartCoroutine(CheckInternetConnection());
    }

    IEnumerator CheckInternetConnection()
    {
        while (true)
        {
            bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;

            if (isConnected)
            {
                Debug.Log("Internet is connected.");
                if(currentPanel != null)
                {
                    Destroy(currentPanel);
                    FindObjectOfType<EventManager>().ReloadGame();
                }
            }
            else
            {
                Debug.Log("Internet is not connected.");
                if (currentPanel == null)  // Check if the panel is already instantiated
                {
                    InstantiatePanel();
                }
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void InstantiatePanel()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas != null)
        {
            currentPanel = Instantiate(panelPrefab, canvas.transform);  // Instantiate the panel as a child of the found Canvas
        }
        else
        {
            Debug.LogError("No Canvas found in the current scene to instantiate the panel.");
        }
    }
}
