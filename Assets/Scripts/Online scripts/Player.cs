using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
using Cinemachine;

//[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Player : NetworkBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    public DynamicJoystick joyStick;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject leaf;
    [SerializeField] private GameObject ripple;
    [SerializeField] private float movingSpeed;

    public bool trapped = false;
    public bool onWood = false;
    public bool onWater = false;
    public bool onMud = false;

    AudioManager manager;

    public string role;
    public static Player LocalInstance;



    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            // Only do this for the local player
            CinemachineVirtualCamera vCam = FindObjectOfType<CinemachineVirtualCamera>();
            if (vCam != null)
            {
                vCam.Follow = transform;
            }
            if(JoyStickManager.Instance.joystick != null)
                joyStick = JoyStickManager.Instance.joystick;

            LocalInstance = this;
        }
        

    }

    private void Start()
    {
        manager = FindAnyObjectByType<AudioManager>();

        Debug.Log($"Player Info: " +
          $"\nHasStateAuthority: {HasStateAuthority}" +
          $"\nHasInputAuthority: {HasInputAuthority}" +
          $"\nObject ID: {Object.Id}" +
          $"\nNetwork Role: {Runner.Mode}");
    }

    public override void FixedUpdateNetwork()
    {

        //Debug.Log("FixedUpdateNetwork() is running!");

        if (HasStateAuthority == false)
        {
            Debug.Log("No Authority!");
            return;
        }

        
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

        

        rigidBody.linearVelocity = new Vector3(joyStick.Horizontal * effectiveSpeed, joyStick.Vertical * effectiveSpeed, 0);
        if (joyStick.Horizontal != 0 || joyStick.Vertical != 0)
        {
            Debug.Log("Force is apply");
            transform.rotation = Quaternion.LookRotation(Vector3.forward, rigidBody.linearVelocity);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Apple")
        {
            movingSpeed += 5;
            Destroy(collision.gameObject);
            manager.Play("PowerUp");
        }
        if (collision.gameObject.tag == "Toxic")
        {
            movingSpeed -= 5;
            Destroy(collision.gameObject);
            manager.Play("PowerDown");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pond")
        {
            //manager.Play("Trap");
            trapped = true;
            if (collision.transform.name == "Pond") onWater = true;
            else onMud = true;
        }
        if (collision.gameObject.tag == "Wood")
        {
            onWood = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pond")
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
        if (collision.gameObject.tag == "Green")
        {
            //Debug.Log("Is it Working or not");
            GameObject newObj = Instantiate(leaf, new Vector2(transform.position.x, transform.position.y), transform.rotation);
            Destroy(newObj, 2f);
        }
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_NotifyPoliceReady()
    {
        if (!Object.HasStateAuthority)
        {
            Debug.Log("RPC Police is ready on the other player.");
            JoyStickManager.Instance.policeReady = true;
            PlayerReadyCheck();
        }
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All)]
    public void RPC_NotifyChorReady()
    {
        if (!Object.HasStateAuthority)
        {
            Debug.Log("RPC Chor is ready on the other player.");
            JoyStickManager.Instance.chorReady = true;
            PlayerReadyCheck();
        }
    }


    public void LocalPlayerClickedReady()
    {
        if (FindObjectOfType<NetworkManager>().isPolice)
        {
            JoyStickManager.Instance.policeReady = true;
            LocalInstance.RPC_NotifyPoliceReady();
            PlayerReadyCheck();
        }
        else
        {
            JoyStickManager.Instance.chorReady = true;
            LocalInstance.RPC_NotifyChorReady();
            PlayerReadyCheck();
        }
    }

    public void Relocate()
    {
        Vector3 newPosition = new(Random.Range(-15, 15), Random.Range(-15, 15), 0);

        string role = FindObjectOfType<NetworkManager>().isPolice ? "Police" : "Chor";

        if (HasStateAuthority)
        {
            transform.position = newPosition;
            GetComponent<NetworkTransform>().Teleport(newPosition);
            Debug.Log($"{role}'s new location is {newPosition}. and current location is {transform.position}..");
        }

    }

    public void PlayerReadyCheck()
    {
        if(JoyStickManager.Instance.chorReady && JoyStickManager.Instance.policeReady)
        {
            Debug.Log("This is the new Function to check the REady");
            FindObjectOfType<NetworkManager>().StartNextRound();
        }
    }

    public void StopPlayerMovement()
    {
        rigidBody.linearVelocity = Vector3.zero;
    }
}
