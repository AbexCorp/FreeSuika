using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour, InputControlls.IPlayerActions
{
    public GameObject spawner;
    private GameObject uiController;
    InputControlls controlls;

    private float mousePosition;
    private float dropPosition;
    private bool fruitReady;
    private bool fruitHadTime = true;

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
        if (fruitHadTime)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()).x;
            if(!fruitReady || currentFruit is null)
            {
                PrepareFruit();
            }
            else
            {
                dropPosition = mousePosition;
                if((mousePosition - (fruitSize / 2)) <= -3.4) dropPosition = (-3.4f + (fruitSize / 2) + 0.01f);
                if((mousePosition + (fruitSize / 2)) >=  3.4) dropPosition = ( 3.4f - (fruitSize / 2) - 0.01f);
                if (currentFruit == null) Debug.Log("1");
                currentFruit.transform.position = new Vector3(dropPosition, 9, 0);
            }
        }
    }

    public void OnDropFruit(InputAction.CallbackContext context)
    {
        if(context.started && fruitHadTime)
        {
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
        if (currentFruit == null) Debug.Log("3");
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
        fruitSize = fruit.GetComponent<SpriteRenderer>().bounds.size.x;
        fruitReady = true;
        currentFruit = fruit;
        if (currentFruit == null) Debug.Log("4");
        return fruit;
    }
}
