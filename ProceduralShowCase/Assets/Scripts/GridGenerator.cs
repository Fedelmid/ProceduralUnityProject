﻿using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    float[,] map;

    public int width = 256;
    public int height = 256;
    public float scale = 20.0f;

    public float treeDensity = 0.5f;

    public GameObject point;
    public Terrain terrain;

    void Start()
    {
        map = new float[width, height];

        for ( int x = 2; x < width-1; x++ )
        {
            for ( int y = 2; y < height-1; y++ )
            {
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;

                //Debug.Log("x: " + xCoord + " y: "+ yCoord + " Perlin noise " + Mathf.PerlinNoise(xCoord, yCoord));

                float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                if ( noiseValue  < treeDensity )
                {
                    GameObject tree = Instantiate(point, new Vector3(x, terrain.terrainData.GetHeight(x, y), y), Quaternion.identity);
                    tree.transform.localScale = new Vector3( 1, scale * noiseValue, 1 );
                }
                    
            }
        }
    }


}