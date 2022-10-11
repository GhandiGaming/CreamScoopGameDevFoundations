using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TImer : MonoBehaviour
{
    public Text timertext;
    
    public float timeStart;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        timertext.text = timeStart.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        timeStart += Time.deltaTime;
        timertext.text = timeStart.ToString("F2");
    }


}