using UnityEngine;

public class TreeProperties : MonoBehaviour
{

    [HideInInspector]
    public float lifeLength;

    float lifeTime;

    float life_timer_max = 0.2f;

    [HideInInspector]
    public int tick;


    Animator animController;

    void Awake()
    {
        lifeLength = Random.Range(10f, 100f);
        tick = 0;

        animController = GetComponent<Animator>();

        animController.speed = map(lifeLength, 10f, 100f, 1f, 2f);      
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
            animController.SetBool("isDead", true);

            float time = animController.runtimeAnimatorController.animationClips[1].length;

            Invoke("Death", time);
        }
            
    }

    public float map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((lifeLength - start1) / (stop1 - start1));
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
