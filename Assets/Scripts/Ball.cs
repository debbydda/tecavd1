using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    public float backspin = .02f;  // Taxa de rotação da BB. Ajuste conforme necessário.

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 1.2f);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Debug.DrawRay(transform.position, rb.velocity.normalized, Color.green, 100, false);
        
        //Debug.Log(rb.velocity.magnitude);
        Vector3 magnusDirection = Vector3.Cross(rb.velocity, transform.right).normalized;
        
        Vector3 magnusForce = Mathf.Sqrt(rb.velocity.magnitude) * magnusDirection * backspin * Time.fixedDeltaTime;
        
        Debug.DrawRay(transform.position, magnusForce * 1000, Color.red, Mathf.Infinity);
        rb.AddForce(magnusForce);
    }
}