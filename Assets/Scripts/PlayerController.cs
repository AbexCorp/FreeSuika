using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour, InputControlls.IPlayerActions
{
    public GameObject spawner;
    private GameObject uiController;
    private GameObject gameManager;
    InputControlls controlls;

    private float mousePosition;
    private float dropPosition;
    private bool fruitReady;
    private bool fruitHadTime = true;
    private bool gameIsInFocus = true;

    private string nowFruit;
    private string laterFruit;
    private float fruitSize;
    private GameObject currentFruit;

    private System.Random rng = new();


    // Start is called before the first frame update
    void Start()
    {
        if(spawner == null)
            spawner = GameObject.FindGameObjectWithTag("Spawn");
        if(uiController == null)
            uiController = GameObject.FindGameObjectWithTag("UIController");
        if(gameManager == null)
            gameManager = GameObject.FindGameObjectWithTag("GameManager");

        if(controlls == null)
        {
            controlls = new InputControlls();
            controlls.Player.SetCallbacks(this);
        }
        controlls.Player.Enable();
        mousePosition = 0f;
        dropPosition = 0f;
        fruitReady = false;
        nowFruit = null;
        laterFruit = null;
        PrepareFruit();
    }

    // Update is called once per frame
    void Update()
    {
        if (fruitHadTime && !gameManager.GetComponent<GameManagerScript>().Lost)
        {
            mousePosition = ReadMouseX();
            if(!fruitReady || currentFruit is null)
            {
                PrepareFruit();
            }
            else
            {
                CalculateDropPosition(mousePosition);
            }
        }
    }

    void OnApplicationFocus()
    {
        if (!Application.isFocused)
            gameIsInFocus = false;
        if (Application.isFocused)
            StartCoroutine(RegainFocus());
    }
    private IEnumerator RegainFocus()
    {
        yield return new WaitForSeconds(0.5f);
        gameIsInFocus = true;
    }
    public float ReadMouseX()
    {
        return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x;
    }
    public void CalculateDropPosition(float mousePositionX)
    {
        dropPosition = mousePosition;
        if((mousePosition - (fruitSize / 2)) <= -3.4) dropPosition = (-3.4f + (fruitSize / 2) + 0.01f);
        if((mousePosition + (fruitSize / 2)) >=  3.4) dropPosition = ( 3.4f - (fruitSize / 2) - 0.01f);
        if (currentFruit == null) Debug.Log("1");
        currentFruit.transform.position = new Vector3(dropPosition, 9, 0);
    }
    public void OnDropFruit(InputAction.CallbackContext context)
    {
        if(!gameIsInFocus)
            return;
        if(gameManager.GetComponent<GameManagerScript>().Lost)
            return;
        if(uiController.GetComponent<UiControllerScript>().PlayerIsInUi)
            return;
        if (ReadMouseX() > 5 && Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).y >= 7.9)
            return;

        if(context.started && fruitHadTime && !gameManager.GetComponent<GameManagerScript>().Lost)
        {
            CalculateDropPosition(ReadMouseX());
            fruitReady = false;
            if (currentFruit == null) Debug.Log("2");
            currentFruit.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            Vector3 fruitPosition = currentFruit.GetComponent<Rigidbody2D>().position; //These two lines prevent the fruit from floating if mouse
            currentFruit.GetComponent<Rigidbody2D>().position = new Vector3(fruitPosition.x, (fruitPosition.y - 0.01f), 0); //was not moving when dropped
            currentFruit.GetComponent<FruitScript>().wasDropped = true;
            fruitHadTime = false;
            StartCoroutine(FruitDropDelay());
        }
    }
    private IEnumerator FruitDropDelay()
    {
        yield return new WaitForSeconds(0.9f);
        fruitHadTime = true;
    }


    private GameObject PrepareFruit()
    {
        currentFruit = null;
        if(nowFruit is null || laterFruit is null)
        {
            nowFruit = "Cherry";
            laterFruit = "Cherry";
        }
        else
        {
            nowFruit = laterFruit;

            int num = rng.Next(1,101);
            if (num > 90 && num <= 100)
                laterFruit = "Orange";
            else if (num > 70 && num <= 90)
                laterFruit = "Tangerine";
            else if (num > 50 && num <= 70)
                laterFruit = "Grape";
            else if (num > 25 && num <= 50)
                laterFruit = "Strawberry";
            else
                laterFruit = "Cherry";
        }
        uiController.GetComponent<UiControllerScript>().ChangeNextFruit(laterFruit);

        GameObject fruit = spawner.GetComponent<SpawnerScript>().SpawnFruit(nowFruit, new Vector3(0, 30, 0)); //Normal should be y=9
        fruit.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        fruitSize = fruit.GetComponent<CircleCollider2D>().bounds.size.x;
        fruitReady = true;
        currentFruit = fruit;
        if (currentFruit == null) Debug.Log("4");
        return fruit;
    }
}
