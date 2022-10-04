using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPU : MonoBehaviour
{
    [SerializeField]
    private float increaseSpeedAmount = 10;
    [SerializeField]
    private float powerupDuration = 30;
    [SerializeField]
    private GameObject artToDisable = null;
    public AudioSource PU;
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider Other)
    {
        PlayerMovement pM = Other.gameObject.GetComponent<PlayerMovement>();
        if (pM != null)
        {
            StartCoroutine(RunPowerUp(pM));
        }

    }

    // Update is called once per frame
    public IEnumerator RunPowerUp(PlayerMovement pM)
    {
        collider.enabled = false;
        artToDisable.SetActive(false);
        PU.Play();
        EnablePowerUp(pM);
        yield return new WaitForSeconds(powerupDuration);
        DisablePowerUp(pM);
        Destroy(gameObject);
    }
    private void EnablePowerUp(PlayerMovement pM)
    {
        pM.SetSpeed(increaseSpeedAmount);
    }
    private void DisablePowerUp(PlayerMovement pM)
    {
        pM.SetSpeed(-increaseSpeedAmount);
    }
}
