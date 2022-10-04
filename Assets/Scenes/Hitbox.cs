using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    
    public void Start()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        BossEnemy enemy = GetComponentInParent<BossEnemy>();
        int dmg = enemy.damage;

        if (collision.collider.CompareTag("Player"))
        {
            IHittable damageableObject = collision.collider.GetComponent<IHittable>();
            Debug.Log("RIP");
            if (damageableObject != null)
            {
                damageableObject.TakeDamage(dmg);
                Debug.Log("Yeouchy");
            }

        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
