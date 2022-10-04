using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawn : MonoBehaviour
{
    [System.Serializable]
    // Start is called before the first frame update
    public class DropCurrency
    {
        public string name;
        public GameObject item;
        public int dropRarity;

    }

    public List<DropCurrency> LootTable = new List<DropCurrency>();
    public int dropChance;
    // Update is called once per frame
    public void calculateLoot()
    {
        int calc_dropChance = Random.Range(0, 101);

        if (calc_dropChance > dropChance)
        {
            Debug.Log("L bozo");
        }
        if (calc_dropChance <=  dropChance)
        {
            int itemWeight = 0;

            for (int i = 0; i < LootTable.Count; i++)
            {
                itemWeight += LootTable[i].dropRarity;

            }
            Debug.Log("itemWeight =" + itemWeight);

            int randomValue = Random.Range(0, itemWeight);

            for (int j = 0; j < LootTable.Count; j++)
            {
                if (randomValue <= LootTable [j].dropRarity)
                {
                    Instantiate(LootTable[j].item, transform.position, Quaternion.identity);
                    return;
                }
                randomValue -= LootTable[j].dropRarity;
                Debug.Log("randomValue decreased =" + randomValue);
            }


        }

    }
}
