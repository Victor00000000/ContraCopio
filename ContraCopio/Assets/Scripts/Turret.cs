using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ParentMonster
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float shootingSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", shootingSpeed, shootingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Shoot()
    {
        GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        BulletScript bul = go.GetComponent<BulletScript>();
        bul.SetDirection(Vector2.left);
        bul.target = "Player";
    }

}
