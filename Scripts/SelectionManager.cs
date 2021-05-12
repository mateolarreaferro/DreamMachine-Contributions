using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Mateo Larrea
//DREAM MACHINE

//Interactable Detection Script (Currently just for platformer)

public class SelectionManager : MonoBehaviour
{
    [Header("Enter Tag for 'interactables'")]
    [SerializeField] private string selectableTag = "ball"; //add any tag that seems appropiate
    [Header("Materials")]
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;

    private Transform _selection;

    public static string selectedObject;
    [Header("Debuging")]
    public string internalObject; //Debugging

    // Update is called once per frame
    void Update()
    {
        //Deselection/Selection Renderer
        if (_selection != null)
        {
            var selection = _selection;
            OnDiselect(selection);

        }

        //Creating a Ray
        RaycastHit hit;

        //Selection Determination
        _selection = null;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            var selection = hit.transform;

            //Debugging: What Object?
            selectedObject = hit.transform.gameObject.name;
            internalObject = hit.transform.gameObject.name;

            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
            }
        }

        //Deselection/Selection Renderer
        if (_selection != null)
        {
            var selection = _selection;

            OnSelect(selection);

        }



    }

    private void OnSelect(Transform selection)
    {
        var selectionRenderer = _selection.GetComponent<Renderer>();

        if (selectionRenderer != null)
        {
            selectionRenderer.material = highlightMaterial;
        }
    }

    private void OnDiselect(Transform selection)
    {
        var selectionRenderer = _selection.GetComponent<Renderer>();
        if (selectionRenderer != null)
        {
            selectionRenderer.material = defaultMaterial;
        }
    }

}
        



