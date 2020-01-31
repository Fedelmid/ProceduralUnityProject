using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLeafColor : MonoBehaviour
{
    Material mat; // leaf material
    float life, lifeTime, colorChangeSpeed;

    const float TICK_TIMER_MAX = .2f;
    int tick;
    float tickTimer;

    void Awake()
    {
        tick = 0;
    }

    void Start()
    {
        // Get the material on the leaves
        mat = GetComponent<Renderer>().material;

        // Tree's life length
        life = gameObject.GetComponentInParent<TreeProperties>().lifeLength;

        lifeTime = 0.0f;

        colorChangeSpeed = 1.0f / life;
    }

    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer -= TICK_TIMER_MAX;
            tick++;

            // Make new value for the slider
            lifeTime += colorChangeSpeed;
            
            // Change the slider value from green to yellow
            mat.SetFloat("Vector1_C6DA6EA3", lifeTime);
        }
    }
}
