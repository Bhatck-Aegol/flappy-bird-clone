using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject message, duck;

    [SerializeField] private GameObject source;
    [SerializeField] private GameObject pipesPrefab;
    [SerializeField] private float interval;

    // list of all the created pipe instances
    private List<GameObject> _pipes = new List<GameObject>();
    
    private bool _started = false;

    void Start()
    {
        duck.GetComponent<NotDuck>().onLost.AddListener(OnLost);
        
        InvokeRepeating("SpawnPipes", 0f, interval);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_started)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        duck.SetActive(true);
        _started = true;
        Destroy(message);
    }

    void OnLost()
    {
        StopPipes();
        
        
    }

    void SpawnPipes()
    {
        if (!_started) return;
        GameObject pipesInstance = Instantiate(
            pipesPrefab,
            source.transform.position + Vector3.up * Random.Range(-200, 200) / 100,
            Quaternion.identity);
        
        _pipes.Add(pipesInstance);
    }

    void StopPipes()
    {
        foreach (GameObject pipe in _pipes)
        {
            if (pipe == null) continue; // pipe has been destroyed
            // set all pipe velocities to 0
            pipe.GetComponent<Pipes>().speed = 0;
            
            // stop spawning pipes
            CancelInvoke();
        }
    }
}
