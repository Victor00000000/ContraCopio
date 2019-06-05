using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public Transform bulletParent;
    public float shootingSpeed = 3f;
    public Vector2 spawnPosition;
    public float spawn = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", shootingSpeed, shootingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Spawn()
    {
        GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        BulletScript bul = go.GetComponent<BulletScript>();
        bul.SetDirection(Vector2.left);
        bul.target = "Player";
    }

}
