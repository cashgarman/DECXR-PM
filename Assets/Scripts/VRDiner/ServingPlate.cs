using System.Collections.Generic;
using UnityEngine;

public class ServingPlate : MonoBehaviour
{
    public Transform raycastStart;
    public LayerMask raycastMask;
    public float raycastRadius;

    public string[] Ingredients
    {
        get
        {
            // Get all the ingredients on the plate
            var ingredientHits = GetIngredientsOnPlate();

            // Create the list of ingredient names
            List<string> foundIngredients = new List<string>();
            foreach(var hit in ingredientHits)
            {
                foundIngredients.Add(hit.collider.name.Replace("(Clone)", ""));
            }

            return foundIngredients.ToArray();
        }
    }

    private RaycastHit[] GetIngredientsOnPlate()
    {
        // Fire as raycast down from the raystart start towards the plate and get all the ingredients hit
        return Physics.SphereCastAll(raycastStart.position, raycastRadius, raycastStart.forward, 1000f, raycastMask);
    }

    private void Awake()
    {
        // Let the plate contents script know about this new plate
        //FindObjectOfType<PlateContents>().servingPlate = this;
    }

    internal void Clear()
    {
        // For each ingredient on the plate
        var hits = GetIngredientsOnPlate();
        foreach (var hit in hits)
        {
            // Destroy it
            Destroy(hit.collider.gameObject);
        }

        // Destroy the plate
        Destroy(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach(var ingredient in Ingredients)
            {
                Debug.Log(ingredient);
            }
        }
    }
}
