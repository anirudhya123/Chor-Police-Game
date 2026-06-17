using UnityEngine;
using Fusion;
using TMPro;
using System.Linq;
public class NetworkManager : SimulationBehaviour, IPlayerJoined
{
    public TMP_InputField roomName;
    public static NetworkRunner runnerInstance;
    public int joinedPlayer;
    public bool isPolice;
    public GameObject policePrefab, thiefPrefab, playerStatePrefab;
    bool spawnned;
    PlayerRef myPlayer;
    public bool policeReady;
    public bool chorReady;
    bool nextRound = false;
    float ticker;
    private void Awake()
    {
        runnerInstance = gameObject.AddComponent<NetworkRunner>();
        if (runnerInstance == null)
            runnerInstance = gameObject.AddComponent<NetworkRunner>();
        

        joinedPlayer = 0;
        spawnned = false;
    }

    public void CheckSession()
    {
        if (!string.IsNullOrEmpty(roomName.text))
        {
            CreatePrivateSession();
        }
        else
        {
            CreateRandomSession();
        }
    }

    public void CreateRandomSession()
    {
        //string randomRoom = $"Room {UnityEngine.Random.Range(1000, 9999)}";
        runnerInstance.StartGame(new StartGameArgs()
        {
            Scene = SceneRef.FromIndex(1),
            GameMode = GameMode.Shared,
            PlayerCount = 2
        });
    }

    public void CreatePrivateSession()
    {
        //string randomRoom = $"Room {UnityEngine.Random.Range(1000, 9999)}";
        runnerInstance.StartGame(new StartGameArgs()
        {
            SessionName = roomName.text,
            Scene = SceneRef.FromIndex(1),
            GameMode = GameMode.Shared,
            PlayerCount = 2
        });
    }

    public void UpdateJoinPlayer()
    {
        Debug.Log("Player Joined " + Runner.LocalPlayer.RawEncoded);
        joinedPlayer = Runner.ActivePlayers.Count();
    }
    
    public override void FixedUpdateNetwork()
    {
        
        if(joinedPlayer == 2 && !spawnned)
        {
            Debug.Log("Spawnn Players");
            SpawnPlayers(myPlayer);
            JoyStickManager.Instance.loadingScene.SetActive(false);
            spawnned = true;
        }
        
    }

    public void PlayerJoined(PlayerRef player)
    {
        if(player == Runner.LocalPlayer)
        {
            if(Runner.ActivePlayers.Count() == 1)
            {
                isPolice = true;
                myPlayer = player;
            }
            else if(Runner.ActivePlayers.Count() == 2)
            {
                isPolice = false;
                myPlayer = player;
            }
            

        }
        UpdateJoinPlayer();
    }

    public void SpawnPlayers(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {

            if (isPolice)
            {
                NetworkObject playerobj = Runner.Spawn(policePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                playerobj.gameObject.name = "Police";
                Runner.SetPlayerObject(player, playerobj);
            }
            else
            {
                NetworkObject playerobj = Runner.Spawn(thiefPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                playerobj.gameObject.name = "Chor";
                Runner.SetPlayerObject(player, playerobj);
            }
            Debug.Log("Prefab Succesfully Created");
        }
    }

    public void SetPlayerReady()
    {
        Debug.Log("Player Setting");
        Player.LocalInstance.LocalPlayerClickedReady();
        JoyStickManager.Instance.readyBtn.gameObject.SetActive(false);
        JoyStickManager.Instance.waitingSection.SetActive(true);
        if (isPolice)
        {
            Debug.Log($"I am Police | Police Ready: {policeReady} | Chor Ready: {chorReady}");
        }
        else
        {
            Debug.Log($"I am Chor | Chor Ready: {chorReady} | Police Ready: {policeReady}");
        }
        nextRound = true;
    }


    public void ResetPlayerReady()
    {
        policeReady = false;
        chorReady = false;
    }
    
    public void StartNextRound()
    {
        Debug.Log("Start Next Round");
        JoyStickManager.Instance.currentRound++;
        Player.LocalInstance.Relocate();
        JoyStickManager.Instance.policeReady = false;
        JoyStickManager.Instance.chorReady = false;
        Invoke(nameof(EnableJoystick), 2f);
    }

    public void EnableJoystick()
    {
        Debug.Log("Let's enable the Joystick");
        JoyStickManager.Instance.joystick.gameObject.SetActive(true);
        JoyStickManager.Instance.intermediatePanel.SetActive(false);
    }

    public void GameOver()
    {
        JoyStickManager.Instance.gameOverPanel.SetActive(true);
    }

    public void Down()
    {
        Debug.Log("Shutting Down");
        Runner.Shutdown();
    }
}
