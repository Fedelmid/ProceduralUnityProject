
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    public int depth = 20;
    public int width = 256;
    public int height = 256;
    public float scale = 10.0f;

    public int islandType;

    public GridGenerator gridScript;

    //public float xOffset = 100f;
    //public float yOffset = 100f;

    void Start ()
    {
        //xOffset = Random.Range(0f, 9999f);
        //yOffset = Random.Range(0f, 9999f);

        GenerateIsland(islandType);
    }

    TerrainData GenerateTerrain(TerrainData terrainData, int order)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights(order));
        return terrainData;
    }

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

    float CalculateHeight ( int x, int y, int order ) // maybe add here the option to mix 1, 2 or 3 perlin noise
    {
        float xCoord = (float)x / width; //* scale; // + xOffset
        float yCoord = (float)y / height; //* scale; // + yOffset

        float n = 0.0f;

        for(int i = 1; i <= order; i++)
        {
            n += Mathf.PerlinNoise(xCoord * (scale * Mathf.Pow(2,i)), yCoord * (scale * Mathf.Pow(2, i)));
        }

        return n / order;
    }

    public void GenerateIsland(int type)
    {
        gridScript.clearIsland();

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData, type);

        gridScript.plantTrees(gridScript.treeDensity);
    }
}
