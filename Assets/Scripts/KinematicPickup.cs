using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicPickup : Pickup
{
    internal override void OnPickup(Grabber grabber)
    {
        base.OnPickup(grabber);

        // Parent this object to the grabber that picked it up
        transform.SetParent(grabber.transform);

        // Make this object kinematic and disable gravity
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
        rigidBody.useGravity = false;
    }

    internal override void OnDrop()
    {
        base.OnDrop();

        // Unparent this object from everything
        transform.SetParent(null);

        // Make this object non-kinematic and enable gravity
        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.useGravity = true;
    }
}
