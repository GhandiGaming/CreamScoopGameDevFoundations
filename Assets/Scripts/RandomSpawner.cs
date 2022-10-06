using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnNow", 6f, 2.5f);
    }

    Vector3 getRandomPose()
    {
        float _x = Random.Range(40, 57);
        float _y = 1f;
        float _z = Random.Range(44, 57);

        Vector3 newPos = new Vector3(_x, _y, _z);
        return newPos;
    }
    private void Update()
    {
        Invoke(nameof(StopSpawn), 50f);
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
