using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] Transform tracked;
    [SerializeField] Vector3 trackedLocation;
    [SerializeField] Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        trackedLocation = tracked.position + tracked.transform.forward * offset.x + tracked.transform.right * offset.z;

        trackedLocation.y = offset.y;

        if (tracked.transform.position.y > trackedLocation.y)
        {
            trackedLocation.y = tracked.transform.position.y    ;

        }



        transform.rotation = tracked.transform.rotation;





        transform.position = trackedLocation;



    }
}
