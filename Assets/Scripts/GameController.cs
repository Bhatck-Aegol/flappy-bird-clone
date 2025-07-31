using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject message, duck;

    [SerializeField] private GameObject source, pipes;
    [SerializeField] private float interval;

    private bool _started, _first = true;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            duck.SetActive(true);
            _started = true;
            Destroy(message);
        }
        

        if (_started && _first)
        {
            InvokeRepeating("SpawnPipes", 0f, interval);
            _first = false;
        }
    }

    void SpawnPipes()
    {
        Instantiate(
            pipes,
            source.transform.position + Vector3.up * Random.Range(-200, 200) / 100,
            Quaternion.identity);
    }
}
