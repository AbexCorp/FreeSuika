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

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("UIScene", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
    public void GameOver()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    public void ChangeNextFruit(string fruit)
    {
        NextFruit.text = $"Next Fruit: {fruit}";
    }
}
