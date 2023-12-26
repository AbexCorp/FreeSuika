using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiControllerScript : MonoBehaviour
{
    public Sprite Cherry; //1
    public Sprite Strawberry; //2
    public Sprite Grape; //3
    public Sprite Tangerine; //4
    public Sprite Orange; //5
    public Sprite Apple; //6
    public Sprite Lemon; //7
    public Sprite Peach; //8
    public Sprite Pineapple; //9
    public Sprite Melon; //10
    public Sprite Watermelon; //11
    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>
    {
        {"Cherry",  default}, //1
        {"Strawberry",  default}, //2
        {"Grape",  default}, //3
        {"Tangerine",  default}, //4
        {"Orange",  default}, //5
        {"Apple",  default}, //6
        {"Lemon",  default}, //7
        {"Peach",  default}, //8
        {"Pineapple",  default}, //9
        {"Melon",  default}, //10
        {"Watermelon",  default}, //11
    };


    TextMeshProUGUI Score;
    UnityEngine.UI.Image NextFruit;

    GameObject MenuButton;
    public bool PlayerIsInUi { get { if(MenuButton is null) return false; return MenuButton.GetComponent<GameUiScript>().PlayerIsInUi; } }


    private int score = 0;
    //public GameObject data;

    private GameObject gameManager;


    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        sprites["Cherry"] = Cherry; //1
        sprites["Strawberry"] = Strawberry; //2
        sprites["Grape"] = Grape; //3
        sprites["Tangerine"] = Tangerine; //4
        sprites["Orange"] = Orange; //5
        sprites["Apple"] = Apple; //6
        sprites["Lemon"] = Lemon; //7
        sprites["Peach"] = Peach; //8
        sprites["Pineapple"] = Pineapple; //9
        sprites["Melon"] = Melon; //10
        sprites["Watermelon"] = Watermelon; //11
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject.FindGameObjectWithTag("HighScore").GetComponent<TextMeshProUGUI>().text = $"Record: {gameManager.GetComponent<GameManagerScript>().HighScore}";
        Score = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        UpdateScore();
        NextFruit = GameObject.FindGameObjectWithTag("NextFruit").GetComponent<UnityEngine.UI.Image>();
        ChangeNextFruit("Cherry");
        MenuButton = GameObject.FindGameObjectWithTag("MenuButton");
    }
    public void UpdateScore(int score = 0)
    {
        this.score += score;
        Score.text = $"Score: {this.score}";
    }
    public int GetScore()
    {
        return score;
    }
    public void ChangeNextFruit(string fruit)
    {
        try
        {
            NextFruit.sprite = sprites[fruit];
        }
        catch (NullReferenceException) { } //Prevents errors when called before refference is set
    }
}
