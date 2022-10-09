using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePU : MonoBehaviour
{
    [SerializeField]
    private int increaseBulletAmount = 1;
    [SerializeField]
    private int increaseAmmoCapacity = 50;
    [SerializeField]
    private float powerupDuration = 30;
    [SerializeField]
    private GameObject artToDisable = null;
    public bool StartTimer = false;
    private Collider collider;
    public AudioSource PU;
    private void Awake()
    {
        collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider Other)
    {
        ProjectileGunTutorial pGT = Other.gameObject.GetComponentInChildren<ProjectileGunTutorial>();
        if (pGT != null)
        {
            StartCoroutine(RunPowerUp(pGT));
        }

    }

    // Update is called once per frame
    public IEnumerator RunPowerUp(ProjectileGunTutorial pGT)
    {
        collider.enabled = false;
        artToDisable.SetActive(false);
        PU.Play();
        StartTimer = true;
        EnablePowerUp(pGT);
        
        yield return new WaitForSeconds(powerupDuration);
        DisablePowerUp(pGT);
        StartTimer = false;
        Destroy(gameObject);
    }
    private void EnablePowerUp(ProjectileGunTutorial pGT)
    {
        pGT.SetBN(increaseBulletAmount);
        pGT.SetAC(pGT.bulletsPerTap);
    }
    private void DisablePowerUp(ProjectileGunTutorial pGT)
    {
        pGT.SetBN(pGT.bulletsPerTap);
        pGT.SetAC(-increaseAmmoCapacity);
    }
}
