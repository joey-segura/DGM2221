using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private float moveSpeed;
    private float defaultSpeed = 5f, sprintSpeed = 10f;

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

        if (Input.anyKey)
        {
            transform.LookAt(transform.position + new Vector3(movement.x, 0, movement.z));
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
