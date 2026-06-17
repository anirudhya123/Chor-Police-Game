using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(CircleCollider2D))]
public class BodyControllers : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private DynamicJoystick joyStick;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject leaf;
    [SerializeField] private GameObject ripple;
    [SerializeField] private float movingSpeed;

    public bool trapped = false;
    public bool onWood = false;
    public bool onWater = false;
    public bool onMud = false;
    
    AudioManager manager;

    private void Start()
    {
        manager = FindAnyObjectByType<AudioManager>();
        GameObject canvas = GameObject.Find("Canvas");
        if(joyStick == null)
        {
            Transform firstChild = canvas.transform.GetChild(0);
            joyStick = firstChild.GetComponentInChildren<DynamicJoystick>();
        }
    }

    private void FixedUpdate()
    {
        float effectiveSpeed;
        // adjust Speed
        if (trapped)
        {
            effectiveSpeed = movingSpeed / 2;
        }
        else
        {
            effectiveSpeed = movingSpeed;
        }


        rigidBody.linearVelocity = new Vector3(joyStick.Horizontal * effectiveSpeed, joyStick.Vertical * effectiveSpeed,0);
        if(joyStick.Horizontal != 0 || joyStick.Vertical !=0) {
            transform.rotation = Quaternion.LookRotation(Vector3.forward,rigidBody.linearVelocity);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Apple")
        {
            movingSpeed += 5;
            Destroy(collision.gameObject);
            manager.Play("PowerUp");
        }
        if(collision.gameObject.tag == "Toxic")
        {
            movingSpeed -= 5;
            Destroy(collision.gameObject);
            manager.Play("PowerDown");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pond")
        {
            //manager.Play("Trap");
            trapped = true;
            if(collision.transform.name == "Pond") onWater = true;
            else onMud = true;
        }
        if (collision.gameObject.tag == "Wood")
        {
            onWood = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pond")
        {
            trapped = false;
            if (collision.transform.name == "Pond") onWater = false;
            else onMud = false;
        }
        if (collision.gameObject.tag == "Wood")
            onWood = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Green")
        {
            //Debug.Log("Is it Working or not");
            GameObject newObj = Instantiate(leaf, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            Destroy(newObj,2f);
        }
    }
}
