using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{

    public GameObject enemy;
    public float spawn = 1f;
    public float spawnTime = 3f;
    private Vector2 spawnPosition;
    private Vector2 target;
    private Rigidbody2D enemyRigidbody;

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
    spawnPosition.x = 8;
    spawnPosition.y = 1;

    Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}
