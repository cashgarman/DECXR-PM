using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Transform target;
    public float rocketPower;
    public GameObject engineGlow;
    private float fuel = 100f;
    public float fuelPerSecond;

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        // Turn the rocket towards its target
        transform.LookAt(target);

        // If the space bar is being held AND we have some fuel left
        if(Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            // Add some forward force (from the rocket engine) to the rocket's rigid body
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * rocketPower * Time.deltaTime);

            // Reduce the fuel left
            fuel -= fuelPerSecond * Time.deltaTime;
            Debug.Log(fuel);

            // Show the engine glow
            engineGlow.SetActive(true);
        }
        else
        {
            // Hide the engine glow
            engineGlow.SetActive(false);
        }
    }

    public void FlyToTarget(Transform _target)
    {
        target = _target;
    }
}
