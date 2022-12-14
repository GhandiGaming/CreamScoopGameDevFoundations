using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject WeaponHolder;
    public GameObject MainCamera;

    public Canvas PauseMenuCanvas;
    public Canvas AudioMenuCanvas;
    public Canvas HUDCanvas;
    public Canvas ElevatorPromptCanvas;
    public PlayerStats stats;

    void Start()
    {
        HUDCanvas.gameObject.SetActive(true);
        PauseMenuCanvas.gameObject.SetActive(false);
        AudioMenuCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !stats.IsDead)
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
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        PauseMenuCanvas.gameObject.SetActive(false);
        AudioMenuCanvas.gameObject.SetActive(false);
        HUDCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FreezeCameraOff();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FreezeCamera()
    {
        MainCamera.GetComponent<PlayerSee>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        WeaponHolder.GetComponent<WeaponSway>().enabled = false;
    }

    void FreezeCameraOff()
    {
        MainCamera.GetComponent<PlayerSee>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        WeaponHolder.GetComponent<WeaponSway>().enabled = true;
    }

    public void LoadAudioMenu()
    {
        if (GameIsPaused)
        {
            Debug.Log("Loading Audio Menu");
            AudioMenuCanvas.gameObject.SetActive(true);
            PauseMenuCanvas.gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void QuitToTitle()
    {
        Debug.Log("Quitting to Title");
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlainsMenuScene");
    }

    public void RestartRound()
    {
        Debug.Log("Restarting Round");

        // Write code to reset to current round
        SceneManager.LoadScene("WaveOne");
        Time.timeScale = 1f;
    }

    public void ReturnToPauseMenu()
    {
        Debug.Log("BackButtonPressed");
        AudioMenuCanvas.gameObject.SetActive(false);
        PauseMenuCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
