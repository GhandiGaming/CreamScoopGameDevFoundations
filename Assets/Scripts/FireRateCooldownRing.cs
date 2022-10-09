using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FireRateCooldownRing : MonoBehaviour
{
    /// <summary>
    /// Responsible for the speed of the cooldown ring, used to indicate
    /// the time remaining until abilities are available to the player
    /// </summary>

    [Header("Radial Timers")]
    [SerializeField] private float indicatorTimer = 1f;
    [SerializeField] private float maxIndicatorTimer = 1f;

    [Header("UI Indicator")]
    [SerializeField] private Image radialIndicatorUI = null;



    [Header("Unity Event")]
    [SerializeField] private UnityEvent myEvent = null;

    private bool coolDownActivated = false;
    public GameObject fireRateUP;


    public void Update()
    {
        FireRatePU fireRate = fireRateUP.GetComponent<FireRatePU>();

        if (fireRate.StartTimer)
        {
            coolDownActivated = true;
            Debug.Log("Kill");
        }

        if (coolDownActivated == true)
        {
            indicatorTimer -= Time.deltaTime / 20;
            radialIndicatorUI.enabled = true;
            radialIndicatorUI.fillAmount = indicatorTimer;

            if (indicatorTimer <= 0)
            {
                indicatorTimer = maxIndicatorTimer;
                radialIndicatorUI.fillAmount = maxIndicatorTimer;
                myEvent.Invoke(); //perform the task after the ringloader
                                  //has reset: Replenish ability to dash, grapple etc.
                                  // You can change the event which you wish to invoke in the inspector 
                coolDownActivated = false;
                radialIndicatorUI.enabled = false;
            }
        }
    }
}
