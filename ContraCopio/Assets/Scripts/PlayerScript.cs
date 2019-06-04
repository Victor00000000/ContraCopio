using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    Vector2 inputVector;
    public float speed = 5;
    public float jumpingSpeed = 5;
    Rigidbody2D rb2d;
    Animator animator;
    bool jumping = false;

    // Fireing
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public Transform bulletParent;
    public float gunCooldown = 1f;
    float gunCooldownTimer;

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

        if (jumping && (rb2d.velocity.sqrMagnitude < 0.0001f))
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position = transform.position - Vector3.down * 0.1f;
            }
            animator.SetBool("Sit", true);
        } else
        {
            animator.SetBool("Sit", false);
        }

        // Fireing
        gunCooldownTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && gunCooldownTimer < 0) Shoot();
    }

    void Move()
    {
        transform.Translate(inputVector * speed * Time.deltaTime);
        transform.Translate(Vector2.down * Time.deltaTime * 10f);
    }

    void Shoot() {
        bool right = Input.GetKey(KeyCode.D);
        bool left = Input.GetKey(KeyCode.A);
        bool up = Input.GetKey(KeyCode.W);
        bool down = Input.GetKey(KeyCode.S);
        
        gunCooldownTimer = gunCooldown;
        GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity, bulletParent);
        BulletScript bul = go.GetComponent<BulletScript>();
        
        if (down) { // Check down inputs
            if (right) bul.SetDirection(Vector2.right + Vector2.down);
            else if (left) bul.SetDirection(Vector2.left + Vector2.down);
            else bul.SetDirection(Vector2.down);
        } else if (up) { // Check up inputs
            if (right) bul.SetDirection(Vector2.right + Vector2.up);
            else if (left) bul.SetDirection(Vector2.left + Vector2.up);
            else bul.SetDirection(Vector2.up);
        } else if (transform.localScale.x < 0) { // If facing right
            bul.SetDirection(Vector2.right);
        } else { // Else it's facing left
            go.GetComponent<BulletScript>().SetDirection(Vector2.left);
        }
    }
}
