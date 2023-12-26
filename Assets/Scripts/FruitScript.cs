using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    public int ScoreForCombine;
    public bool WillGrow = false;
    public bool wasDropped = false;

    private GameObject spawner;
    private GameObject uiController;
    private GameObject gameManager;

    private float maxSize;
    private string tagOfThis;
    private float scale = 1;
    private bool willCombine;
    private bool isInTheGame;
    private bool prepareToDie = false;

    // Start is called before the first frame update
    void Start()
    {
        willCombine = false;
        isInTheGame = false;
        tagOfThis = this.gameObject.tag;
        spawner = GameObject.FindGameObjectWithTag("Spawn");
        uiController = GameObject.FindGameObjectWithTag("UIController");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        maxSize = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(WillGrow)
            Grow();
        if(IsOutsideOfTheGame() && isInTheGame && !prepareToDie) 
        {
            prepareToDie = true;
            StartCoroutine(CheckIfGameOver());
        }
        if(!isInTheGame && wasDropped)
        {
            prepareToDie = true;
            StartCoroutine(CheckIfGameOver());
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(tagOfThis == other.gameObject.tag)
        {
            FruitScript fruit = other.gameObject.GetComponent<FruitScript>();
            if(willCombine == false && fruit.willCombine == false)
            {
                willCombine = true;
                fruit.willCombine = true;
                Combine(fruit);
            }
        }
        if(other.gameObject.tag == "GameZone")
            isInTheGame = true;
    }
    private bool IsOutsideOfTheGame()
    {
        if(gameObject.GetComponent<Rigidbody2D>().position.y >= 7.75f)
            return true;
        return false;
    }
    private IEnumerator CheckIfGameOver()
    {
        yield return new WaitForSeconds(1.5f);
        if (IsOutsideOfTheGame() && !gameManager.GetComponent<GameManagerScript>().Lost)
            gameManager.GetComponent<GameManagerScript>().GameOver();
        prepareToDie = false;
    }



    private void Combine(FruitScript fruit)
    {
        if(tagOfThis == "Watermelon")
            gameManager.GetComponent<GameManagerScript>().PlayWinSound();
        if (tagOfThis == "Melon" && !(gameManager.GetComponent<GameManagerScript>().Won))
            gameManager.GetComponent<GameManagerScript>().WinGame();
        Vector2 thisFruit = transform.position;
        Vector2 thatFruit = fruit.transform.position;
        Destroy(fruit.gameObject);
        Vector3 position = (thisFruit + thatFruit) / 2;
        gameManager.GetComponent<GameManagerScript>().PlayFruitPop();
        spawner.GetComponent<SpawnerScript>().SpawnBiggerFruit(tagOfThis, position);
        uiController.GetComponent<UiControllerScript>().UpdateScore(ScoreForCombine);
        Destroy(this.gameObject);
    }
    public void GetCombined()
    {     
        wasDropped = true;
        WillGrow = true;
        scale = 0.1f;
        gameObject.transform.localScale.Set(scale * maxSize, scale * maxSize, scale);
    }
    private void Grow()
    {
        scale += 0.05f;
        scale = System.MathF.Round(scale,2);
        gameObject.transform.localScale = new Vector3(scale * maxSize, scale * maxSize, scale);
        if(scale >= 1)
        {
            gameObject.transform.localScale.Set(maxSize, maxSize, 1);
            WillGrow = false;
        }
    }
}
