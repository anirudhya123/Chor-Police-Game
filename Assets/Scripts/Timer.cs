using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float minSize = 20f; // Minimum font size
    public float maxSize = 40f; // Maximum font size
    public float duration = 1f; // Duration of one full size change cycle
    [SerializeField] float remainingTime;
    AudioManager audioManager;
    [SerializeField] Color[] colors;
    Color startingColor;
    [SerializeField] AudioSource _audio;

    private Coroutine animationCoroutine;

    private void Start()
    {
        GameManager.TimeElapsed = false;
        audioManager = FindObjectOfType<AudioManager>();
        _audio.mute = GameManager.mute;
        startingColor = timerText.color;
    }
    private void Update()
    {

        if (remainingTime > 0)
            remainingTime -= Time.deltaTime;
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            Debug.Log("Time Up");
            GameManager.ThiefScore++;
            if(GameManager.ThiefScore == 3)
            {
                GameManager.PoliceLost = true;
                GameManager.ThiefScore = 0;
                GameManager.PoliceScore = 0;
            }
            else
                GameManager.TimeElapsed = true;

            SceneManager.LoadScene("Post Game");
        }

        bool verify = remainingTime < 30f;
        if (verify && animationCoroutine == null)
        {
            animationCoroutine = StartCoroutine(AnimateTextSize());
        }
        else if (!verify && animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }



        int min = Mathf.FloorToInt(remainingTime/60);
        int sec = Mathf.FloorToInt(remainingTime%60);
        timerText.text = string.Format("{0:00}:{1:00}",min,sec);
    }

    public float RequestTime()
    {
        return remainingTime;
    }

    public void UpdateTime(float amount,bool playerType)
    {
        if (playerType)
        {
            remainingTime -= amount;
            audioManager.Play("Time");
            SetColor(colors[1]);
        }
        else
        {
            remainingTime += amount;
            audioManager.Play("Time");
            SetColor(colors[0]);
        }
    }

    void SetColor(Color color)
    {
        timerText.color = color;
        Invoke("ResetColor", 2f);
    }

    void ResetColor()
    {
        timerText.color = startingColor;
    }

    private IEnumerator AnimateTextSize()
    {
        while (true)
        {
            // Increase font size
            yield return StartCoroutine(ChangeTextSize(maxSize));
            // Decrease font size
            yield return StartCoroutine(ChangeTextSize(minSize));
        }
    }

    private IEnumerator ChangeTextSize(float targetSize)
    {
        float startSize = timerText.fontSize;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            timerText.fontSize = Mathf.Lerp(startSize, targetSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        timerText.fontSize = targetSize;
    }
}
