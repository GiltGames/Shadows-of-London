using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RaycastCube : MonoBehaviour
{
    public Material bloodYellow;
    public Material originalMaterial;

    private Vector3 initialScale;
    private Renderer objectRenderer;
    private Color originalColor;
    [SerializeField] GameObject renderTarget;
    Color mouseOverColor = Color.yellow;
    

    public bool evidence1, evidence2, evidence3, evidence4, evidence5, evidence6;

    public int evidenceValue;

    private void Start()
    {
        objectRenderer = renderTarget.GetComponent<Renderer>();

        {
            if (evidence1 is true)
            {
                evidenceValue = 1;
            }

            if (evidence2 is true)
            {
                evidenceValue = 2;
            }

            if (evidence3 is true)
            {
                evidenceValue = 3;
            }

            if (evidence3 is true)
            {
                evidenceValue = 3;
            }

            if (evidence4 is true)
            {
                evidenceValue = 4;
            }

            if (evidence5 is true)
            {
                evidenceValue = 5;
            }

            if (evidence6 is true)
            {
                evidenceValue = 6;
            }
        }
    }
    public void Update()
    {
        
    }
    private void Awake()
    {
        initialScale = transform.localScale;
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }
    private void OnMouseEnter()
    {
        IncreaseScale(true);
        objectRenderer.material = bloodYellow;
        Debug.Log("Hover enter.");
    }
    private void OnMouseExit()
    {
        IncreaseScale(false);
        objectRenderer.material = originalMaterial;
        Debug.Log("Hover exit.");
    }
    private void IncreaseScale(bool status)
    {
        Vector3 finalScale = initialScale;

        if (status) finalScale = initialScale * 1.1f;

        transform.localScale = finalScale;
    }
}