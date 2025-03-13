using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] Transform tracked;
    [SerializeField] Vector3 trackedLocation;
    [SerializeField] Vector3 offset;
    [SerializeField] endStateBehav endStateBehav;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        endStateBehav = FindFirstObjectByType<endStateBehav>();

    }

    // Update is called once per frame
    void Update()
    {
        trackedLocation = tracked.position + tracked.transform.forward * offset.x + tracked.transform.right * offset.z;
        if (tracked.position.y <-40)
        {
            endStateBehav.ActivateGameDrowned();

        }


        trackedLocation.y = offset.y;

        if (tracked.transform.position.y > trackedLocation.y)
        {
            trackedLocation.y = tracked.transform.position.y    ;

        }



        transform.rotation = tracked.transform.rotation;





        transform.position = trackedLocation;



    }
}
