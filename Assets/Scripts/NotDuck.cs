using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class NotDuck : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _jumping;
    private bool _firstCollision = true;
    
    private int _score;
    
    public bool lost;
    public UnityEvent onLost;
    
    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationAmount;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    
        //TODO: do better
        _jumping = true; // After the start screen jump
    }

    private void FixedUpdate()
    {
        if (_jumping && !lost) 
        {
            _rb.velocity = Vector2.up * jumpForce;
            _jumping = false;
        } 
        
        // Update notDuck rotation
        _rb.rotation = _rb.velocity.y * rotationAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !lost)
        {
            _jumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // on hitting the ground or the pipes
        if (other.gameObject.name is "Ground" or "PipeTop" or "PipeBottom" && _firstCollision)
        {
            lost = true;
            
            onLost?.Invoke();
            
            // unfreeze duck's x position
            _rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            
            // add random force upwards and to the sides
            _rb.AddForce(new Vector2(
                (float) Random.Range(-200, 200) / 10,
                25),
                ForceMode2D.Impulse);

            // don't do this again
            _firstCollision = false;
            
            // kinda hacky but it works
            GetComponent<Animator>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Score"))
        {
            GameController.instance.IncreaseScore(1);
        }
    }
}
