using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;


    public GameObject WeaponHolder;
    public GameObject MainCamera;

    public Canvas PauseMenuCanvas;
    public Canvas HUDCanvas;

    void Start()
    {
        HUDCanvas.gameObject.SetActive(true);
        PauseMenuCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    
    void Pause()
    {
        PauseMenuCanvas.gameObject.SetActive(true);
        HUDCanvas.gameObject.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FreezeCamera();
    }

    public void Resume()
    {
        PauseMenuCanvas.gameObject.SetActive(false);
        HUDCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FreezeCameraOff();
    }

    void FreezeCamera()
    {
        MainCamera.GetComponent<PlayerSee>().enabled = false;
        WeaponHolder.GetComponent<WeaponSway>().enabled = false;
    }

    void FreezeCameraOff()
    {
        MainCamera.GetComponent<PlayerSee>().enabled = true;
        WeaponHolder.GetComponent<WeaponSway>().enabled = true;
    }

    public void LoadAudioMenu()
    {
        Debug.Log("Loading Audio Menu");
    }

    public void QuitToTitle()
    {
        Debug.Log("Quitting to Title");
    }

    public void RestartRound()
    {
        Debug.Log("Restarting Round");
    }


}
