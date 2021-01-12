using UnityEngine;

public class Teleporter : MonoBehaviour
{
	public string teleportInputName;
	public string turnInputName;
	public float range;
	public Transform teleportTarget;
	public Color validColour;
	public Color invalidColour;
	public Transform player;

	private LineRenderer ray;
	private bool targetValid;
	private bool turning;

	private void Start()
	{
		// Cache the line renderer
		ray = GetComponent<LineRenderer>();

		// Hide the ray
		ray.enabled = false;
		
		// Hide our teleport target
		teleportTarget.gameObject.SetActive(false);
	}

	private void Update()
	{
		// If the thumbstick is pressed forward
		if (Input.GetAxis(teleportInputName) < -0.5f)
		{
			// Show the ray
			ray.enabled = true;
			
			// If a raycast from the hand forward hit anything
			if (Physics.Raycast(transform.position, transform.forward, out var hit, range))
			{
				// Update the ray's end position
				UpdateRayEndPosition(hit.point);
				
				// If the thing we hit was an invalid teleport target
				if (hit.collider.gameObject.layer == LayerMask.NameToLayer("InvalidTeleportTarget"))
				{
					// Set the target to invalid
					SetTargetValid(false);
				}
				else
				{
					// Set the target to valid
					SetTargetValid(true);
				
					// Move the teleport target to the valid teleport position
					teleportTarget.position = hit.point + Vector3.up * 0.0001f;
				}
			}
			// If the raycast missed everything
			else
			{
				// Set the target to invalid
				SetTargetValid(false);

				// Update the ray's end position
				UpdateRayEndPosition(transform.position + transform.forward * range);
			}
		}
		// If the thumbstick is released
		else
		{
			// If the teleport target is valid
			if (targetValid)
			{
				// Teleport the player to the target
				player.position = teleportTarget.position;
			}
			
			// Hide the ray
			ray.enabled = false;
			
			// Set the target to invalid
			SetTargetValid(false);
		}
		
		// If the thumbstick was just turned left
		if (!turning && Input.GetAxis(turnInputName) < -0.5f)
		{
			// Flag the player as turning
			turning = true;
			
			// Turn the player left
			player.transform.Rotate(0, -45, 0);
		}
			
		// If the thumbstick was just turned right
		if (!turning && Input.GetAxis(turnInputName) > 0.5f)
		{
			// Flag the player as turning
			turning = true;
			
			// Turn the player right
			player.transform.Rotate(0, 45, 0);
		}
		
		// If the thumbstick was released
		if (Input.GetAxis(turnInputName) > -0.5 && Input.GetAxis(turnInputName) < 0.5)
		{
			// Unflag the player as turning	
			turning = false;
		}
	}

	private void SetTargetValid(bool isValid)
	{
		// Show or hide the teleport target
		teleportTarget.gameObject.SetActive(isValid);

		targetValid = isValid;
		
		// Change the colour of the teleport ray to reflect if it's valid
		if (isValid)
		{
			ray.material.color = validColour;
		}
		else
		{
			ray.material.color = invalidColour;
		}
	}

	private void UpdateRayEndPosition(Vector3 endPosition)
	{
		// Set the starting position of the teleport ray
		ray.SetPosition(0, transform.position);
		ray.SetPosition(1, endPosition);
	}
}
