using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Paintbrush : KinematicPickup
{
    public LineRenderer brushStrokePrefab;
    public float segmentLength;

    private LineRenderer stroke;
    private List<Vector3> points = new List<Vector3>();

    internal override void OnTriggerStart()
    {
        base.OnTriggerStart();

        // Start a new brush stroke
        stroke = Instantiate(brushStrokePrefab);

        // Setup the list that will keep track of all the points on the brush stroke
        points.Clear();
        points.Add(transform.position);
        points.Add(transform.position);
    }

    internal override void OnTriggerStop()
    {
        base.OnTriggerStop();

        // Stop painting
        stroke = null;
    }

    private void Update()
    {
        // If we're painting
        if(stroke != null)
        {
            // Update the end of the stroke to match the paintbrush position
            points[points.Count - 1] = transform.position;

            // If the paintbrush has moved far enough away from the last point we added
            if (Vector3.Distance(points[points.Count - 2], points[points.Count - 1]) >= segmentLength)
            {
                // Add a new point to the points
                points.Add(transform.position);
            }

            // Update the line renderer
            UpdateLineRenderer();
        }
    }

    private void UpdateLineRenderer()
    {
        // Use the list of points to define the line
        stroke.positionCount = points.Count;
        stroke.SetPositions(points.ToArray());
    }
}
