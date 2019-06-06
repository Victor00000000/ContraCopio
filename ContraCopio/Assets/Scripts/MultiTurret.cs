using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTurret : ParentMonster {
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float shootingSpeed = 3f;
    public Transform player;

    public float speed;
    public float shootingDelay; // time between shots in seconds
    public float lastTimeShot;
    public float bulletSpeed;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("Shoot", shootingSpeed, shootingSpeed);
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > lastTimeShot + shootingDelay) {
            
        }
    }

    void FixedUpdate() {
        // Figure out which way to move to approach the player
        
        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void Shoot() {
        direction = (player.position - transform.position).normalized;
        // Shoot
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        // Make a bullet
        GameObject bul = Instantiate(bulletPrefab, bulletSpawnPoint.position, q);

        //bul.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, bulletSpeed));
        BulletScript scr = bul.GetComponent<BulletScript>();
        scr.target = "Player";

        /*GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        BulletScript bul = go.GetComponent<BulletScript>();
        bul.SetDirection(Vector2.left);
        bul.target = "Player";*/
    }

}
