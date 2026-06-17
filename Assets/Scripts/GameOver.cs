using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.tag == "Thief")
        {
            //Debug.Log("Theif Caught");
            GameManager.PoliceScore++;
            if(GameManager.PoliceScore == 3)
            {
                GameManager.PoliceScore = 0;
                GameManager.ThiefScore = 0;
                GameManager.ThiefLost = true;
            }
            else
            { 
                GameManager.ThiefCaught = true;
            }
            SceneManager.LoadScene("Post Game");
        }
    }

}
