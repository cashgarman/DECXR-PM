using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class IngredientPickup : PhysicsPickup
{
    public float respawnDelay = 3f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool shouldRespawn = true;

    internal override void OnPickup(Grabber grabber)
    {
        // Remember the original position and rotation of the ingredient
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        // If the ingredient should respawn
        if(shouldRespawn)
        {
            // Respawn it after a delay
            StartCoroutine(Respawn());

            // This picked up object should no longer respawn clones of itself
            shouldRespawn = false;
        }

        // Now we've handled anything to do with respawning, pickup the actual object
        base.OnPickup(grabber);
    }

    private IEnumerator Respawn()
    {
        // Delay a bit
        yield return new WaitForSeconds(respawnDelay);

        // Respawn the object back in the original position and rotation
        var respawnedIngredient = Instantiate(gameObject, originalPosition, originalRotation).GetComponent<IngredientPickup>();

        // Reset the respawned ingredient
        respawnedIngredient.Start();
        respawnedIngredient.OnUnhighlight();
        Destroy(respawnedIngredient.GetComponent<FixedJoint>());
        respawnedIngredient.shouldRespawn = true;   // TODO: Why does it work without this line still?
    }
}
