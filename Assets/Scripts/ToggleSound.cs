using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSound : MonoBehaviour
{
    [SerializeField] GameObject on;
    [SerializeField] GameObject off;


    private void Start()
    {
        if (GameManager.mute)
        {
            off.SetActive(true);
            on.SetActive(false);
        }
        else
        {
            off.SetActive(true);
            on.SetActive(false);
        }
    }

    public void Toggle(bool flag) {
        if (flag) { 
            GameManager.mute = false; 
            off.SetActive(true); 
            on.SetActive(false);
            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.Save();
        }
        else if (!flag) { 
            GameManager.mute = true; 
            on.SetActive(true); 
            off.SetActive(false); 
            PlayerPrefs.SetInt("Sound", 0);
            PlayerPrefs.Save();
            
        }
        else return; // wrong buzzer
    }
}
