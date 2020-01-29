using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // Size of island
    public int width = 64,
               height = 64;

    // Spawn pattern size scale
    public float scale = 20.0f;

    public float treeDensity = 0.1f;

    public GameObject treePrefab;
    public Terrain terrain;

    void Start()
    {
        plantTrees(treeDensity);
    }


    public void plantTrees (float density)
    {
        treeDensity = density;

        // Remove old trees
        clearIsland();

        for (int x = 2; x < width - 1; x++)
        {
            for (int y = 2; y < height - 1; y++)
            {
                // Get x and y coordinates in float number
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;

                // Procedural value from coordinates
                float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                // Check if a tree can be spawned
                if (noiseValue < density)
                {
                    // Spawn new tree on the island close to the surface
                    GameObject tree = Instantiate(treePrefab, new Vector3(x, terrain.terrainData.GetHeight(x, y), y), Quaternion.identity);
                    // Set a random height for the tree
                    tree.transform.localScale = new Vector3(1, Random.Range(0.7f, 2.0f), 1);
                }

            }
        }
    }

    // Change scale of coordinates for Perlin Noise
    public void scaleChange(float newScale)
    {
        scale = newScale;

        plantTrees(treeDensity);
    }

    // Remove all trees on the island
    public void clearIsland()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");

        foreach(GameObject t in trees)
        {
            Destroy(t);
        }
    }
}
