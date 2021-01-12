using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Color normalColour;
    public Color highlightedColour;

    private Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    internal void OnHighlight()
    {
        // Change the colour of the object to the highlighted colour
        material.color = highlightedColour;
    }

    internal void OnUnhighlight()
    {
        // Change the colour of the object back to its normal colour
        material.color = normalColour;
    }

    internal virtual void OnPickup(Grabber grabber)
    {
    }

    internal virtual void OnDrop()
    {
    }

    internal virtual void OnTriggerStart()
    {
    }

    internal virtual void OnTriggerStop()
    {
    }
}
