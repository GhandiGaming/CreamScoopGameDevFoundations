using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private int damage = 20;
    //public float Speed = 1;

    private Transform target;
    public Rigidbody rb;
    public Vector3 relativeVelocity = new Vector3();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.Rotate(90f, 0f, 0f, Space.Self);
    }
    public void Seek(Transform _target)
    {

        target = _target;

    }
    private void Update()
    {
        // Vector3 dir = target.position;
        //float distanceThisFrame = Speed * Time.deltaTime;
        // transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }




    }

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
       
        if (collision.collider.CompareTag("Bullet")) return;
        if (collision.collider.CompareTag("Enemy")) return;

    }
}
