using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) {
            if (target.transform.position.x > transform.position.x)
            {
                transform.position = new Vector3(target.transform.position.x, transform.position.y, -10f);
            }

            if (target.transform.position.y > transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, target.transform.position.y, -10f);
            }

            if (target.transform.position.y < transform.position.y && transform.position.y > 0)
            {
                transform.position = new Vector3(transform.position.x, target.transform.position.y, -10f);
            }
        }
    }
}
