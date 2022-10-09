using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteor : MonoBehaviour
{
    public GameObject meteor;
    public GameObject boss;
    public int xPos;
    public int zPos;
    public int index;
    public float wait;


    public void Start()
    {
       
        {
            StartCoroutine(Spawn());
        }
        
    }
    public void Update()
    {
        


    }

    // Update is called once per frame
    IEnumerator Spawn()
    {
        {
            xPos = Random.Range(10, 110);
            zPos = Random.Range(10, 110);

            Instantiate(meteor, new Vector3(xPos, 50, zPos), Quaternion.identity);
            yield return new WaitForSeconds(wait);
            StartCoroutine(Spawn());

        }
        

    }

    IEnumerator ResetMeteors()
    {
        while (index >= 20)
        {
            index = 0;
            Debug.Log("lL");
            yield return new WaitForSeconds(9);
        }

    }

}
