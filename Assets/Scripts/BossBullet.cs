using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    
    private int damage = 20;
    public float Speed = 30;

    private Transform target;
    public Rigidbody rb;
    public Vector3 relativeVelocity = new Vector3();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Seek(Transform _target)
    {

        target = _target;

    }
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = Speed * Time.deltaTime;
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        transform.Rotate(0f, 1f, 1f, Space.Self);
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }




    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }

        if (collision.collider.CompareTag("Enemy")) return;

        if (collision.collider.CompareTag("Player"))
        {
            IHittable damageableObject = collision.collider.GetComponent<IHittable>();

            if (damageableObject != null)
            {
                damageableObject.TakeDamage(damage);
                Destroy(gameObject);
            }

        }


    }

}

