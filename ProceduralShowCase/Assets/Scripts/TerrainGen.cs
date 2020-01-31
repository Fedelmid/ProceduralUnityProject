
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    // Island object
    Terrain terrain;

    // Island dimensions
    public int depth = 20;
    public int width = 64;
    public int height = 64;
    public float scale = 10.0f;

    // Indicator of how many Perlin Noise should 
    // run on each coordinate
    public int islandType;

    // Reference in order to access clearIsland function
    public GridGenerator gridScript;

    void Start ()
    {
        terrain = GetComponent<Terrain>();
        GenerateIsland(islandType);
    }

    // Generate the a specific type shape for the island 
    public void GenerateIsland(int type)
    {
        // Clear island from trees to prevent new island shape
        // to occlude/intersect trees
        gridScript.clearIsland();  

        terrain.terrainData = GenerateTerrain(terrain.terrainData, type);

        // Re-plant trees
        gridScript.plantTrees(gridScript.treeDensity);
    }

    TerrainData GenerateTerrain(TerrainData terrainData, int order)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights(order));
        return terrainData;
    }

    // Generate the height for each point in the terrain
    float[,] GenerateHeights(int order)
    {
        float[,] heights = new float[width, height];

        for( int x = 1; x < width; x++)
        {
            for ( int y = 1; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y, order);
            }
        }
        return heights;
    }

    // Generate point's height with Perlin Noise
    float CalculateHeight ( int x, int y, int order ) 
    {
        // Get float coordinates
        float xCoord = (float)x / width; 
        float yCoord = (float)y / height; 

        // Sum of Perlin noise results
        float n = 0.0f;

        // Order is same as island type
        for(int i = 1; i <= order; i++)
        {
            n += Mathf.PerlinNoise(xCoord * (scale * Mathf.Pow(2,i)), yCoord * (scale * Mathf.Pow(2, i)));
        }

        // Return normalized height
        return n / order;
    }

    
}
