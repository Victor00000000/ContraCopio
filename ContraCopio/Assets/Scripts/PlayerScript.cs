using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    Vector2 inputVector;
    public float speed;
    public float jumpingSpeed;
    Rigidbody2D rb2d;
    Animator animator;
    bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        InputReader();
        Move();

        if (animator.GetBool("Jumping") && (rb2d.velocity.sqrMagnitude < 0.0001f))
        {
            jumping = false;
            animator.SetBool("Jumping", false);
        }
    }

    void InputReader()
    {
        inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
            transform.localScale = new Vector3(1, 1, 1);
        } else if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.W) && jumping == false)
        {
            rb2d.velocity = new Vector2(0, jumpingSpeed);
            animator.SetBool("Jumping", true);
            jumping = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Sit", true);
        } else
        {
            animator.SetBool("Sit", false);
        }
    }

    void Move()
    {
        transform.Translate(inputVector * speed * Time.deltaTime);

    }
}
