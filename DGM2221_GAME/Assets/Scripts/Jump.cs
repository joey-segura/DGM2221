using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jump : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 100;
    public int currentHealth;
    
    public GameObject player;
    public Vector3 playerSpawn;
    
    public Rigidbody rb;

    public GameObject groundCheck;
    
    public GameObject bullet;
    public Transform bulletPosition;
    public float bulletSpeed = 2000f;

    private float moveSpeed;
    public float defaultSpeed = 3f, sprintSpeed = 6f;
    
    public float gravity;
    public float jumpStrength = 5f;

    public int jumpCount = 0, jumpMax = 2;
    
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = GameObject.Find("/Player/GroundCheck");
        player = GameObject.Find("Player");
        playerSpawn = new Vector3(-1, 1, -6);
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
    }
    
    void Update()
    {
        //DEATH
        
        healthSlider.value = currentHealth;
        if (healthSlider.value <= 1f)
        {
            Respawn();
        }
        
        //WALKING

        rb.AddForce(Vector3.down * gravity);
        
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * moveSpeed * Time.deltaTime;
        
        rb.MovePosition(transform.position + movement);

        // IF PLAYER IS NOT GROUNDED DO NOT TAKE ANY INPUT, THIS WILL CONTINUE TO LET THE PLAYER TUMBLE IN THE AIR IGNORING THIS LOOKAT FUNCTION.
        if (Input.anyKey)
        {
            transform.LookAt(transform.position + new Vector3(hAxis, 0, vAxis));
        }
        
        //SPRINTING
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = defaultSpeed;
        }

        //JUMPING
        
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < jumpMax)
        {
            rb.velocity = (Vector3.up * jumpStrength);
            jumpCount++;
        }
        
        //SHOOTING
        
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        jumpCount = 0;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Plane")
        {
            Respawn();
        }

        if (other.gameObject.name == "Spike")
        {
            Debug.Log("Hit");
            currentHealth -= 10;
        }
    }

    void Respawn()
    {
        currentHealth = maxHealth;
        player.transform.position = playerSpawn;
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletPosition.position, bulletPosition.rotation) as GameObject;
        newBullet.transform.Rotate (90f, 0f, 0f);
        newBullet.AddComponent<Rigidbody>();
        newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
    }
}
