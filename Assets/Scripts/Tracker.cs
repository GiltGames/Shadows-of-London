using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] Transform tracked;
    [SerializeField] Vector3 trackedloction;
    [SerializeField] Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = new Vector3(0,1,0);    
    }

    // Update is called once per frame
    void Update()
    {
        trackedloction = tracked.position;
        


    }
}
