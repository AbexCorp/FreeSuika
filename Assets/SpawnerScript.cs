using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject Cherry;
    public GameObject Strawberry;
    public GameObject Grape;
    public GameObject Tangerine;
    public GameObject Orange;
    public GameObject Apple;
    public GameObject Lemon;
    public GameObject Peach;
    public GameObject Pineapple;
    public GameObject Melon;
    public GameObject Watermelon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject SpawnFruit(string tag, Vector3 position)
    {
        GameObject newFruit = null;
        switch(tag)
        {
            default:
            case "Cherry":
                newFruit = Instantiate(Cherry, position, Quaternion.identity);
                break;

            case "Strawberry":
                newFruit = Instantiate(Strawberry, position, Quaternion.identity);
                break;

            case "Grape":
                newFruit = Instantiate(Grape, position, Quaternion.identity);
                break;

            case "Tangerine":
                newFruit = Instantiate(Tangerine, position, Quaternion.identity);
                break;

            case "Orange":
                newFruit = Instantiate(Orange, position, Quaternion.identity);
                break;

            case "Apple":
                newFruit = Instantiate(Apple, position, Quaternion.identity);
                break;

            case "Lemon":
                newFruit = Instantiate(Lemon, position, Quaternion.identity);
                break;

            case "Peach":
                newFruit = Instantiate(Peach, position, Quaternion.identity);
                break;

            case "Pineapple":
                newFruit = Instantiate(Pineapple, position, Quaternion.identity);
                break;

            case "Melon":
                newFruit = Instantiate(Melon, position, Quaternion.identity);
                break;

            case "Watermellon":
                newFruit = Instantiate(Watermelon, position, Quaternion.identity);
                break;
        }
        return newFruit;
    }
    public void SpawnBiggerFruit(string tag, Vector3 position)
    {
        switch (tag)
        {
            case "Cherry":
                tag = "Strawberry";
                break;

            case "Strawberry":
                tag = "Grape";
                break;

            case "Grape":
                tag = "Tangerine";
                break;

            case "Tangerine":
                tag = "Orange";
                break;

            case "Orange":
                tag = "Apple";
                break;

            case "Apple":
                tag = "Lemon";
                break;

            case "Lemon":
                tag = "Peach";
                break;

            case "Peach":
                tag = "Pineapple";
                break;

            case "Pineapple":
                tag = "Melon";
                break;

            case "Melon":
                tag = "Watermellon";
                break;

            case "Watermellon":
                tag = "Watermellon";
                break;
        }
        SpawnFruit(tag, position).GetComponent<FruitScript>().GetCombined();
    }
}
