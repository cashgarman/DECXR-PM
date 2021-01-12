using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreePlanter : MonoBehaviour
{
    public Transform placeholderTree;
    public Tree treePrefab;
    public Mesh[] treeMeshes;
    public TMP_Text numTreesLabel;
    public TMP_Text numGrassesLabel;
    public float grassGrowingDuration;
    public float timeBetweenGrassSprouts;
    public Transform grassPrefab;
    public float grassRadius;
    public float maxGrowth;
    public Slider growthProgressBar;
    public GameObject rainPrefab;

    private int numTrees;
    private int numGrasses;
    private float timeLeftGrowingGrass;
    private float timeSinceLastGrassSprouted;
    private bool raining;

    private void Start()
    {
        // Randomize the current tree
        RandomizeTree();

        UpdateUI();
    }

    private void RandomizeTree()
    {
        // Change the placeholder tree's mesh to a random tree mesh
        int num = Random.Range(0, treeMeshes.Length);
        placeholderTree.GetComponent<MeshFilter>().mesh = treeMeshes[num];
    }

    public void PlantTree()
    {
        // Make a copy of the placeholder tree in the scene
        Tree tree = Instantiate(treePrefab, transform.position, transform.rotation);
        
        // Use the randomly selected mesh from the placeholder tree on the newly planted tree
        Mesh placeholderTreeMesh = placeholderTree.GetComponent<MeshFilter>().mesh;
        tree.GetComponent<MeshFilter>().mesh = placeholderTreeMesh;

        // Increase the number of trees
        ++numTrees;
        UpdateUI();

        // Randomize the current tree
        RandomizeTree();
    }

    public void NextTree()
    {
        // Randomize the current tree
        RandomizeTree();
    }

    private void Update()
    {
        // Are we growing grass right now?
        if(timeLeftGrowingGrass > 0)
        {
            // Reduce the time left growing grass
            timeLeftGrowingGrass -= Time.deltaTime;

            // If enough time has past once the last grass sprouted
            timeSinceLastGrassSprouted += Time.deltaTime;
            if (timeSinceLastGrassSprouted >= timeBetweenGrassSprouts)
            {
                // Sprout a new grass
                SproutGrass();

                // Reset the time the last grass sprouted
                timeSinceLastGrassSprouted = 0;
            }
        }
    }

    public void GrowGrass()
    {
        // Start growing grass for a specfied number of seconds
        timeLeftGrowingGrass = grassGrowingDuration;

        // Sprout the first blade of grass
        SproutGrass();
    }

    private void SproutGrass()
    {
        // Calculate the position of the new grass
        Vector2 offset = Random.insideUnitCircle * grassRadius;
        Vector3 position = transform.position + offset.x * transform.right + offset.y * transform.forward;

        // Plant the grass
        Instantiate(grassPrefab, position, transform.rotation);

        // Update the number of grasses
        ++numGrasses;
        UpdateUI();
    }

    private void UpdateUI()
    {
        numTreesLabel.text = $"Number of Trees: {numTrees}";
        numGrassesLabel.text = $"Number of Grasses: {numGrasses}";

        // Calculate the total growth
        float totalGrowth = numTrees * 10f + numGrasses * 0.1f;
        float percent = totalGrowth / maxGrowth;

        // Update the growth progress bar
        growthProgressBar.value = percent;

        // If the growth bar has filled up
        if(percent >= 1 && !raining)
        {
            // Make it rain!
            raining = true;
            Instantiate(rainPrefab);
        }
    }
}
