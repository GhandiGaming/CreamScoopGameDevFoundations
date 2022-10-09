using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingRandomSpawner : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    public Transform player;


    // Start is called before the first frame update
    public void Awake()
    {
        InvokeRepeating("SpawnNow", 7f, 6f);
        
        
        
        
    }

    Vector3 getRandomPose()
    {
        float _x = Random.Range(10, 87);
        float _y = Random.Range(10, 17);
        float _z = Random.Range(14, 87);

        Vector3 newPos = new Vector3(_x, _y, _z);
        return newPos;
    }
    private void Update()
    {
        Invoke(nameof(StopSpawn), 90f);
    }
    void SpawnNow()
    {
        var go = Instantiate(enemiesToSpawn[Random.Range(0, 5)], getRandomPose(), Quaternion.identity);
        var component = go.GetComponent<FlyingEnemy>();
        component.target = player;
        Debug.Log("Enemy created");
    }

    void StopSpawn()
    {
        CancelInvoke();
    }
}
