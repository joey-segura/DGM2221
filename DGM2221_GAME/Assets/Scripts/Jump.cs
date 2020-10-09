using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    public Rigidbody rb;

    public GameObject groundCheck;
    
    private float moveSpeed;
    public float defaultSpeed = 3f, sprintSpeed = 6f;
    
    public float gravity;
    public float jumpStrength = 5f;

    public int jumpCount = 0, jumpMax = 2;
    
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = GameObject.Find("/Player/GroundCheck");
    }


    void Update()
    {
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
    }

    void OnTriggerEnter()
    {
        jumpCount = 0;
    }
}
