using UnityEngine;

public class FoodThrower : MonoBehaviour
{
    public string triggerName;
    public GameObject[] foodstuffs;
    public float throwForce;

    private Rigidbody heldFood;

    void Update()
    {
        // If the hand's trigger button has been pressed
        if(Input.GetButtonDown(triggerName))
        {
            // Spawn some food in the hand
            SpawnFood();
        }

        // If the hand is holding some food, and the trigger button was released
        if(Input.GetButtonUp(triggerName))
        {
            // Throw the food
            ThrowFood();
        }
    }

    private void SpawnFood()
    {
        // Instantiate a random food item in the hand
        heldFood = Instantiate(foodstuffs[Random.Range(0, foodstuffs.Length)], transform).GetComponent<Rigidbody>();

        // Make sure the food follows the hand exactly
        heldFood.isKinematic = true;
        heldFood.useGravity = false;
    }

    private void ThrowFood()
    {
        // Unparent the held food from the hand
        heldFood.transform.SetParent(null);

        // Make the food obey physics and gravity
        heldFood.isKinematic = false;
        heldFood.useGravity = true;

        // Apply a force to the food
        heldFood.AddForce(transform.forward * throwForce);
    }
}
