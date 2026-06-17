using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThiefAnimControl : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] GameObject Wave;
    [SerializeField] GameObject MudPatch;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    public void Walking()
    {

        if(transform.GetComponentInParent<BodyControllers>() != null)
        {
            if(transform.GetComponentInParent<BodyControllers>().trapped) {
                GameObject newObj;
                if(transform.GetComponentInParent<BodyControllers>().onWater) 
                    newObj = Instantiate(Wave, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                else 
                    newObj = Instantiate(MudPatch, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                audioManager.Play("Trap");
                Destroy(newObj,1f);
            }
            else if (transform.GetComponentInParent<BodyControllers>().onWood)
            {
                audioManager.Play("Wood");
            }
            else
            {
                audioManager.Play("Grass");
            }
        }
        else
        {
            if (transform.GetComponentInParent<Player>().trapped)
            {
                GameObject newObj;
                if (transform.GetComponentInParent<Player>().onWater)
                    newObj = Instantiate(Wave, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                else
                    newObj = Instantiate(MudPatch, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                audioManager.Play("Trap");
                Destroy(newObj, 1f);
            }
            else if (transform.GetComponentInParent<Player>().onWood)
            {
                audioManager.Play("Wood");
            }
            else
            {
                audioManager.Play("Grass");
            }
        }


    }


}
