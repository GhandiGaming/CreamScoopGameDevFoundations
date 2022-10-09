using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePU : MonoBehaviour
{
    [SerializeField]
    private float decreaseFRAmount = 0.25f;
    [SerializeField]
    private float powerupDuration = 20;
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
        EnablePowerUp(pGT);
        StartTimer = true;
        yield return new WaitForSeconds(powerupDuration);
        DisablePowerUp(pGT);
        StartTimer = false;
        Destroy(gameObject);
    }
    private void EnablePowerUp(ProjectileGunTutorial pGT)
    {
        pGT.SetFR(decreaseFRAmount);
        StartTimer = true;
    }
    private void DisablePowerUp(ProjectileGunTutorial pGT)
    {
        pGT.SetFR(-decreaseFRAmount);
        StartTimer = true;
    }

}
