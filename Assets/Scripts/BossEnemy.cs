using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour, IHittable
{
    public NavMeshAgent pathfinder;
    Transform target;
    Transform Qtarget;
    public int damage = 15;
    public float bSpeed = 70f;
    public BossHealthBar healthbar;
    public int maxHealth = 600;
    public int Phase1HP;
    public int Phase2HP;
    public int Phase3HP;
    public int currentHealth;
    bool shooting;
    public float fireRate = 1;
    public float fireCountdown = 0f;
    public Transform barrel;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab1;
    public GameObject meteor;
    Animator animator;

    void Start()
    {
        setColliderState(true);
        setRigidBodyState(true);
        currentHealth = maxHealth;
        pathfinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Qtarget = GameObject.FindGameObjectWithTag("QPlayer").transform;
        animator = GetComponent<Animator>();

        StartCoroutine(UpdatePath());
        EnableShooting();
        healthbar.SetMaxHealth(maxHealth);
        var Colliders = GetComponentsInChildren<Collider>();
        foreach(var collider in Colliders)
        {
            collider.gameObject.AddComponent<Hitbox>();
        }
    }
    

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
    public bool isHealthy 
    {
        get
        {
            return currentHealth >= maxHealth - Phase1HP; 
        }
    }
    public bool isSlightDamaged 
    {
        get
        {
            return (currentHealth > maxHealth - Phase2HP && currentHealth <= maxHealth - Phase1HP);
        }
    }
    public bool isDamaged
    {
        get
        {
            return (currentHealth > maxHealth - Phase3HP && currentHealth <= maxHealth - Phase2HP);
        }
    }

    public bool isVeryDamaged
    {
        get
        {
            return (currentHealth > 0 && currentHealth <= maxHealth - Phase3HP);
        }
    }

    public void TakeDamage(int weaponDamage)
    {
        currentHealth = currentHealth - weaponDamage;
        healthbar.SetCurrentHealth(currentHealth);
    }


    public void Update()
    {
        
            
        if (!isVeryDamaged)
        {
            
            Aim();
            barrel.LookAt(target);
        }
        if (shooting)
        {
            if (fireCountdown <= 0f)
            {
                if (isSlightDamaged || isDamaged)

                {
                    Invoke(nameof(Shoot), 1.8f);
                    fireCountdown = 2.2f / fireRate;
                }
            }
        }
        fireCountdown -= Time.deltaTime;
        if (IsDead)
        {
            meteor.SetActive(false); 
            
            StopCoroutine(UpdatePath());
            Destroy(gameObject, 2.5f);
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Animator>().enabled = false;
            setColliderState(true);
            setRigidBodyState(false);
        }
        if (isHealthy)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Mthrow", false);
            animator.SetBool("Roar", false);
            animator.SetBool("Sthrow", false);
        }
        if(isSlightDamaged)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Mthrow", false);
            animator.SetBool("Roar", false);
            animator.SetBool("Sthrow", true);
        }
        if (isDamaged)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("Mthrow", true);
            animator.SetBool("Roar", false);
            animator.SetBool("Sthrow", false);
            StartCoroutine(UpdatePath());
            return;
        }
        if (isVeryDamaged)
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Mthrow", false);
            animator.SetBool("Roar", true);
            animator.SetBool("Sthrow", false);
            meteor.SetActive(true);
        }
        else
        {
            meteor.SetActive(false);
        }
    }


    IEnumerator UpdatePath()//float delay = 6f)
    {
       // if (delay != 0)
          //  yield return new WaitForSeconds(delay);
        if (!IsDead)
        {
            float refreshRate = 0.15f;

            while (Qtarget != null && isHealthy || isDamaged)
            {
                Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);

                {
                    pathfinder.SetDestination(target.position);
                    yield return new WaitForSeconds(refreshRate);
                }
                if (IsDead)
                {
                    StopCoroutine(UpdatePath());
                }
            }
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
        if (isSlightDamaged || isDamaged)
        { 
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        GameObject bulletGO4 = (GameObject)Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        GameObject bulletGO5 = (GameObject)Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        bulletGO.GetComponent<Rigidbody>().AddForce(barrel.forward * 500);
        bulletGO4.GetComponent<Rigidbody>().AddForce((barrel.right + barrel.forward ) * 500);
        bulletGO5.GetComponent<Rigidbody>().AddForce((-barrel.right + barrel.forward) * 500);

        BossBullet bullet = bulletGO.GetComponent<BossBullet>();
        BossBullet bullet2 = bulletGO4.GetComponent<BossBullet>();
        BossBullet bullet3 = bulletGO5.GetComponent<BossBullet>();

        if (bullet != null)
        {
            bullet.Seek(target);

        }
        if (bullet2 != null)
        {
            bullet2.Seek(target);

        }
        if (bullet3 != null)
        {
            bullet3.Seek(target);

        }

        Destroy(bulletGO, 6f);
        Destroy(bulletGO4, 6f);
        Destroy(bulletGO5, 6f);
        }
        if (isDamaged)
        {
            
            GameObject bulletGO = Instantiate(bulletPrefab1, barrel.position, barrel.rotation);
            GameObject bulletGO2 = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
            GameObject bulletGO3 = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
            bulletGO.GetComponent<Rigidbody>().AddForce(barrel.forward * 2000);
            bulletGO2.GetComponent<Rigidbody>().AddForce(barrel.right * 1000);
            bulletGO3.GetComponent<Rigidbody>().AddForce(-barrel.right* 1000);

            EnemyBullet bullet = bulletGO.GetComponent<EnemyBullet>();
            BossBullet bullet2 = bulletGO2.GetComponent<BossBullet>();
            BossBullet bullet3 = bulletGO3.GetComponent<BossBullet>();
            if (bullet != null)
            {
                bullet.Seek(target);

            }
            if (bullet2 != null)
            {
                bullet2.Seek(target);

            }
            if (bullet3 != null)
            {
                bullet3.Seek(target);

            }
            Destroy(bulletGO, 4f);
            Destroy(bulletGO2, 6f);
            Destroy(bulletGO3, 6f);
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

    }
    public Transform GetTransform()
    {
        return transform;
    }
}
