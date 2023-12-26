using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    public AudioSource MenuMusic;
    public AudioSource ButtonPop;

    // Start is called before the first frame update
    void Start()
    {
        MenuMusic.loop = true;
        MenuMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ButtonPlay()
    {
        ButtonPop.Play();
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    public void ButtonQuit()
    {
        ButtonPop.Play();
        Application.Quit();
    }
}
