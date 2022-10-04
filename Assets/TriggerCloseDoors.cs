using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCloseDoors : MonoBehaviour
{
    public Collider ExitTriggerCollider;
    public GameObject PreventEntryCollider;

    // Start is called before the first frame update
    void Start()
    {
        PreventEntryCollider.SetActive(false);
    }

    public void OnTriggerEntry(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PreventEntryCollider.SetActive(true);
            Debug.Log("triggered");
        }
    }
}
