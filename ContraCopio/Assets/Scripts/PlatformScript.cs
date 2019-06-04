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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Physics2D.IgnoreLayerCollision(0,8);
        }
        else
        {
            Debug.Log(collision.gameObject.name);
        }
    }

}
