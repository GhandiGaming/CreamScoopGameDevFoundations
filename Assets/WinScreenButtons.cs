using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinScreenButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AudioHUD;
    public void LoadMenu()
    {
        SceneManager.LoadScene("PlainsMenuScene");
    }

    
}
