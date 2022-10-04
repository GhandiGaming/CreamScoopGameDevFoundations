using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
	public int Heal = 20; //Int Health = (Amount of Health to heal)
	PlayerStats stats;
	
	void Awake() //On START
	{
		stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>(); //Call PlayerStats C# script from gameobject player
		
	}

	public bool isHealthy //Set Bool Healthy to True //I think its the bools
	{
		get
		{
			return stats.currentHealth == stats.maxHealth; //when(PlayerStats.CurrentHealth is = stats.maxHealth)
		}				
	}

	public bool isSlightDamaged //Set Bool SlightDamaged to true
	{
		get
		{
			return (stats.currentHealth >= stats.maxHealth - Heal); //when(150 >= PlayerStats.CurrentHealth is >= stats.maxHealth-Health to heal)
		}               
	}


	public bool isDamaged  //Set Bool Damaged to true
	{
		get
		{
			return stats.currentHealth <= stats.maxHealth - Heal; //when(PlayerStats.CurrentHealth is <= stats.maxHealth - Health to heal)
		}    
	}

	private void OnTriggerEnter(Collider other) //Always Check If Player collides with Healing Item
	{
		PlayerStats stats = other.GetComponent<PlayerStats>();
		if (stats != null)
		{ 
				if (!isHealthy) //If(Healthy is False) //Item will ignore a full health player 
				{
				
					if (isDamaged)//If(Damaged is True)
					{

						stats.HealDamage(Heal); //HealPlayer

						Destroy(gameObject);//Delete healing item

					} //ENDIF


					if(isSlightDamaged) //If(SlightDamaged is True)
					{   //Item Behaviour
						stats.SlightHealDamage(); //Set Player Health to maxHealth
						Destroy(gameObject); //Delete healing item
					} //ENDIF
				}//ENDIF
		}
	}//ENDIF
	
}
