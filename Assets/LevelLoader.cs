using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Reference to the animator used for the transition between scenes
    public Animator transition;

    // Reference to the time the system waits to load the next scene,
    // allows this wait time ot be changed in the inspector
    public float transitionTime = 1;

    public int playerHealth = 100;

    public bool LoadGameScene;

    void Start()
    {
        LoadGameScene = false;
        PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadGameScene)
        {
            LoadNextLevel();
            PlayerPrefs.SetInt("PlayerCurrentHealth", playerHealth); //On start the player's health is loaded
            PlayerPrefs.SetInt("PlayerMaxHealth", playerHealth);
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play transition animation 
        transition.SetTrigger("Start");

        // Wait a set amount of time before loading the new scene - works best
        // if this time matches the transition animation time
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(levelIndex);
    }
}
