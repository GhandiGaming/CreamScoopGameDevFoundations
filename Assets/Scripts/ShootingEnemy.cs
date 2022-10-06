using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : MonoBehaviour, IHittable
{
    public NavMeshAgent pathfinder;
    Transform target;
    Transform Qtarget;
    public LootSpawn loot;
    public int damage = 15;
    public float bSpeed = 70f;

    public int maxHealth = 20;
    public int currentHealth;
    bool shooting;
    void Start()
    {
        currentHealth = maxHealth;
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Qtarget = GameObject.FindGameObjectWithTag("QPlayer").transform;
        StartCoroutine(UpdatePath());
        loot = GetComponentInChildren<LootSpawn>();
        setColliderState(false);
        setRigidBodyState(true);

    }
    public float fireRate = 1;
    public float fireCountdown = 0f;
    public Transform barrel;
    public GameObject bulletPrefab;

    private void Aim()
    {
        // TURN
        float targetPlaneAngle = vector3AngleOnPlane(target.position, transform.position, -transform.up, transform.forward);
        Vector3 newRotation = new Vector3(0, targetPlaneAngle, 0);
        barrel.transform.Rotate(newRotation, Space.Self);
            
        // UP/DOWN
        float upAngle = Vector3.Angle(target.position, barrel.transform.up);
        Vector3 upRotation = new Vector3(-upAngle + 90, 0, 0);
        barrel.transform.Rotate(upRotation, Space.Self);
    }

    float vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toZeroAngle)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toZeroAngle, planeNormal);

        return projectedVectorAngle;
    }

    public bool IsDead
    {
        get
        {
            return currentHealth <= 0;
        }
    }

    public void TakeDamage(int weaponDamage)
    {
        currentHealth = currentHealth - weaponDamage;
    }


    public void Update()
    {
        Aim();
        barrel.LookAt(target);
        if (shooting)
        {
            if (fireCountdown <= 0f)
            {
                Invoke(nameof(Shoot),1f);
                fireCountdown = 1f / fireRate;
            }
        }
        fireCountdown -= Time.deltaTime;
        if (IsDead)
        {
           
            StopCoroutine(UpdatePath());
            Destroy(gameObject, 2.5f);
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Animator>().enabled = false;
            setColliderState(true);
            setRigidBodyState(false);
            Invoke(nameof(loot.calculateLoot), 2.5f);
            Component.Destroy(loot);
        }
    }

    void setRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

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
    IEnumerator UpdatePath()
    {
        if (!IsDead)
        {
            float refreshRate = 0.15f;

            while (target != null)
            {
                Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);


                pathfinder.SetDestination(target.position);
                yield return new WaitForSeconds(refreshRate);

                if (IsDead)
                {
                    StopCoroutine(UpdatePath());
                }
            }
        }

    }
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("QPlayer"))
        {
            EnableShooting();
        }
        else
        {
            DisableShooting();
        }
    }
    void EnableShooting()
    {
        shooting = true;
    }

    void DisableShooting()
    {
        shooting = false;
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
            }

        }
        
        
    }

    void Shoot()
    {
        if (!IsDead)
        {
            GameObject bulletGO = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
            bulletGO.GetComponent<Rigidbody>().AddForce(barrel.forward * 2000);

            EnemyBullet bullet = bulletGO.GetComponent<EnemyBullet>();

            if (bullet != null)
            {
                bullet.Seek(target);

            }

            Destroy(bulletGO, 1f);
        }
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
