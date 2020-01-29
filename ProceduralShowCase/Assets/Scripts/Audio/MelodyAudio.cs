using ATKSharp.Generators.Oscillators.Trivial;
using ATKSharp.Generators.Oscillators.Wavetable;
using ATKSharp.Envelopes;
using ATKSharp.Modifiers;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MelodyAudio : MonoBehaviour
{
    [SerializeField]
    float[] toneHz; // = new float[3] { 261.6256f, 329.6276f, 391.9954f }; //261.6256f;

    [SerializeField]
    float toneAmplitude = .7f;

    WTSine toneGenerator;
    CTEnvelope toneEnveloper;

    LowPass lowPass;

    Coroutine toneCoroutine;

    const float TICK_TIMER_MAX = .2f;
    int tick;
    float tickTimer;

    void Awake()
    {
        tick = 0;
    }

    void Start()
    {
        toneHz = new float[3] { 261.6256f, 329.6276f, 391.9954f };

        toneGenerator = new WTSine(toneHz[0]);
        toneEnveloper = new CTEnvelope(20, 5, .7f, 3000);

        lowPass = new LowPass();
    }

    void Update()
    {
        //toneGenerator.Frequency = toneHz[0];

        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer -= TICK_TIMER_MAX;
            tick++;

            if (tick % 15 == 0) {

                toneGenerator.Frequency = toneHz[Random.Range(0, 2)];

                if (toneCoroutine != null)
                    StopCoroutine(toneCoroutine);
                toneCoroutine = StartCoroutine(Tone());
            }

            //          ^
            // add this | below but with other than 15 check and mix with other frequencies
        }        
    }

    IEnumerator Tone()
    {
        toneEnveloper.Gate = 1;
        yield return new WaitForSeconds(.1f);
        toneEnveloper.Gate = 0;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {

        for ( int i = 0; i < data.Length; i += channels )
        {
            float currentSample = toneEnveloper.Generate() * toneGenerator.Generate() * toneAmplitude;
            currentSample = lowPass.Modify(currentSample);
            for( int j = 0; j < channels; j++ )
            {
                data[i + j] = currentSample;
            }
        }
    }
}
