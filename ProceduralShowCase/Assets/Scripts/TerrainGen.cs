
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    public int depth = 20;
    public int width = 256;
    public int height = 256;
    public float scale = 20.0f;

    //public float xOffset = 100f;
    //public float yOffset = 100f;

    void Start ()
    {
        //xOffset = Random.Range(0f, 9999f);
        //yOffset = Random.Range(0f, 9999f);

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for( int x = 1; x < width; x++)
        {
            for ( int y = 1; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }

        return heights;
    }

    float CalculateHeight ( int x, int y ) // maybe add here the option to mix 1, 2 or 3 perlin noise
    {
        float xCoord = (float) x / width * scale; // + xOffset
        float yCoord = (float) y / height * scale; // + yOffset

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
