using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoliceText : MonoBehaviour
{
    TextMeshProUGUI m_TextMeshProUGUI;

    private void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        int score = GameManager.PoliceScore;
        m_TextMeshProUGUI.text = string.Format("{0:00}", score);
    }
}
