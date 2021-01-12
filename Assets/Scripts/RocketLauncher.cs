using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Rocket rocketPrefab;
    public Transform planet;

    // Update is called once per frame
    void Update()
    {
        // If the space bar is pressed
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Create the rocket
            Rocket rocket = Instantiate(rocketPrefab, transform.position, transform.rotation);

            // Tell the rocket to fly to the planet
            rocket.FlyToTarget(planet);

            // Disable the solar system camera (so no more rockets, and only 1 camera)
            gameObject.SetActive(false);
        }
    }
}
