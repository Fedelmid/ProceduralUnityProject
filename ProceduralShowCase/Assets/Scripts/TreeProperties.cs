using UnityEngine;

public class TreeProperties : MonoBehaviour
{

    public float lifeLength;
    float lifeTime;

    float life_timer_max = 0.2f;
    int tick;

  
    void Awake()
    {
        lifeLength = Random.Range(0f, 100f);
        tick = 0;
    }

    
    void Update()
    {
        lifeTime += Time.deltaTime;
        if(lifeTime >= life_timer_max)
        {
            lifeTime -= life_timer_max;
            tick++;
        }

        if (tick > lifeLength)
        {
            Debug.Log(gameObject.name + " " + lifeLength);

            Destroy(gameObject);
        }
            
    }
}
