//Mateo Larrea - Dream Machine '21
using System.Collections;
using UnityEngine;

//Attach this script to Time Keeper character
//Use methods starting on line 118 to go into specific states of the ticking (can be called from timeline)

 [RequireComponent(typeof(AudioSource))]

 public class TimeKeeperTickTockManager : MonoBehaviour {

    //Initializers
     [Header("Metronome Set Up")]
     public bool NewMetronome; //It's only pressed once
     public AudioSource _Emitter; //Audio Source

     [Header("Audio Files")]
     public AudioClip _upBeat;
     public AudioClip _downBeat;

     [Header("Speed/Interval")]
     [Range(1.5f, 0.05f)]
     public double Interval; //The interval between the ticks
     double BPM = 120;

     [Header("Beats/Count")]
     public int _counter;
     int _setTheTime = 1000;
     public int NumberOfBeatsInBar = 4;

    [Header("States")]
    public bool Slow, Normal, Fast;
    public float[] Values;
    public int[] Seconds;




    public void Awake()
     {
        
         Interval = 60.0f / BPM; //Seconds/beats (in minute)

         NewMetronome = false;

         Normal = true;

     }

        //NEW METRONOME
     public void Update()
     {
         if (NewMetronome)
         {
             StartCoroutine(secondRoutine()); 
             NewMetronome = false;
         }

        //STATES LOGIC
        if (Slow == true)
        {
            Interval = 1.3f;
            Slow = false;
           
        }
        if (Normal == true) 
        {
            Interval = 0.6f;
            Normal = false;
        }
        if (Fast == true)
        {
            StartCoroutine(IncreasePace());
            Fast = false;
            
        }


    }
     IEnumerator secondRoutine()
     {
       
         while (Time.time < _setTheTime) 
         {
             _counter++;
             if (_counter % NumberOfBeatsInBar == 1) { 

                 _Emitter.clip = _upBeat; //Up Beat
                 _Emitter.Play();
             
             }
             else
             {
                 _Emitter.clip = _downBeat; //Down Beat
                 _Emitter.Play();
             
             }

             yield return new WaitForSecondsRealtime((float)Interval); 
         }
     }
    IEnumerator IncreasePace()
    {
        for (int a = 0; a < Values.Length; a++)
        {
            Interval = Values[a];

            yield return new WaitForSeconds(Seconds[a]);
        }
    }


    //METHODS that will set the tempo and can be accessed from timeline

    public void IncreaseTickTockRate() //FAST
    {
        StartCoroutine(IncreasePace());
    }

    public void SlowTickTockRate() //SLOW
    {
        Interval = 1.3f;
    }

    public void MediumTickTockRate() //MEDIUM
    {
        Interval = 0.5f;
    }

    public void FastTickTockRate()
    {
        Interval = 0.125f;
    }

    public void StartTickTock()
    {
        Interval = 1f;
        StartCoroutine(secondRoutine());
    }
    
    public void StopTickTock()
    {
        StopAllCoroutines();
    }
 }


