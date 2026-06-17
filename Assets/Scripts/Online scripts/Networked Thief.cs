using UnityEngine;
using Fusion;
public class NetworkedThief : NetworkBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Police"))
        {
            Player.LocalInstance.StopPlayerMovement();
            // Interval Page
            if(JoyStickManager.Instance.currentRound < JoyStickManager.Instance.totalRound)
            {
                FindObjectOfType<NetworkManager>().ResetPlayerReady();
                //FindObjectOfType<NetworkManager>().DespawnPrefabs();
                JoyStickManager.Instance.heading.text = "You Got Caught.";
                JoyStickManager.Instance.nextRoundMessage.text = "Get Ready for Next Round";
                JoyStickManager.Instance.readyBtn.gameObject.SetActive(true);
                JoyStickManager.Instance.readyBtn.onClick.AddListener(FindObjectOfType<NetworkManager>().SetPlayerReady);
                JoyStickManager.Instance.waitingSection.SetActive(false);
                JoyStickManager.Instance.joystick.gameObject.SetActive(false);
                JoyStickManager.Instance.intermediatePanel.SetActive(true);
            }
            else
            {
                Debug.Log("Game Over");
                FindObjectOfType<NetworkManager>().GameOver();
            }
        }
    }
}
