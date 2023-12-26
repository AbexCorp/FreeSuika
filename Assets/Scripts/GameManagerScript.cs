using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public AudioSource FruitPop;
    public AudioSource FailSound;
    public AudioSource GameMusic;
    public AudioSource WinSound;

    public bool Lost = false;
    public bool Won = false;
    public int HighScore;
    
    private GameObject uiController;


    // Start is called before the first frame update
    void Start()
    {
        uiController = GameObject.FindGameObjectWithTag("UIController");
        PrepareHighScore();

        GameMusic.loop = true;
        GameMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayFruitPop()
    {
        FruitPop.PlayOneShot(FruitPop.clip);
    }
    public void GameOver()
    {
        Lost = true;
        GameMusic.Stop();
        FailSound.Play();
        var fruits = FindObjectsOfType<GameObject>();
        foreach(var fruit in fruits)
        {
            if (fruit.activeInHierarchy)
            {
                if( fruit.CompareTag("Cherry") ||
                    fruit.CompareTag("Strawberry") ||
                    fruit.CompareTag("Grape") ||
                    fruit.CompareTag("Tangerine") ||
                    fruit.CompareTag("Orange") ||
                    fruit.CompareTag("Apple") ||
                    fruit.CompareTag("Lemon") ||
                    fruit.CompareTag("Peach") ||
                    fruit.CompareTag("Pineapple") ||
                    fruit.CompareTag("Melon") ||
                    fruit.CompareTag("Watermelon"))
                        fruit.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        UpdateHighScore(uiController.GetComponent<UiControllerScript>().GetScore());
        //Coroutine here and lose pop up
        Debug.Log("Lose popup here");
    }

    private void PrepareHighScore()
    {
        if (System.IO.File.Exists("score.txt"))
            HighScore = int.Parse(System.IO.File.ReadAllText("score.txt"));
        else
        {
            System.IO.File.WriteAllText("score.txt", "0");
            HighScore = 0;
        }
    }
    public void UpdateHighScore(int score)
    {
        if (score < HighScore)
            return;

        HighScore = score;
        System.IO.File.WriteAllText("score.txt", HighScore.ToString());
    }

    public void PlayWinSound()
    {
        WinSound.Play();
    }
    public void WinGame()
    {
        PlayWinSound();
        Won = true;
        //Display victory pop up here
        Debug.Log("Win popup here");
    }
}
