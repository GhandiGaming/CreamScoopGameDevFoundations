using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    int damage = 1;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            IHittable damageableObject = collision.collider.GetComponent<IHittable>();
            Debug.Log("RIP");
            if (damageableObject != null)
            {
                damageableObject.TakeDamage(damage);
                Debug.Log("RIPDebug.Log");
                Destroy(gameObject);
            }

        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        transform.Rotate(180f, 0f, 0f, Space.Self);
    }
}
