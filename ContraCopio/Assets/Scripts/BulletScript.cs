using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public Vector2 direction;
    public float speed = 5;
    public string target = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
