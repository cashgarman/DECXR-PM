using UnityEngine;

public class ServingZone : MonoBehaviour
{
    public DinerManager game;

    private void OnTriggerEnter(Collider other)
    {
        // Make sure it's the serving plate that has entered the trigger
        if(other.tag == "ServingPlate")
        {
            // Let the game know the order has been submitted
            game.OnOrderSubmitted();
        }
    }
}
