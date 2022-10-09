using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour, IHittable
{
    public Transform target;
    
    public int currentHealth;
    public int maxHealth = 30;
    private Rigidbody rb;
    public int damage = 20;
    public float rotationalDamp = 0.5f;
    public float movementSpeed = 10f;
    public float rayCastOffset = 2.5f;
    public float detectionDistance = 20f;
    public bool IsDead
    {
        get
        {
            return currentHealth <= 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        setColliderState(false);
        setRigidBodyState(true);

    }

    // Update is called once per frame
    void Update()
    {
        Pathfinding();
        Turn();
        Move();
        if (IsDead)
        {
            Destroy(gameObject, 2.5f);
            GetComponent<Animator>().enabled = false;
            setColliderState(true);
            setRigidBodyState(false);
        }
    }
   void Turn()
    {
        transform.LookAt(target);
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    } 

    void Move()
    {
        if(!IsDead)
        { Vector3 dir = target.position - transform.position;
            float distanceThisFrame = movementSpeed * Time.deltaTime;
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }
    public void TakeDamage(int weaponDamage)
    {
        currentHealth = currentHealth - weaponDamage;
    }

    void Pathfinding()
    {
        if (!IsDead)
        {
            RaycastHit hit;
            Vector3 raycastOffset = Vector3.zero;

            Vector3 left = transform.position - transform.right * rayCastOffset;
            Vector3 right = transform.position + transform.right * rayCastOffset;
            Vector3 up = transform.position + transform.up * rayCastOffset;
            Vector3 down = transform.position - transform.up * rayCastOffset;

            Debug.DrawRay(left, transform.forward * detectionDistance, Color.cyan);
            Debug.DrawRay(right, transform.forward * detectionDistance, Color.cyan);
            Debug.DrawRay(up, transform.forward * detectionDistance, Color.cyan);
            Debug.DrawRay(down, transform.forward * detectionDistance, Color.cyan);

            if (Physics.Raycast(left, transform.forward, out hit, detectionDistance))
                raycastOffset += Vector3.right;
            else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance))
                raycastOffset -= Vector3.right;


            if (Physics.Raycast(down, transform.forward, out hit, detectionDistance))
                raycastOffset += Vector3.up;
            else if (Physics.Raycast(up, transform.forward, out hit, detectionDistance))
                raycastOffset -= Vector3.up;

            if (raycastOffset != Vector3.zero)
                transform.Rotate(raycastOffset * 5f * Time.deltaTime);
            else
                Turn();
        }
    }  
    public Transform GetTransform()
    {
        return transform;
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if(!IsDead)
        { 
        if (other.gameObject.CompareTag("Player"))
        {
            IHittable damageableObject = other.gameObject.GetComponent<IHittable>();
            Debug.Log("RIP");
            if (damageableObject != null)
            {
                damageableObject.TakeDamage(damage);
                Debug.Log("Ouch");
              
            }

        }
        }

    }
    void setRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
        GetComponent<Rigidbody>().isKinematic = !state;
    }
    void setColliderState(bool state)
    {
        Collider[] Col = GetComponentsInChildren<Collider>();

        foreach (Collider Cl in Col)
        {
            Cl.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }
}
