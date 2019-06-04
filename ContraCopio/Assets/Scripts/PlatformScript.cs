using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    public BoxCollider2D ownCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Physics2D.IgnoreCollision(ownCollider, collision.GetComponent<BoxCollider2D>());
        } else
        {
            Debug.Log(collision.name);
        }


    }
}
