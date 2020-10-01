using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
   [SerializeField] private float knockbackStrength = 50f;


   private void OnCollisionEnter(Collision collision)
   {
      Rigidbody rb = collision.collider.GetComponent<Rigidbody>();

      if (rb != null)
      {
         Vector3 collisionDirection = collision.transform.position - transform.position;
         
         rb.AddForce(collisionDirection.normalized * knockbackStrength, ForceMode.Impulse);
      }
   }
}
