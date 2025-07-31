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
    public bool lost;
    public UnityEvent onLost;
    
    [SerializeField] private float jumpForce;
    [SerializeField] private float rotationAmount;
    
    [SerializeField] private GameObject gameOverObject;
    
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
        if (other.gameObject.name is "Ground" or "PipeTop" or "PipeBottom" && _firstCollision)
        {
            gameOverObject.SetActive(true);
            lost = true;
            
            onLost?.Invoke();
            
            // unfreeze duck's x position and add some force
            _rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            
            _rb.AddForce(new Vector2(
                (float) Random.Range(-200, 200) / 10,
                50),
                ForceMode2D.Impulse);

            _firstCollision = false;
        }
    }
}
