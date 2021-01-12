using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Game game;
    public float speed;
    public float range;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the target and food
        Destroy(gameObject);
        Destroy(collision.collider.gameObject);

        // Tell the game a target was hit
        game.OnTargetHit(this);
    }

    private void Update()
    {
        // Animate the target's position away from the initial position using a sine wave over time
        transform.position = initialPosition + transform.right * Mathf.Sin(Time.time * speed) * range;
    }
}
