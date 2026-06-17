using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool Multiplayer ;
    public static bool Singleplayer;
    public static string character;
    public static bool Paused;
    public static bool TimeElapsed;
    public static bool ThiefCaught;
    public static bool ThiefLost;
    public static bool PoliceLost;
    public static int PoliceScore = 0;
    public static int ThiefScore = 0;
    public static bool mute = false;
    public static int level = 0;
    //public static bool inWater = false;

    [SerializeField] GameObject LoadingScreen;
    [SerializeField] TextMeshProUGUI fpsChecker;


    private void Awake()
    {
       level =  PlayerPrefs.GetInt("Hardness", 0);
        int sound = PlayerPrefs.GetInt("Sound", 1);
        mute = (sound == 1) ? false : true;
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }


    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            LoadingScreen.GetComponentInChildren<Slider>().value = operation.progress;
            Debug.Log("Progress->"+operation.progress);
            yield return null;
        }
    }

   

}
