using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    GameMaster gm;
    SpriteRenderer sr;
    Camera camera;
    Vector2 inputVector;
    public float speed = 5;
    public float jumpingSpeed = 5;
    Rigidbody2D rb2d;
    Animator animator;
    bool jumping = false;
    bool collision = true;
    float fallingTimer;
    public Transform groundCheck;
    Color spawnColor;

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
        bulletParent = GameObject.Find("Bullets").transform;
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        camera = Camera.main;

        spawnColor = Color.white;
        spawnColor.a = 0.3f;
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        if (colliders != null)
        {
            Debug.Log("collider not null");
            foreach(Collider2D col in colliders)
            {
                if (col.gameObject != gameObject)
                {
                    jumping = false;
                    animator.SetBool("Jumping", false);
                }
            }
            
        } else
        {
            Debug.Log("in air");
            jumping = true;
            animator.SetBool("Jumping", true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        fallingTimer -= Time.deltaTime;

        InputReader();
        Move();

        if (fallingTimer < 0 && Physics2D.GetIgnoreLayerCollision(0,8))
        {
            Physics2D.IgnoreLayerCollision(0, 8, false);
        }
    }

    void InputReader()
    {
        inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Y) && jumping == false)
        {
            rb2d.velocity = new Vector2(0, jumpingSpeed);
            animator.SetBool("Jumping", true);
            jumping = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                fallingTimer = 0.5f;
                Physics2D.IgnoreLayerCollision(0, 8);
                rb2d.velocity = Vector2.zero;
            }
            animator.SetBool("Sit", true);
        } else
        {
            animator.SetBool("Sit", false);
        }

        // Fireing
        gunCooldownTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.G) && gunCooldownTimer < 0)
            Shoot();
    }

    void Move()
    {
        transform.Translate(inputVector * speed * Time.deltaTime);
        //transform.Translate(Vector2.down * Time.deltaTime * 10f);
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
        } else if (transform.localScale.x > 0) { // If facing right
            bul.SetDirection(Vector2.right);
        } else { // Else it's facing left
            go.GetComponent<BulletScript>().SetDirection(Vector2.left);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Ground")) {
            jumping = false;
            animator.SetBool("Jumping", false);
        } else if (other.collider.CompareTag("Death") ||
                  other.collider.CompareTag("Enemy")) {
            if (collision)
                Die();
        }
    }

    void Die() {
        Vector2 newPos = new Vector2(-50000, 0);
        transform.position = newPos;
        collision = false;
        sr.color = spawnColor;
        gm.UpdateLives(-1);
        if (gm.lives > 0)
            Invoke("Respawn", 2f);
        else
            gm.GameOver();
    }

    void Respawn() {
        // Spawn player
        Vector3 newPos = camera.ViewportToWorldPoint(new Vector3(0.2f, 0.5f, 0));
        newPos.z = 0f;
        transform.position = newPos;
        Invoke("EnableCollision", 2f);
    }

    void EnableCollision() {
        // Turn on collision
        collision = true;
        sr.color = Color.white;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;  
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    }

}
