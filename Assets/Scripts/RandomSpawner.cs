using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnNow", 6f, 1.5f);
    }

    Vector3 getRandomPose()
    {
        float _x = Random.Range(30, 67);
        float _y = 1f;
        float _z = Random.Range(34, 67);

        Vector3 newPos = new Vector3(_x, _y, _z);
        return newPos;
    }
    private void Update()
    {
        Invoke(nameof(StopSpawn), 45f);
    }
    void SpawnNow()
    {
        Instantiate(enemiesToSpawn[Random.Range(0, 2)], getRandomPose(), Quaternion.identity);
        Debug.Log("Enemy created");
    }

    void StopSpawn()
    {
        CancelInvoke();
    }
}
