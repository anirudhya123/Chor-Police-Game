
using UnityEngine;

public class NoInternet : MonoBehaviour
{
    [SerializeField] EventManager eventManager;


    private void Update()
    {
        Check();
    }

    public void Check()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            
            eventManager.ReloadGame();
        }
        else
        {
            Debug.Log("Kindly Connect Internet");
        }
    }

}
