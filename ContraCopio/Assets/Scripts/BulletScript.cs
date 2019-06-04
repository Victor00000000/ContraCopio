using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    Vector2 direction;
    float speed = 5;

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

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag ("Enemy")) {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        Debug.Log("Tunnistettu");
    }    
}
