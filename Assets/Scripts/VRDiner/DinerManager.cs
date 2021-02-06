using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DinerManager : MonoBehaviour
{
    public GameObject[] baseIngredients;
    public GameObject[] fillingIngredients;
    public int minNumFillings;
    public int maxNumFillings;
    public OrderBoard orderBoard;
    public ScoreBoard scoreBoard;
    public ServingPlate servingPlatePrefab;

    public Transform servingPlateSpawnPoint;

    private ServingPlate servingPlate;
    private Stopwatch orderTimer;

    void Start()
    {
        // Start our first order
        StartNewOrder();
    }

    public void StartNewOrder()
    {
        // Clear away any old plate
        if(servingPlate != null)
        {
            servingPlate.Clear();
        }

        // Spawn a new plate
        servingPlate = Instantiate(servingPlatePrefab, servingPlateSpawnPoint.position, servingPlateSpawnPoint.rotation);

        // Generate the ingredients for our new order
        orderBoard.Ingredients = GenerateOrderIngredients();

        // Hide the score board
        scoreBoard.gameObject.SetActive(false);

        // Start the order timer
        orderTimer = Stopwatch.StartNew();
    }

    private string[] GenerateOrderIngredients()
    {
        // Create an empty list of ingredients
        var ingredients = new List<string>();

        // Add a random base to the ingredients
        ingredients.Add(baseIngredients[Random.Range(0, baseIngredients.Length)].name);

        // Choose a random number of fillings
        var numberOfFillings = Random.Range(minNumFillings, maxNumFillings + 1);

        // For each filling
        for(var i = 0; i < numberOfFillings; ++i)
        {
            // Add a random filling to the ingredients
            ingredients.Add(fillingIngredients[Random.Range(0, fillingIngredients.Length)].name);
        }

        // Return the finished list of ingredients
        return ingredients.ToArray();
    }

    public void OnOrderSubmitted()
    {
        // Get the number of correct ingredients (as well as the number of unexpected ingredients)
        var plateIngredients = servingPlate.Ingredients;
        int correctIngredients = orderBoard.GetNumCorrectIngredients(plateIngredients, out var unexpectedIngredients);

        foreach(var unexpectedIngredient in unexpectedIngredients)
        {
            Debug.Log($"Found unexpected {unexpectedIngredient}");
        }

        // Stop the order timer
        orderTimer.Stop();

        // Calculate the score and display it
        scoreBoard.CalculateScore(correctIngredients, orderBoard.Ingredients.Length, unexpectedIngredients.Length, orderTimer.Elapsed.TotalSeconds);
        scoreBoard.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            orderBoard.Ingredients = GenerateOrderIngredients();
        }
    }
}
