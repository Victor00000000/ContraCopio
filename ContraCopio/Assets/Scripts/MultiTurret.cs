using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTurret : ParentMonster {
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float shootingSpeed = 2f;
    public Transform player;
    public Transform gun;

    public float bulletSpeed;
    public Vector2 direction;
    Quaternion q;

    // Start is called before the first frame update
    void Start() {
        base.Start();
        player = GameObject.FindWithTag("Player").transform;
        InvokeRepeating("Shoot", 0.1f, shootingSpeed);
    }

    // Update is called once per frame
    void Update() {
        base.Update();
    }

    void FixedUpdate() {
        // Figure out which way to move to approach the player
        
        //gun.LookAt(direction);
        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void Shoot() {

        direction = (player.position - transform.position).normalized;
        // Shoot
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        q = Quaternion.AngleAxis(angle, Vector3.forward);
        gun.transform.rotation = q;

        // Make a bullet
        GameObject bul = Instantiate(bulletPrefab, bulletSpawnPoint.position, q);
        bul.transform.rotation = q;

        bul.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, bulletSpeed));
        BulletScript scr = bul.GetComponent<BulletScript>();
        scr.target = "Player";

        /*GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        BulletScript bul = go.GetComponent<BulletScript>();
        bul.SetDirection(Vector2.left);
        bul.target = "Player";*/
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            BulletScript bul = other.GetComponent<BulletScript>();
            if (bul.target == "Enemy") {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
        }
    }

}
