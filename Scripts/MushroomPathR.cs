using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mateo Larrea
//Dream Machine: Path Revealer


public class MushroomPathR : MonoBehaviour
{
    //VARIABLES

    // Time it takes to get to the desired scale (in seconds)
    [SerializeField] float scalingDuration = 8f;

    // Target scale
    [SerializeField] Vector3 targetScale = Vector3.one * .1f;

    // Starting scale
    Vector3 startScale;

    // Linear interpolation.
    float interpolant = 0;

    //Mushroom Objects
    [SerializeField] GameObject[] Mushrooms1, Mushrooms2, Mushrooms3, Mushrooms4, Mushrooms5; //Different parts of the path

    List<GameObject[]> arrayList = new List<GameObject[]>(); //Stores all of the arrays

    //[SerializeField] bool Scaling; //When it should start


    void Start()
    {
        //Add all of the arrays to the list

        arrayList.Add(Mushrooms1);
        arrayList.Add(Mushrooms2);
        arrayList.Add(Mushrooms3);
        arrayList.Add(Mushrooms4);
        arrayList.Add(Mushrooms5);


        startScale = Mushrooms1[0].transform.localScale; //might have to correct if the differences are too big

        interpolant = 0;
    }

    public void Shrink()
    {
        StartCoroutine(ProcessShrinkingSequence());
    }

    IEnumerator ScaleDown(int count)
    {

        interpolant = 0;
        //time it takes from 0 - 1
        interpolant += Time.deltaTime / scalingDuration;



        // Lerp from startScale to targetScale (interpolant -> 0-1)
        Vector3 newScale = Vector3.Lerp(startScale, targetScale, interpolant);

        float elapsedTime = 0;

        while (elapsedTime < scalingDuration)
        {

            for (int i = 0; i < arrayList[count].Length; i++)
            {
                arrayList[count][i].transform.localScale = newScale;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }



    }

    IEnumerator ProcessShrinkingSequence()
    {
        foreach (var mushroomGroup in arrayList)
        {
            int i = 0;
            StartCoroutine(ScaleDown(i));
            i++;
            yield return new WaitForSeconds(Random.Range(4, 7));
        }
    }
}