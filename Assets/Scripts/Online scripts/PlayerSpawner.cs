using Fusion;
using System.Linq;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    //public GameObject policePrefab, thiefPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        Debug.Log("Am I even getting called");
        Runner.GetComponent<NetworkManager>().UpdateJoinPlayer();
        //if (player == Runner.LocalPlayer)
        //{

        //    if(Runner.ActivePlayers.Count() <= 1)
        //    {
        //        NetworkObject playerobj =  Runner.Spawn(policePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        //        playerobj.gameObject.name = "Police";
        //        Runner.SetPlayerObject(player,playerobj);
        //    }
        //    else
        //    {
        //        NetworkObject playerobj =  Runner.Spawn(thiefPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        //        playerobj.gameObject.name = "Chor";
        //        Runner.SetPlayerObject(player,playerobj);
        //    }
        //        Debug.Log("Prefab Succesfully Created");
        //}
    }
}