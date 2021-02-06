using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public TMP_Text ingredientsText;
    public TMP_Text unexpectedText;
    public TMP_Text scoreText;
    public string leftTriggerName;
    public string rightTriggerName;
    public DinerManager game;

    public void CalculateScore(int correctIngredients, int totalIngredients, int unexpectedIngredients, double totalSeconds) 
    {
        // TODO: Calculate a real score based on everything that happened
        var score = 42;

        // Ingredients: 999/999 100%
        ingredientsText.text = $"Ingredients: {correctIngredients}/{totalIngredients} {correctIngredients / (float)totalIngredients * 100f:F0}%";

        // Unexpected: 999
        unexpectedText.text = $"Unexpected: {unexpectedIngredients}";

        // Score: 999,999,999
        scoreText.text = $"Score: {score}";
    }

    private void Update()
    {
        // Wait for a trigger to be pressed
        if(Input.GetButtonDown(leftTriggerName) || Input.GetButtonDown(rightTriggerName))
        {
            // Start a new order
            game.StartNewOrder();
        }
    }
}