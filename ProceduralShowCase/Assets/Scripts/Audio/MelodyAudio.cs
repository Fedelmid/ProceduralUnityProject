using ATKSharp.Generators.Oscillators.Wavetable;
using ATKSharp.Envelopes;
using ATKSharp.Modifiers;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MelodyAudio : MonoBehaviour
{       
    // ----- Melody sound variables ----- //
    float[] toneCMajorHz,
            toneGMajorHz,
            toneAMinorHz;

    float toneAmplitude = .7f;
    float longToneAmplitude = .3f;

    WTSine toneGeneratorC, 
           toneGeneratorG,
           toneGeneratorA;

    CTEnvelope toneEnveloper, 
               longToneEnveloper;
    
    Coroutine toneCoroutine,
              longToneCoroutine;
    // ---------------------------------- //

    LowPass lowPass;

    // ------------ Timer -------------- //
    const float TICK_TIMER_MAX = .2f;
    int tick;
    float tickTimer;

    void Awake()
    {
        tick = 0;
    }
    // ---------------------------------- //

    void Start()
    {
        toneCMajorHz = new float[3] { 261.6f, 329.6f, 392f }; // C F G
        toneGMajorHz = new float[3] { 392f, 493.9f, 293.7f }; // G B D
        toneAMinorHz = new float[3] { 440f, 523.2f, 659.2f }; // A C E

        toneGeneratorC = new WTSine(toneCMajorHz[0]);
        toneGeneratorG = new WTSine(toneGMajorHz[0]);
        toneGeneratorA = new WTSine(toneAMinorHz[0]);

        toneEnveloper = new CTEnvelope(20, 5, .7f, 3000); // (attackTime, decayTime, sustainLevel, releaseTime)
        longToneEnveloper = new CTEnvelope(20, 100, .3f, 3000);

        lowPass = new LowPass();
    }

    void Update()
    {
        // Count up the timer
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIMER_MAX)
        {
            tickTimer -= TICK_TIMER_MAX;
            tick++;

            // Play a note from C major accord every 10th tick
            if (tick % 10 == 0)
            {
                toneGeneratorC.Frequency = toneCMajorHz[Random.Range(0, 2)];

                if (toneCoroutine != null)
                    StopCoroutine(toneCoroutine);
                toneCoroutine = StartCoroutine(Tone());
            }

            // Play a note from G major accord every 25th tick
            if (tick % 25 == 0)
            {
                toneGeneratorG.Frequency = toneGMajorHz[Random.Range(0, 2)];

                if (toneCoroutine != null)
                    StopCoroutine(toneCoroutine);
                toneCoroutine = StartCoroutine(Tone());
            }

            // Play a note from A minor accord every 7th tick
            if (tick % 7 == 0 || tick == 0)
            {
                toneGeneratorA.Frequency = toneAMinorHz[Random.Range(0, 2)];

                if (longToneCoroutine != null)
                    StopCoroutine(longToneCoroutine);
                longToneCoroutine = StartCoroutine(LongTone());
            }
        }        
    }

    // Play a tone
    IEnumerator Tone()
    {
        toneEnveloper.Gate = 1;
        yield return new WaitForSeconds(.1f);
        toneEnveloper.Gate = 0;
    }

    // Play a longer tone
    IEnumerator LongTone()
    {
        longToneEnveloper.Gate = 1;
        yield return new WaitForSeconds(5f);
        longToneEnveloper.Gate = 0;
    }

    // Make the sound sample and send it to the audio source
    void OnAudioFilterRead( float[] data, int channels )
    {
        for ( int i = 0; i < data.Length; i += channels )
        {
            // Add together the 3 tones and normalize
            float currentSample = toneEnveloper.Generate() * toneGeneratorC.Generate() * toneAmplitude; // C major tone
            currentSample += toneEnveloper.Generate() * toneGeneratorG.Generate() * toneAmplitude; // G major tone
            currentSample += longToneEnveloper.Generate() * toneGeneratorA.Generate() * longToneAmplitude / 3; // A minor tone

            currentSample = lowPass.Modify(currentSample);

            for( int j = 0; j < channels; j++ )
                data[i + j] = currentSample;
        }
    }
}
