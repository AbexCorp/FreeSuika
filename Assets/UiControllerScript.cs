using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiControllerScript : MonoBehaviour
{
    TextMeshProUGUI Score;
    TextMeshProUGUI NextFruit;

    private int score = 0;
    public GameObject data;

    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject.FindGameObjectWithTag("HighScore").GetComponent<TextMeshProUGUI>().text = gameManager.GetComponent<GameManagerScript>().HighScore.ToString();
        Score = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();
        UpdateScore();
        NextFruit = GameObject.FindGameObjectWithTag("NextFruit").GetComponent<TextMeshProUGUI>();
        ChangeNextFruit("Cherry");
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
        //Debug.Log(NextFruit == null);////////////////////////
        NextFruit.text = $"Next Fruit: {fruit}";
    }
}
