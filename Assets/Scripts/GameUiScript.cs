using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUiScript : MonoBehaviour
{
    public GameObject MenuButton;
    public GameObject PopUp;
    public AudioSource ButtonPop;

    public bool PlayerIsInUi { get { return inUi; } }
    private bool inUi = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MenuOpened()
    {
        MenuButton.GetComponent<GameUiScript>().ButtonPop.Play();
        MenuButton.GetComponent<GameUiScript>().inUi = true;
        MenuButton.GetComponent<GameUiScript>().PopUp.SetActive(true);
    }
    public void MenuClosed()
    {
        MenuButton.GetComponent<GameUiScript>().ButtonPop.Play();
        MenuButton.GetComponent<GameUiScript>().inUi = false;
        MenuButton.GetComponent<GameUiScript>().PopUp.SetActive(false);
    }
    public void ReturnToMenu()
    {
        MenuButton.GetComponent<GameUiScript>().ButtonPop.Play();
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        Application.Quit();
    }
}
