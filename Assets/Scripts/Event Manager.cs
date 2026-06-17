using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameObject LandingPage;
    [SerializeField] private GameObject PlayMode;
    [SerializeField] private GameObject Player;
    AudioManager aud;
    GameManager gameManager;

    private void Start()
    {
        aud = FindAnyObjectByType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();  
    }

    public void ChoosePlayMode(bool tag)
    {
        // click sound effect for each 
        aud.Play("Click");
        LandingPage.SetActive(!tag);
        PlayMode.SetActive(tag);
        //Player.SetActive(false);
    }

    public void ToggleSettings(bool toggle)
    {
        aud.Play("Click");
        Player.SetActive(toggle);
        LandingPage.SetActive(!toggle);
    }

    public void MultiPlayer()
    {
        aud.Play("Click");
        GameManager.Multiplayer = true;
        GameManager.Singleplayer = false;
        //SceneManager.LoadScene("MainGame");
        gameManager.LoadScene(1);
    }

    public void SinglePLayer()
    {
        aud.Play("Click");
        GameManager.Singleplayer = true;
        GameManager.Multiplayer = false;
        //SceneManager.LoadScene("MainGame");
        gameManager.LoadScene(1);

    }

    public void Play()
    {
        aud.Play("Click");
        GameManager.Paused = false;
    }
    public void Pause()
    {
        aud.Play("Click");
        GameManager.Paused = true;
    }

    public void ResetGame()
    {
        aud.Play("Click");
        gameManager.LoadScene(1);
    }

    public void ReloadGame()
    {
        aud.Play("Click");
        GameManager.Singleplayer = false;
        GameManager.Multiplayer = false;
        SceneManager.LoadScene("MenuScene");
    }

    

}
