using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSceneChange : MonoBehaviour
{
    public GameObject elevatorTrigger;
    void Start()
    {
        elevatorTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 1)
        {
            elevatorTrigger.SetActive(true);
            Debug.Log("Level clear");
        }
        else
        { 
        elevatorTrigger.SetActive(false);
        }
    }
}
