using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentMonster : MonoBehaviour
{

    public int points;
    GameMaster gm;
    Camera cam;

    // Start is called before the first frame update
    public void Start()
    {
        cam = Camera.main;
        transform.gameObject.tag = "Enemy";
    }

    // Update is called once per frame
    public void Update() {
        if (cam.WorldToScreenPoint(transform.position).x < -200) {
            Destroy(gameObject);
        }
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
