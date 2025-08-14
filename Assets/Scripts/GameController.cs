using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject message, duck, gameOverObject;

    [SerializeField] private GameObject source;
    [SerializeField] private GameObject pipesPrefab;
    [SerializeField] private float interval;

    [SerializeField] private Text scoreText;
    
    // list of all the created pipe instances
    private List<GameObject> _pipes = new List<GameObject>();

    public static GameController instance;
    
    private bool _started;
    private int _score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // no two game controllers
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _score = 0;
        _started = false;
        
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
        scoreText.gameObject.SetActive(true);
        _started = true;
        Destroy(message);
    }

    void OnLost()
    {
        gameOverObject.SetActive(true);
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

    // ReSharper disable once ParameterHidesMember
    public void IncreaseScore(int score)
    {
        this._score += score;
        scoreText.text = this._score.ToString();
        Debug.Log("Score: " + this._score);
    }

    public int GetScore()
    {
        return _score;
    }
}
