using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioLowPassFilter))]
public class ProceduralAudio : MonoBehaviour
{
    float time, timer_max = 0.2f;
    int tick;

    private float sampling_frequency = 48000;

    [Range(0f, 1f)]
    public float noiseRatio = 0.5f;

    //for noise part
    [Range(-1f, 1f)]
    public float offset;

    public float cutoffOn = 800;
    public float cutoffOff = 100;

    public bool cutOff;

    //for tonal part
    public float frequency = 440f;
    public float gain = 0.05f;

    private float increment;
    private float phase;


    public float[] frequencies;


    System.Random rand = new System.Random();
    AudioLowPassFilter lowPassFilter;

    void Awake()
    {
        frequencies = new float[3]{ 261.6256f, 329.6276f, 391.9954f};

        sampling_frequency = AudioSettings.outputSampleRate;

        lowPassFilter = GetComponent<AudioLowPassFilter>();
        Update();
    }

    int randomNum()
    {
        return Random.Range(0, 2);
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        float tonalPart = 0;
        float noisePart = 0;

        // update increment in case frequency has changed
        increment = frequency * 2f * Mathf.PI / sampling_frequency;

        for (int i = 0; i < data.Length; i++)
        {
            //noise
            noisePart = noiseRatio * (float)(rand.NextDouble() * 2.0 - 1.0 + offset);

            phase = phase + increment;
            if (phase > 2 * Mathf.PI) phase = 0;

            //tone
            tonalPart = (1f - noiseRatio) * (float)(gain * Mathf.Sin(phase));

            //together
            data[i] =  tonalPart; // noisePart +

            // if we have stereo, we copy the mono data to each channel
            if (channels == 2)
            {
                data[i + 1] = data[i];
                i++;
            }
        }
    }

    void Update()
    {
        lowPassFilter.cutoffFrequency = cutOff ? cutoffOn : cutoffOff;

        time += Time.deltaTime;

        if (time >= timer_max)
        {
            time -= timer_max;
            tick++;

            if (tick % 10 == 0)
            {
                frequency = frequencies[Random.Range(0, 2)];
            }
        }

        
    }


}
