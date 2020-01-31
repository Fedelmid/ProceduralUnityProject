using UnityEngine;

public class TreeProperties : MonoBehaviour
{
    // Tree life length 
    public float lifeLength;

    // Timer variables
    const float TIME_MAX = 0.2f;
    float deltaTime;
    int tick;

    // Controller for animations
    Animator animController;

    void Awake()
    {
        // Set tree life length to a random value
        lifeLength = Random.Range(10, 100);

        // Initilize timer
        tick = 0;

        // Get animation controller
        animController = GetComponent<Animator>();

        // Set the animation speed
        animController.speed = map(lifeLength, 10f, 100f, 1f, 2f);
    }

    
    void Update()
    {
        // Count uo the timer
        deltaTime += Time.deltaTime;
        if(deltaTime >= TIME_MAX)
        {
            deltaTime -= TIME_MAX;
            tick++;
        }

        // Check if the tree's life has ended
        if (tick > lifeLength)
        {
            // Change animation to decay
            animController.SetBool("isDead", true);

            // Get the animation duration for decay
            float time = animController.runtimeAnimatorController.animationClips[1].length;

            // After decay animation has finished destroy tree object
            Invoke("Death", time);
        }
            
    }

    // Re-map value to different interval function
    public float map(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((lifeLength - start1) / (stop1 - start1));
    }

    // Tree death
    void Death()
    {
        Destroy(gameObject);
    }
}
