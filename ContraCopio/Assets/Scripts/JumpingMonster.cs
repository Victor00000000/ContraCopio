using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingMonster : MonoBehaviour
{

    public float speed = 1f;

    public float min = 0f;
    public float max = 10f;
    public float randomUp = 1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left *(speed * Time.deltaTime));
        randomUp = Random.Range(min, max);
        transform.Translate(Vector2.up * randomUp * Time.deltaTime);
    }

}
