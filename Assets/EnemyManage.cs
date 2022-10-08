using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyManage : MonoBehaviour
{
    public GameObject EnableTrigger;
   // public GameObject EnableText;
    public GameObject DisableCollider;
    public GameObject[] Enemy;
    public Text enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        EnableTrigger.SetActive(false);
       // EnableText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if(GameObject.FindGameObjectsWithTag("Enemy").Length < 1 )
        {
            Debug.Log("GO to elevator");
            EnableTrigger.SetActive(true);
            //EnableText.SetActive(true);
            DisableCollider.SetActive(false);

        }
        
        else
        {
          EnableTrigger.SetActive(false);
          //EnableText.SetActive(false);
        }

        int NumberofEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyCount.text = NumberofEnemies.ToString();
    }
}
