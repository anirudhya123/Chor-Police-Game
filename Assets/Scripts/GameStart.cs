using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private GameObject ControllerPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject police;
    [SerializeField] private GameObject thief;
    [SerializeField] private Vector3[] policePositions;
    [SerializeField] private Vector3[] thiefPositions;
    [SerializeField] private GameObject PoliceCursor;

    private void Start()
    {
        police.transform.position = policePositions[Random.Range(0,(policePositions.Length - 1))];
        thief.transform.position = thiefPositions[Random.Range(0,(thiefPositions.Length - 1))];

        if (GameManager.Multiplayer)
        {
            police.GetComponentInChildren<EnemyAI>().enabled = false;
            police.GetComponentInChildren<BodyControllers>().enabled = true;
            PoliceCursor.SetActive(true);
        }
        else if(GameManager.Singleplayer)
        {
            police.GetComponent<EnemyAI>().enabled = true;
            police.GetComponent<BodyControllers>().enabled = false;
            PoliceCursor.SetActive(false);
        }
        GameManager.Paused = true;
        GameManager.ThiefCaught = false;
        GameManager.TimeElapsed = false;

    }

    private void Update()
    {
        if(GameManager.Paused)
        {
            PausePanel.SetActive(true);
            ControllerPanel.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            PausePanel.SetActive(false);
            ControllerPanel.SetActive(true);
            Time.timeScale = 1;
        }
    }

}
