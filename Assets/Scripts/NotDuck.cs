using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDuck : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool jumping;
    [SerializeField] float jumpForce;
    [SerializeField] float rotationAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (jumping) 
        {
            rb.velocity = Vector2.up * jumpForce;
            jumping = false;
        } 
        
        // Update notDuck rotation
        rb.rotation = rb.velocity.y * rotationAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumping = true;
        }
    }
}
