using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class OrderBoard : MonoBehaviour
{
    public Transform ingredientsList;
    public TMP_Text ingredientListItemPrefab;

    private string[] ingredients;
    public string[] Ingredients
    {
        get
        {
            return ingredients;
        }
        set
        {
            ingredients = value;

            // Clear the ingredients list
            ClearChildren(ingredientsList);

            // Populate the list of ingredients
            foreach(var ingredient in ingredients)
            {
                // Instantiate a list item for the ingredient
                var newListItem = Instantiate(ingredientListItemPrefab, ingredientsList);
                newListItem.text = ingredient;
            }
        }
    }

    void Awake()
    {
        // Clear the order board
        ClearBoard();

        //Name = "Ham & Cheese Sarnie";
        //Ingredients = new string[] { "Bread", "Butter", "Ham", "Cheese" };
    }

    private void ClearBoard()
    {
        // Clear the ingredients list
        ClearChildren(ingredientsList);
    }

    private void ClearChildren(Transform parent)
    {
        // Looping over all the parent's children
        foreach(Transform child in parent)
        {
            // Destroy the child
            Destroy(child.gameObject);
        }
    }

    public int GetNumCorrectIngredients(string[] providedIngredients, out string[] unexpectedIngredients)
    {
        // Make a list copy of the provided ingredients
        var providedIngredientsCopy = providedIngredients.ToList();

        // Get all the order ingredient texts
        var ingredientTexts = ingredientsList.GetComponentsInChildren<TMP_Text>();
        foreach(var ingredientText in ingredientTexts)
        {
            ingredientText.color = Color.white;
        }

        // Start counting correct ingredients
        int correctCount = 0;

        // For each order ingredient
        foreach(var orderIngredientText in ingredientTexts)
        {
            // For each of the provided ingredients
            foreach(var providedIngredient in providedIngredientsCopy)
            {
                // If the provided ingredient matches the order ingredient
                if(providedIngredient == orderIngredientText.text)
                {
                    // Highlight the order ingredient on the order
                    orderIngredientText.color = Color.green;

                    // Count the correct ingredient
                    correctCount++;

                    // Remove the provided ingredient from the list of provided ingredients
                    providedIngredientsCopy.Remove(providedIngredient);

                    // Continue with the next order ingredient
                    break;
                }
            }
        }

        // Return the unexpected ingredients
        unexpectedIngredients = providedIngredientsCopy.ToArray();

        // Return the number of correct ingredient
        return correctCount;
    }
}
