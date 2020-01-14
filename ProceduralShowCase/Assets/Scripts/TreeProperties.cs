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

        Animator anim = GetComponent<Animator>();


        anim.speed = map(lifeLength, 0f, 100f, 0f, 1f);

        //Debug.Log(gameObject.name + " " + anim.speed);

            

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
            //Debug.Log(gameObject.name + " " + lifeLength);

            Destroy(gameObject);
        }
            
    }

    float map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((lifeLength - start1) / (stop1 - start1));
    }
}
