using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePU : MonoBehaviour
{
    [SerializeField]
    private float decreaseFRAmount = 0.25f;
    [SerializeField]
    private float powerupDuration = 30;
    [SerializeField]
    private GameObject artToDisable = null;

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
        yield return new WaitForSeconds(powerupDuration);
        DisablePowerUp(pGT);
        Destroy(gameObject);
    }
    private void EnablePowerUp(ProjectileGunTutorial pGT)
    {
        pGT.SetFR(decreaseFRAmount);
    }
    private void DisablePowerUp(ProjectileGunTutorial pGT)
    {
        pGT.SetFR(-decreaseFRAmount);
    }

}
