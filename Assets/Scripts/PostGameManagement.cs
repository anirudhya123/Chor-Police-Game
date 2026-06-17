using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostGameManagement : MonoBehaviour
{
    [SerializeField] GameObject TimeElapsed;
    [SerializeField] GameObject ThiefCaught;
    [SerializeField] GameObject ThiefWon;
    [SerializeField] GameObject PoliceWon;

    private void Start()
    {
        if (GameManager.TimeElapsed) 
            TimeElapsed.SetActive(true);
        else if(GameManager.ThiefCaught)
            ThiefCaught.SetActive(true);
        else if(GameManager.PoliceLost)
            ThiefWon.SetActive(true);
        else if(GameManager.ThiefLost)
            PoliceWon.SetActive(true);
    }
}
