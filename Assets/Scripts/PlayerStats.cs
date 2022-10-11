using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IHittable
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
    public AudioSource GameOver;
    public HealthBar healthbar;
    public GameObject DeathHUD;
    public GameObject WeaponHolder;
    public GameObject MainCamera;

    void Start()
      {
        DeathHUD.SetActive(false);
        maxHealth = SetMaxHealthFromLevel();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    private int SetMaxHealthFromLevel()
    {
        maxHealth = healthLevel * 10; //configure health levels to give different max healths for different playable characters
        return maxHealth;
    }

    public bool IsDead
    {
      get
         {
            return currentHealth <= 0;
         }
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthbar.SetCurrentHealth(currentHealth);


        if (IsDead)
        {
            GameOver.Play();
            Debug.Log("RIP");
            DeathHUD.SetActive(true);
            Time.timeScale = 0f;
            MainCamera.GetComponent<PlayerSee>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            WeaponHolder.GetComponent<WeaponSway>().enabled = false;
        }


    }
    public void HealDamage(int Heal)
    {
        currentHealth = currentHealth + Heal;
        healthbar.SetCurrentHealth(currentHealth);

    }
    public void SlightHealDamage()
    {
        currentHealth = maxHealth;
        healthbar.SetCurrentHealth(currentHealth);

    }

    public Transform GetTransform()
    {
        return transform;
    }

}
