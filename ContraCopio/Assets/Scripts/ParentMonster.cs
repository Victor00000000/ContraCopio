using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMonster : MonoBehaviour
{

    public int points;
    GameMaster gm;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            BulletScript bul = other.GetComponent<BulletScript>();
            if (bul.target == "Enemy") {
                Destroy(other.gameObject);
                Die();
            }
        }
    }

    void Die() {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        gm.ScorePoins(points);
        Destroy(gameObject);
    }
}
