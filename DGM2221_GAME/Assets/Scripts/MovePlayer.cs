using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private float moveSpeed;
    public float defaultSpeed = 3f, sprintSpeed = 6f;
    public float jumpForce = 500f;

    private Rigidbody rbody;
   
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    
   
    void FixedUpdate()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(hAxis, 0, vAxis) * moveSpeed * Time.fixedDeltaTime;
        
        rbody.MovePosition(transform.position + movement);

        
        // IF PLAYER IS NOT GROUNDED DO NOT TAKE ANY INPUT, THIS WILL CONTINUE TO LET THE PLAYER TUMBLE IN THE AIR IGNORING THIS LOOKAT FUNCTION.
        
        if (Input.anyKey)
        {
            transform.LookAt(transform.position + new Vector3(movement.x, 0, movement.z));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rbody.AddForce(Vector3.up * jumpForce);
            Debug.Log("Working");
        }
       

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = defaultSpeed;
        }
    }
}
