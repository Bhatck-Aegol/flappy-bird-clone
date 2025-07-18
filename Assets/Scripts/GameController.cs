using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject message;
    
    [SerializeField] private GameObject player;
    
    // Start is called before the first frame update=

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetActive(true);
            Destroy(message);
        }
    }
}
