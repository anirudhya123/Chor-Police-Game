using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TImeControl : MonoBehaviour
{
    Timer timer;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
    }
    
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.tag == "Thief")
            { 
                timer.UpdateTime(5f, true);
                // time down sound effect
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == "Police")
            {
                timer.UpdateTime(5f, false);
                // time up sound effect
                Destroy(gameObject);
            }
            else
                return;
        }
    }
    private void Update()
    {
        if(timer == null)
        {
            Debug.Log("timer not found");
            return;
        }
        if(timer.RequestTime() < 10f)
        {
            Destroy(gameObject);
        }
    }
}
