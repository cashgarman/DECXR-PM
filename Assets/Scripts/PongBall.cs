using UnityEngine;

public class PongBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // If the object hit was a wall
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            // Play the ball hitting wall sound
            GetComponent<AudioSource>().Play();
        }
    }
}
