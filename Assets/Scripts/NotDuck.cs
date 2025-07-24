using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class NotDuck : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool jumping;
    public bool lost;
    
    [SerializeField] float jumpForce;
    [SerializeField] float rotationAmount;
    
    [SerializeField] GameObject gameOverObject;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    
        //TODO: do better
        jumping = true; // After the start screen jump
    }

    private void FixedUpdate()
    {
        if (jumping && !lost) 
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
        if (Input.GetKeyDown(KeyCode.Space) && !lost)
        {
            jumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name is "Ground" or "PipeTop" or "PipeBottom")
        {
            gameOverObject.SetActive(true);
            lost = true;
        }
    }
}
