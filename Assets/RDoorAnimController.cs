using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RDoorAnimController : MonoBehaviour
{
    public Animator RDoorAnimator;

    public bool DoorsOpen;
    public bool DoorsOpening;
    public bool DoorsClosed;
    public bool DoorsClosedExit;
    public bool DoorsClosing;

    // Start is called before the first frame update
    void Start()
    {
        RDoorAnimator = gameObject.GetComponent<Animator>();
       
        DoorsClosed = true;
        DoorsClosedExit = false;
        DoorsClosing = false;
        DoorsOpen = false;
        DoorsOpening = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool OpenTime = Time.time > 5;
        bool OpenTimePlus = Time.time > 5.01f;

        if (OpenTime)
        {
            DoorsOpening = true;
        }

        if (DoorsOpening)
        {
            RDoorAnimator.SetBool("DoorsOpening", true);
        }
    }
}
