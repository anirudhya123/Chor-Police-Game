using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlHardness : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI levelText;
    string[] values = { "Easy", "Fun", "Hard" };
    private void Update()
    {
        levelText.text = values[GameManager.level];
    }

    public void UpdateValue(bool tag)
    {
        if (tag && GameManager.level < 2)
        {
            GameManager.level++;
            PlayerPrefs.SetInt("Hardness",GameManager.level);
            PlayerPrefs.Save();
        }
        else if (!tag && GameManager.level > 0) { 
            GameManager.level--; 
            PlayerPrefs.SetInt("Hardness",GameManager.level);
            PlayerPrefs.Save();
        }
        else return;//play Wrong Sound
    }
}
