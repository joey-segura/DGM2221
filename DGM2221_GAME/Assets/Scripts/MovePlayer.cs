using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    private float moveSpeed;
    public float defaultSpeed = 3f, sprintSpeed = 6f;
    public float jumpForce = 600f;
    public float jumpCooldown = 0.8f, timeSinceJump;
    public float gravity;
    public float increasedGravity = 50f, decreasedGravity = 30f;

    private Rigidbody rbody;

    [SerializeField] private bool IsGrounded;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Movement();
        CheckGround();
    }


    //MOVEMENT---------------------------------------------------------------------------------------------------------------------

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == true)
        {
            rbody.AddForce(Vector3.up * jumpForce);
            timeSinceJump = 0;
        }

        timeSinceJump += Time.deltaTime;
       
        
        if (IsGrounded == true)
        {
            gravity = 0;
        }
        else
        {
            if (rbody.velocity.y > 0)
            {
                gravity = decreasedGravity;
            }
            else if (rbody.velocity.y < 0)
            {
                gravity = increasedGravity;
            }
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            StartSprint();
        }
        else
        {
            EndSprint();
        }
        

        //CROUCH-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        void StartCrouch()
        {

        }

        void EndCrouch()
        {

        }

        //SPRINT-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        void StartSprint()
        {
            moveSpeed = sprintSpeed;
        }

        void EndSprint()
        {
            moveSpeed = defaultSpeed;
        }
        
        
        
        //MOVEMENT------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        rbody.AddForce(Vector3.down * gravity);
        
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * moveSpeed * Time.deltaTime;
        
        rbody.MovePosition(transform.position + movement);

        // IF PLAYER IS NOT GROUNDED DO NOT TAKE ANY INPUT, THIS WILL CONTINUE TO LET THE PLAYER TUMBLE IN THE AIR IGNORING THIS LOOKAT FUNCTION.
        if (Input.anyKey)
        {
            transform.LookAt(transform.position + new Vector3(hAxis, 0, vAxis));
        }
    }
    
    void CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position,this.transform.up *-1, out hit, 1.1f))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }
}