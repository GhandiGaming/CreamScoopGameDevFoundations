using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorSFXController : MonoBehaviour
{
    /// <summary>
    /// Responsible for controlling the elevator sound effects, 
    /// also used to view the current boolean states of the elevator 
    /// in the inspector.
    /// </summary>

    public AudioSource RightDoorSoundSource;
    public AudioSource LeftDoorSoundSource;
    public AudioSource ElevatorButtonSource;

    public bool DoorsOpen;
    public bool DoorsOpening;
    public bool DoorsClosedStart;
    public bool DoorsClosedExit;
    public bool DoorsClosing;
    public bool PlayDoorsOpeningSound;
    public bool PlayDoorsClosingSound;


    // Start is called before the first frame update
    void Start()
    {
        DoorsClosedStart = true;
        DoorsClosedExit = false;
        DoorsClosing = false;
        DoorsOpen = false;
        DoorsOpening = false;
        PlayDoorsOpeningSound = false;
        PlayDoorsClosingSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        // When the elevator doors should open
        bool OpenTime = Time.time > 5;
        // OpenTime + .01 seconds
        bool OpenTimePlus = Time.time > 5.01f;

        // After a set amount of time, set DoorsOpening to true, then set DoorsOpen to true and play sound effect
        if (OpenTime)
        {
            DoorsOpening = true;
            PlayDoorsOpeningSound = true;
        }

        // stops door opening sound from playing multiple times
        if (OpenTimePlus)
        {
            PlayDoorsOpeningSound = false;
        }

        if (PlayDoorsOpeningSound == true)
        {
            LeftDoorSoundSource.Play();
            RightDoorSoundSource.Play();
        }

        if (DoorsOpening == true)
        {
            DoorsClosedStart = false;
        }



        // If player triggers collider, set DoorsClosing to true, then set DoorsClosed to true 
        if (DoorsClosedExit)
        {

        }

    }
}
