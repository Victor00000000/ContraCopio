using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameMaster gm;
    public SpriteRenderer sr;
    Camera camera;
    Vector2 inputVector;
    public float speed = 5;
    public float jumpingSpeed = 5;
    Rigidbody2D rb2d;
    Animator animator;
    public bool jumping = false;
    bool collision = true;
    float fallingTimer;
    public Transform groundCheck;
    public Transform grroundCheck2;
    public LayerMask maski;
    bool isGrounded;
    Color spawnColor;

    bool right;
    bool left;
    bool up;
    bool down;

    // Fireing
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public GameObject bulletParent;
    public float gunCooldown = 1f;
    float gunCooldownTimer;

    // Start is called before the first frame update
    void Start()
    {  
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        camera = Camera.main;
        bulletParent = new GameObject("Bullets");

        spawnColor = Color.white;
        spawnColor.a = 0.3f;
    }

    private void FixedUpdate()
    {
        jumping = !Physics2D.OverlapArea(groundCheck.position, grroundCheck2.position, maski);
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
        right = Input.GetKey(KeyCode.D);
        left = Input.GetKey(KeyCode.A);
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);

        inputVector = new Vector2(0, 0);

        animator.SetBool("Move", false);

        if (!jumping)
        {
            if (right)
            {
                //Debug.Log("Right");
                inputVector.x = 1;
                transform.localScale = new Vector3(1, 1, 1);
                animator.SetBool("Move", true);

            }
            else if (left)
            {
                inputVector.x = -1;
                transform.localScale = new Vector3(-1, 1, 1);
                animator.SetBool("Move", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Y) && jumping == false)
        {
            
            if (right)
                rb2d.velocity = new Vector2(jumpingSpeed, jumpingSpeed * 2).normalized * jumpingSpeed;
            else if (left)
                rb2d.velocity = new Vector2(-jumpingSpeed, jumpingSpeed * 2).normalized * jumpingSpeed;
            else 
                rb2d.velocity = new Vector2(0, jumpingSpeed);

            //animator.SetBool("Jumping", true);
            jumping = true;
        }
        

        if (down)
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
        
        gunCooldownTimer = gunCooldown;
        GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity, bulletParent.transform);
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
            //jumping = false;
            //animator.SetBool("Jumping", false);
        } else if (other.collider.CompareTag("Death") ||
                  other.collider.CompareTag("Enemy")) {
            if (collision)
                Die();
        }
    }

    void Die() {
        Debug.Log("die");
        Vector2 newPos = new Vector2(-50000, 0);
        transform.position = newPos;
        collision = false;
        if (sr != null)
            sr.color = spawnColor;
        gm.UpdateLives(-1);
        Debug.Log("die");
        if (gm.lives > 0) {
            Invoke("Respawn", 2f);
            Debug.Log("over");
        } else {
            gm.GameOver();
            Debug.Log("down");
        }
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
        Gizmos.DrawLine(groundCheck.position, grroundCheck2.position);
    }

}
