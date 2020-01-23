using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public int width = 256;
    public int height = 256;
    public float scale = 20.0f;

    public float treeDensity;

    public GameObject point;
    public Terrain terrain;

    

    void Start()
    {
        plantTrees(treeDensity);
    }


    public void plantTrees (float density)
    {

        Debug.Log(density);

        clearIsland();

        for (int x = 2; x < width - 1; x++)
        {
            for (int y = 2; y < height - 1; y++)
            {
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;

                float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                if (noiseValue < density)
                {
                    GameObject tree = Instantiate(point, new Vector3(x, terrain.terrainData.GetHeight(x, y), y), Quaternion.identity);
                    tree.transform.localScale = new Vector3(1, Random.Range(0.7f, 2.0f), 1);
                }

            }
        }
    }

    public void clearIsland()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");

        foreach(GameObject t in trees)
        {
            Destroy(t);
        }
    }
}
