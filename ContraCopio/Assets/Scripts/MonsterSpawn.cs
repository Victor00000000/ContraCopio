using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{

    public GameObject enemy;
    Transform player;
    public float spawnAmount = 1;
    public float spawnInterval = 3f; // In seconds
    int spawned;
    public bool spawnStarted;
    float dist;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {
        if (!spawnStarted) {
            dist = transform.position.x - player.position.x;

            if (dist < 11) {
                Spawn();
                spawnStarted = true;
            }
        }
    }

    void Spawn() { 
        Instantiate(enemy, transform.position, Quaternion.identity);
        spawned++;

        if (spawned >= spawnAmount) {
            CancelInvoke();
            Destroy(gameObject);
        } else {
            Invoke("Spawn", spawnInterval);
        }
    }

}
