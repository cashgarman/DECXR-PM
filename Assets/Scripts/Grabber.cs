using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public string gripInputName;
    public string triggerInputName;

    private Animator animator;
    private Pickup highlightedObject;
    private Pickup pickedUpObject;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // If the grip button is pressed
        if(Input.GetButtonDown(gripInputName))
        {
            // Play the grip animation
            animator.SetBool("Gripped", true);

            // If we have a highlight object
            if(highlightedObject != null)
            {
                // Pickup the highlighted object
                highlightedObject.OnPickup(this);
                pickedUpObject = highlightedObject;
            }
        }

        // If the grip button is released
        if (Input.GetButtonUp(gripInputName))
        {
            // Go back to the idle animation
            animator.SetBool("Gripped", false);

            // If we have a picked up object
            if (pickedUpObject != null)
            {
                // Drop the highlighted object
                pickedUpObject.OnDrop();
                pickedUpObject = null;
            }
        }

        // If the trigger button is pressed
        if (Input.GetButtonDown(triggerInputName))
        {
            // If we're holding something
            if(pickedUpObject != null)
            {
                // Start triggering the thing we're holding
                pickedUpObject.OnTriggerStart();
            }
        }

        // If the trigger button is released
        if (Input.GetButtonUp(triggerInputName))
        {
            // If we're holding something
            if (pickedUpObject != null)
            {
                // Stop triggering the thing we're holding
                pickedUpObject.OnTriggerStop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object touched can be picked up
        Pickup pickup = other.GetComponent<Pickup>();
        if(pickup != null)
        {
            // Highlight the object
            pickup.OnHighlight();
            highlightedObject = pickup;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the object touched can be picked up
        Pickup pickup = other.GetComponent<Pickup>();
        if (pickup != null)
        {
            // Unhighlight the object
            pickup.OnUnhighlight();
            highlightedObject = null;
        }
    }
}
