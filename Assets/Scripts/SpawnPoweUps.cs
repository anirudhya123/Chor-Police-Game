using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnPoweUps : MonoBehaviour
{
    [SerializeField] GameObject clock;
    [SerializeField] GameObject apple;
    [SerializeField] GameObject toxin;

    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            Spawn(clock);
        }
        for (int i = 0; i < 2; i++)
        {
            Spawn(apple);
        }
        for (int i = 0; i < 2; i++)
        {
            Spawn(toxin);
        }
    }

    void Spawn(GameObject gameObject)
    {
        float spawnPosX = Random.Range(-150f,155f);
        float spawnPosY = Random.Range(-72,26);

        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0);
        GameObject newObject = Instantiate(gameObject,spawnPos, Quaternion.identity) as GameObject;
    }
}
