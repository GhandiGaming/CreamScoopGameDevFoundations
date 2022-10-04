using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMusicController : MonoBehaviour
{
    /// <summary>
    /// Responsible for controlling elevator music
    /// </summary>

    public AudioSource ElevatorMusicSource;
    
    public bool DoorsClosedExit;

    // Start is called before the first frame update
    void Start()
    {
        DoorsClosedExit = false;
        ElevatorMusicSource.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorsClosedExit)
        {
            ElevatorMusicSource.gameObject.SetActive(false);
        }
    }            
}
