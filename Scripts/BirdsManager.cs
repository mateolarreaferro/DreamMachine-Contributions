using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Dream Machine
//Mateo Larrea 2021

//BirdsManager is in charge of creating the birds and sending information to IndividualBird.cs

public class BirdsManager : MonoBehaviour
{
    //Variables
    [Header("Bird Generator")]
    [Tooltip("Press in order to position x birds")]
    public bool CreateBirds; //On switch

    [Tooltip("Automatically create the birds on Awake")]
    public bool CreateOnAwake = false;
    
    //Birds Generator
    [Header("How many birds?")]
    [Tooltip("Select # of birds you need")]
    public int numberOfBirds;
    [Tooltip("Bird that will be copied")]
    public GameObject originalBird;

    //Audio Configurations
    [Header("Clips")]
    [Tooltip("insert birds audio clips")]
    public AudioClip[] birdsSounds;

    ////Positions
    [Header("Positioning")]
    [Tooltip("How far?")]
    public int distanceRange; //Range in space where birds can sing
    [Tooltip("How high?")]
    public int maxHeight; //Maximum height from ground

    private void Awake()
    {
        if (CreateOnAwake)
        {
            CreateBirds = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        BirthoftheBirds();
    }

    void BirthoftheBirds()
    {
        if (CreateBirds == true)
        {

            for (int a = 0; a < numberOfBirds; a++)
            {
                Vector3 birdPosition = new Vector3(Random.Range(-distanceRange, distanceRange),
                    Random.Range(1, maxHeight), Random.Range(-distanceRange, distanceRange));

                GameObject birdCopy = Instantiate(originalBird, birdPosition, this.transform.rotation, parent: transform);
                birdCopy.name = "Bird # " + a;

                int randomClip = Random.Range(0, birdsSounds.Length);
                birdCopy.GetComponent<AudioSource>().clip = birdsSounds[randomClip];
                


            }

            CreateBirds = false;
        }
    }
}
