using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR;

public class Paddle : MonoBehaviour
{
    public Transform target;
    public float speed;
    public bool instantMove;
    public float minMoveDistance;
    public Team team;
    private Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Calculate the target position the paddle should move to
        var targetPosition = new Vector3(
            target.position.x,      // Target's X coordinate
            target.position.y,      // Target's Y coordinate
            transform.position.z);  // The paddle's Z coordinate

        // If the paddle should move instantly
        if(instantMove)
        {
            // Move the paddle to the target's position
            transform.position = targetPosition;
        }
        // Otherwise, if the target is far enough away from the paddle
        else if(Vector3.Distance(targetPosition, transform.position) > minMoveDistance)
        {
            // Calculate the direction the paddle needs to move to reach the target
            var direction = Vector3.Normalize(targetPosition - transform.position);

            // Move the paddle towards its target
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the object is the ball
        if (collision.collider.gameObject.name == "Ball")
        {
            // Play the terrible ball hitting paddle sound
            GetComponent<AudioSource>().Play();

            // Punch the scale of the paddle
            var startingScale = transform.localScale;
            transform.DOPunchScale(startingScale * 1.15f, 0.15f);
            
            // Punch the paddle's alpha to full and back
            var initialAlpha = material.color.a;
            material.DOFade(1f, 0.15f / 2f).OnComplete(() => material.DOFade(initialAlpha, 0.15f / 2f));

            // If this paddle belongs to the player
            if(team == Team.Player)
            {
                var leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
                leftHand.SendHapticImpulse(0, .5f, .5f);
            }
        }
    }
}
