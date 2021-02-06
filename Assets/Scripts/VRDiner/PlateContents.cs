using TMPro;
using UnityEngine;

public class PlateContents : MonoBehaviour
{
    public Transform ingredientsList;
    public TMP_Text ingredientListItemPrefab;
    public ServingPlate servingPlate;

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
            foreach (var ingredient in ingredients)
            {
                // Instantiate a list item for the ingredient
                var newListItem = Instantiate(ingredientListItemPrefab, ingredientsList);
                newListItem.text = ingredient;
            }
        }
    }

    private void ClearChildren(Transform parent)
    {
        // Looping over all the parent's children
        foreach (Transform child in parent)
        {
            // Destroy the child
            Destroy(child.gameObject);
        }
    }

    private void Update()
    {
        Ingredients = servingPlate.Ingredients;
    }
}
