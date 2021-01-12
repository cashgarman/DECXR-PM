using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // If the space bar is being held down
        if(Input.GetKey(KeyCode.Space))
        {
            // Speed up the simulation of the world
            Time.timeScale = 5f;
        }
        // Otherwise
        else
        {
            // Restore the speed of the world simulation
            Time.timeScale = 1f;
        }
    }
}
