using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Pipes : MonoBehaviour
{
    [SerializeField] public float speed;

    private bool yVariable;

    void Start()
    {
        yVariable = Random.Range(-1, 5) < 0;

        if (yVariable)
        {
            OscillateY();
        }
    }

    private void OscillateY()
    {
        if (GameController.instance.GetScore() < 10)
        {
            return;
            
        }

        float variation = 0.08f;
        
        float newY = transform.position.y + (yVariable? variation : -variation);

        if (newY > 2 || newY < -2)
        {
            yVariable = !yVariable;
            newY = transform.position.y + (yVariable ? variation : -variation);
            
        }
        
        transform.position = new Vector2(transform.position.x, newY);
        
        Invoke("OscillateY", 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(
            transform.position.x - speed * Time.deltaTime,
            transform.position.y
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "DestroyPoint")
        {
            Destroy(gameObject);
        }
    }
}