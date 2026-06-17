using UnityEngine;
using Fusion;

public class NetwokedPolice : NetworkBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Thief"))
        {
            Player.LocalInstance.StopPlayerMovement();
           if(JoyStickManager.Instance.currentRound < JoyStickManager.Instance.totalRound)
            {
                Debug.Log("Chor Catch Caught Caught");
                FindObjectOfType<NetworkManager>().ResetPlayerReady();
                //FindObjectOfType<NetworkManager>().DespawnPrefabs();
                JoyStickManager.Instance.heading.text = "You Got Caught.";
                JoyStickManager.Instance.nextRoundMessage.text = "Get Ready for Next Round";
                JoyStickManager.Instance.readyBtn.gameObject.SetActive(true);
                JoyStickManager.Instance.readyBtn.onClick.AddListener(FindObjectOfType<NetworkManager>().ResetPlayerReady);
                JoyStickManager.Instance.waitingSection.SetActive(false);
                JoyStickManager.Instance.joystick.gameObject.SetActive(false);
                JoyStickManager.Instance.intermediatePanel.SetActive(true);
            }
            else
            {
                Debug.Log("GameOver Time");
                FindObjectOfType<NetworkManager>().GameOver();
            }
        }
    }
}
