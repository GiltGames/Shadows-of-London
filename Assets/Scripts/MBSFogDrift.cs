using UnityEngine;

public class MBSFogDrift : MonoBehaviour
{
    public Vector3 vecFogDrift;
    public float vecFogExpand=1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(vecFogDrift * Time.deltaTime);
        transform.localScale *= vecFogExpand * Time.deltaTime;

    }
}
