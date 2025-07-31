using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pipes : MonoBehaviour
{
    [SerializeField] private float speed;

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