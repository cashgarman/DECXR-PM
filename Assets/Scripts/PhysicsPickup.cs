using System.Collections.Generic;
using UnityEngine;

public class PhysicsPickup : Pickup
{
    private FixedJoint joint;

    private Queue<Vector3> previousVelocities = new Queue<Vector3>();
    private Vector3 prevPosition;
    public float throwBoost;
    public int numVelocitySamples;

    internal override void OnPickup(Grabber grabber)
    {
        base.OnPickup(grabber);

        // Create a fixed joint between the object and the grabber
        joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = grabber.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Calculate the velocity of the object based on how far it moved in a frame
        var velocity = transform.position - prevPosition;

        // Remember the position this frame, so we can use it to calculate the velocity in the next frame
        prevPosition = transform.position;

        // Add this calculated velocity to the list of previous velocities
        previousVelocities.Enqueue(velocity);

        // Make sure we stay within the limit of previous velocities
        if(previousVelocities.Count > numVelocitySamples)
        {
            previousVelocities.Dequeue();
        }
    }

    internal override void OnDrop()
    {
        base.OnDrop();

        // Destroy the fixed joint
        Destroy(joint);

        // Calculate the smoothed velocity
        Vector3 smoothedVelocity = Vector3.zero;
        foreach(var previousVelocity in previousVelocities)
        {
            smoothedVelocity += previousVelocity;
        }
        smoothedVelocity /= previousVelocities.Count;

        // Apply the calculated velocity to the rigid body to throw the object
        GetComponent<Rigidbody>().velocity = smoothedVelocity * throwBoost;
    }
}