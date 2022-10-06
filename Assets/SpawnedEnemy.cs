using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SpawnedEnemy : MonoBehaviour, IHittable
{
    public NavMeshAgent pathfinder;
    public LootSpawn loot;
    Transform target;
    public int damage = 15;

    public int maxHealth = 20;
    public int currentHealth;
    public AudioSource takeDamage;
    void Start()
    {
        setColliderState(false);
        setRigidBodyState(true);
        currentHealth = maxHealth;
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(UpdatePath(6f));
        loot = GetComponentInChildren<LootSpawn>();

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
        takeDamage.Play();
    }


    public void Update()
    {
        if (IsDead)
        {
            StopCoroutine(UpdatePath());
            Destroy(gameObject, 2.5f);
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Animator>().enabled = false;
            setColliderState(true);
            setRigidBodyState(false);

        }
    }

    public IEnumerator UpdatePath(float delay = 6f)
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (!IsDead)
        {
            float refreshRate = 0.25f;

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
    private void OnCollisionEnter(Collision collision)
    {

        if (!IsDead)
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
    public Transform GetTransform()
    {
        return transform;
    }
}
