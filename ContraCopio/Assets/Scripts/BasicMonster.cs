using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonster : ParentMonster
{
    public float speed = 1f;
    public Vector2 dir = Vector2.left;

    // Update is called once per frame
    void Update()
    {
        base.Update();
        transform.Translate(dir *(speed * Time.deltaTime));
    }

}