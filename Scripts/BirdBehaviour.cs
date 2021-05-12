using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    //Audio Configurations
    AudioSource source;
	float minVolume, maxVolume; //Volume
	float deltaPitch; //Pitch
	float minDoppler, maxDoppler; //Doppler

    float timer = 0;
    float change;
    bool CanPlay = true;

    //Time
    [Header("Time between plays")]
    public int minimumTime, maximumTime;


    // Start is called before the first frame update
    void Start()
    {
        change = Random.Range(minimumTime, maximumTime);

        source = GetComponent<AudioSource>();

        minVolume = 0.1f;
        maxVolume = 0.7f;
        minDoppler = 0.5f;
        maxDoppler = 3.0f;
        deltaPitch = 0.5f;

        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        CanPlay = true;
        PlayandRandomize();

    }

    private void PlayandRandomize()
    {
        if (timer > change && CanPlay == true && source.isPlaying == false)
        {

            source.volume = Random.Range(minVolume, maxVolume);
            source.dopplerLevel = Random.Range(minDoppler, maxDoppler);
            source.pitch = Random.Range(1 - deltaPitch, 1 + deltaPitch);
            source.Play();
            change = Random.Range(minimumTime, maximumTime);
            timer = 0;
            CanPlay = false;


        }
    }
}
