﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonster : ParentMonster
{
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left *(speed * Time.deltaTime));
    }

}