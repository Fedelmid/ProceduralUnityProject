using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLeafColor : MonoBehaviour
{
    public Material mat;
    float life, lifeTime;
    int time;
    TreeProperties script;

    // Start is called before the first frame update
    void Start()
    {
        script = gameObject.GetComponentInParent<TreeProperties>();
        life = script.lifeLength;
    }

    // Update is called once per frame
    void Update()
    {
        time = script.tick;
        lifeTime = script.map(time, 0f, life, 0f, 1f);
        //mat.SetFloat("Vector1_FF6FB49", lifeTime);
    }
}
