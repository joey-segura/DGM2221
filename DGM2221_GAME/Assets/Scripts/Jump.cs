using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    public Rigidbody rb;

    public GameObject groundCheck;

    public float jumpStrength = 5f;

    public int jumpCount = 0, jumpMax = 2;
    
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck = GameObject.Find("/Player/GroundCheck");
    }


    void Update()
    {
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
