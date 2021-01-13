using UnityEngine;

public class Paddle : MonoBehaviour
{
    public Transform target;
    public float speed;

    private void Update()
    {
        // Calculate the target position the paddle should move to
        var targetPosition = new Vector3(
            target.position.x,      // Target's X coordinate
            target.position.y,      // Target's Y coordinate
            transform.position.z);  // The paddle's Z coordinate

        // Calculate the direction the paddle needs to move to reach the target
        var direction = Vector3.Normalize(targetPosition - transform.position);

        // Move the paddle towards its target
        transform.position += direction * speed * Time.deltaTime;

        // TODO: Add this to prevent the jittering
        // If we're close enough to the target
            // Just move to the target
    }
}
