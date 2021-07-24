using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FFT Shader-Based
//Mateo Larrea 2021



[RequireComponent(typeof(AudioSource))]


public class ShaderAurora : MonoBehaviour
{

    //|VARIABLES|

    //1) FFT INPUT
    AudioSource _audioSource;

    [SerializeField] float[] samples = new float[512]; //Total # of Samples extracted

    float[][] _bandBuffer = new float[1][]; //****

    float[][] _bufferDecrease = new float[1][]; //****

    public static float[] freqBand = new float[8]; //Distribution:
                                                   //20-60 Hz - Sub Bass
                                                   //60-250 - Bass
                                                   //250-500 - Low Midrange
                                                   //500-2kHz - Midrange
                                                   //2-4kHz - Upper Midrange
                                                   //4-6kHz - Presence
                                                   //6-20kHz - Brilliance

    //2) FFT OUTPUT

    //Objects
    GameObject[][] _samplesAsObjects = new GameObject[1][]; //****

    [Header("Object's Scale")]
    [Tooltip("Initial transform.scale of objects/samples")]
    [Range(1, 20)] public int _startScale;

    [Range(50f, 200f)] public float _scale = 100; //Determines the variation in Y of the affected objects

    public float yOffset;


    //Buffer
    [Header("Buffer Config.")]
    [Tooltip("Makes movement more continous")]
    public bool UseBuffer; //When to go into Buffer mode

    [Range(0.05f, 0.0005f)] public float bufferSpeedA; //Delta

    [Range(0.9f, 1.5f)] public float bufferSpeedB; //Delta

    //Objects and Parameters
    [Header("Arrays MUST contain same number of elements")]
    [Tooltip("Must be the same number than the size of _samplesAsObjects[x][]")]
    public int[] numberOfSamples; //How many object per group (Samples) ****

    [Tooltip("Must be the same number than the size of _samplesAsObjects[x][]")]
    public GameObject[] SamplePrefabs; //Object that will conform each group ****

    [Tooltip("Must be the same number than the size of _samplesAsObjects[x][]")]
    public int[] Distance; //Distance from the object containing the script ****

    public float distanceScaling = 1f;




    //Shader Parameters
    [SerializeField]
    private Material deformationMaterial = null;

  


    // Start is called before the first frame update
    void Start()
    {

        _audioSource = GetComponent<AudioSource>();

        ////Assigns how many objects/samples need to be created/added for x numbers of groups
        for (int a = 0; a < _samplesAsObjects.Length; a++)
        {
            _samplesAsObjects[a] = new GameObject[numberOfSamples[a]];
            _bandBuffer[a] = new float[numberOfSamples[a]];
            _bufferDecrease[a] = new float[numberOfSamples[a]];

        }

        //Initialize Buffer Parameters
        bufferSpeedA = 0.005f; //change to define how agressiveness of reaction
        bufferSpeedB = 1.15f; //change to define how agressiveness of reaction

        //Environment is created
        AssignGroups();

    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource(); //Gets the Data

        MakeFrequencyBands(); //Splits frequencies into groups (based on value)

        ScalingMovement(); //Makes objects react to the two previous methods

        BandBuffer(); //Does the same than ScalingMovement() but in a more continous way
    }

    //Custom Funcs/Methods

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman); //where the magic happens
    }

    void MakeFrequencyBands()
    {

        int count = 0;

        for (int b = 0; b < 8; b++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, b) * 2; // --> 2, 4, 8, 16, 32,....., 256

            if (b == 7)
            {
                sampleCount += 2; // to get 512 intead of 510
            }

            for (int c = 0; c < sampleCount; c++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[b] = average * 10;

        }

    }


    void AssignGroups() //this section is not too important at the moment since we only have one object
    {
        for (int d = 0; d < _samplesAsObjects.Length; d++)
        {
            for (int e = 0; e < numberOfSamples[d]; e++)
            {
                //Create the object
                GameObject copiedObject = (GameObject)Instantiate(SamplePrefabs[d], this.transform.position, this.transform.rotation); //Copies sampleObjectPrefab
                copiedObject.transform.position = this.transform.position; //Assigns it to position of the object where the script is attached
                copiedObject.transform.parent = this.transform;
                //copiedObject.transform.localPosition = this.transform.localPosition;
                copiedObject.name = "Object #" + e; //Changes name and adds number to copy

                //Position the object
                float EulerY;
                this.transform.eulerAngles = new Vector3(0, EulerY = (float)-360 / numberOfSamples[d] * e, 0); //Euler Y = 360/# of objects in group

                copiedObject.transform.position = new Vector3(0, yOffset, Distance[d] * distanceScaling);

                _samplesAsObjects[d][e] = copiedObject;

            }
        }
    }

    //To scale fft data to a range that makes sense in shader graph

    float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }



    void ScalingMovement()
    {
        for (int f = 0; f < _samplesAsObjects.Length; f++)
        {
            for (int g = 0; g < numberOfSamples[f]; g++)
            {
                if (_samplesAsObjects[f] != null)
                {
                    if (UseBuffer == true)
                    {
                        float _height;
                        float _constant = 5.2f;
                        float _offset = 2f;

                        _samplesAsObjects[f][g].transform.localScale = new Vector3(10, _height = (_bandBuffer[f][g] * 30) + _startScale, 10);
                        
                        if (_height > _constant)
                        {
                            deformationMaterial.SetColor("_Albedo",Color.grey);
                            deformationMaterial.SetColor("_ColorA", Color.cyan);
                            deformationMaterial.SetColor("_ColorB", Color.blue);
                        }

                        else if (_height > _constant + _offset)
                        {
                            deformationMaterial.SetColor("_Albedo", Color.red);
                            deformationMaterial.SetColor("_ColorA", Color.yellow);
                            deformationMaterial.SetColor("_ColorB", Color.gray);

                        }
                    
                        else
                        {
                            deformationMaterial.SetColor("_Albedo", Color.blue);
                            //deformationMaterial.SetColor("_ColorA", Color.blue);
                        }


                        deformationMaterial.SetFloat("_FresnelPower", Scale(0f, 10f, 1f, 10f, (float)samples[g]));
                        deformationMaterial.SetFloat("_NoiseScale", Scale(0f, 10f, 10f, 25f, (float)samples[g]));
                        deformationMaterial.SetFloat("_ColorChange", Scale(0f, 10f, 0f, 2f, (float)samples[g]));
                       
                    }
                    else
                    {
                        _samplesAsObjects[f][g].transform.localScale = new Vector3(10, (samples[g] * _scale) + _startScale, 10);
                        deformationMaterial.SetFloat("_FresnelPower", Scale(0f, 10f, 3f, 10f, (float)samples[g]));
                        //deformationMaterial.SetFloat("_TimeSpeed", Scale(0f, 10, 0.1f, 2f, (float)samples[g]));
                        deformationMaterial.SetFloat("_NoiseScale", Scale(0f, 10f, 10f, 25f, (float)samples[g]));


                      

                    }

                }
            }

        }



    }

    void BandBuffer()
    {
        for (int h = 0; h < _samplesAsObjects.Length; h++)
        {
            for (int i = 0; i < numberOfSamples[h]; i++)
            {

                if (freqBand[h] > _bandBuffer[h][i])
                {

                    _bandBuffer[h][i] = freqBand[h];
                    _bufferDecrease[h][i] = bufferSpeedA;
                }

                if (freqBand[h] < _bandBuffer[h][i])
                {
                    _bandBuffer[h][i] -= _bufferDecrease[h][i];
                    _bufferDecrease[h][i] *= bufferSpeedB;

                }
            }
        }



   

    }
}
