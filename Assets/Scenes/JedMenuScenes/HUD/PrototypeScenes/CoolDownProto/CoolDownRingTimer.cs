using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CoolDownRingTimer : MonoBehaviour
{
    /// <summary>
    /// Responsible for the speed of the cooldown ring, used to indicate
    /// the time remaining until abilities are available to the player
    /// </summary>

    [Header("Radial Timers")]
    [SerializeField] private float indicatorTimer = 1.0f;
    [SerializeField] private float maxIndicatorTimer = 1.0f;

    [Header("UI Indicator")]
    [SerializeField] private Image radialIndicatorUI = null;

    [Header("Key Codes")]
    [SerializeField] private KeyCode selectKey = KeyCode.Mouse0;

    [Header("Unity Event")]
    [SerializeField] private UnityEvent myEvent = null;

    private bool coolDownActivated = false; 

    private void Update()
    {
        if (Input.GetKeyDown(selectKey))
        {
            coolDownActivated = true;
        }

        if (coolDownActivated == true)
        {
            indicatorTimer -= Time.deltaTime;
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
