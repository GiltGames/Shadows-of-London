using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class RaycastCube : MonoBehaviour
{
    private Vector3 initialScale;
    //public Material glow;
    private Renderer objectRenderer;
    private Color originalColor;

    Color mouseOverColor = Color.yellow;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
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
        objectRenderer.material.color = Color.yellow;
    }
    private void OnMouseExit()
    {
        IncreaseScale(false);
        objectRenderer.material.color = Color.white;
    }
    private void IncreaseScale(bool status)
    {
        Vector3 finalScale = initialScale;

        if (status) finalScale = initialScale * 1.1f;

        transform.localScale = finalScale;
    }
}