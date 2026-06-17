using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JoyStickManager : MonoBehaviour
{
    public static JoyStickManager Instance { get; private set; }

    //public DynamicJoystick policeJoystick;
    //public DynamicJoystick thiefJoystick;

    public DynamicJoystick joystick;
    public GameObject loadingScene;


    [Header("Intermidaite Panel Elements")]
    public GameObject intermediatePanel;
    public TextMeshProUGUI heading;
    public TextMeshProUGUI nextRoundMessage;
    public Button readyBtn;
    public GameObject waitingSection;


    [Header("Game over Pnale Elements")]
    public GameObject gameOverPanel;

    public bool policeReady;
    public bool chorReady;


    public int totalRound = 3;
    public int currentRound = 0;
    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // Optional
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentRound = 0;
        joystick.gameObject.SetActive(true);
        loadingScene.SetActive(true);
        policeReady = false;
        chorReady = false;
    }

    public void HomeBoy()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<NetworkManager>().Down();
    } 
}
