using UnityEngine;
using UnityEngine.UIElements;

public class RaycastPersonGuy : MonoBehaviour
{
    [SerializeField] Vector3 initialScale;
    private Renderer objectRenderer;
    private Color originalColor;
    [SerializeField] GameObject renderTarget;
    Color mouseOverColor = Color.yellow;
    [SerializeField] float fltScaling =1;

    private void Start()
    {
        objectRenderer = renderTarget.GetComponent<Renderer>();
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
        //IncreaseScale(true);
        objectRenderer.gameObject.SetActive(true);
        objectRenderer.material.color = Color.yellow;
    }
    private void OnMouseExit()
    {
        //IncreaseScale(false);
        objectRenderer.gameObject.SetActive(false);
        objectRenderer.material.color = originalColor;
    }
    private void IncreaseScale(bool status)
    {
        Vector3 finalScale = initialScale;

        if (status) finalScale = initialScale * fltScaling;

        transform.localScale = finalScale;
    }
}
