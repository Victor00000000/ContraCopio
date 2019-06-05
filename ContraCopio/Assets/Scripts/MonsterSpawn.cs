using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{

    public GameObject enemy;
    public float spawn = 1f;
    public float spawnTime = 3f;
    public Vector2 spawnPosition;
    private int spawnPositionX = 1;
    private int spawnPositionY = 1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn()
    {
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

}
