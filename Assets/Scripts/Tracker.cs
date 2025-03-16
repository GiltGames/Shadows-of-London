using Unity.VisualScripting;
using UnityEngine;

public class Tracker : MonoBehaviour
{

    // This is to stop the camera through the floor issue

    //This script is attached to an invisible camera point.
    // it follows the player x and z, but holds at a constant y
    // if the player is off normal ground level, it adjusts so the camera point isn't underground
    // if this is ever updated, the height should be determined by raycastng rom the camera point down 
    // and setting the height to be a constant distance above the hit.point - that will ensure
    // a constant height above groun level and remove the current judder ont he camera when the player is on the first 
    // floor of the museum or the raised ground near the docks


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
        //tracked is set in the inspector to be the player

        // sets the target to be the player x/z location plus offset

        trackedLocation = tracked.position + tracked.transform.forward * offset.x + tracked.transform.right * offset.z;
        
        // check to see if the player has fallen into the river - if so, end game with drowned screen
        if (tracked.position.y <-20)
        {
            endStateBehav.ActivateGameDrowned();

        }

        // set target to be constant off ground
        trackedLocation.y = offset.y;

// if the player is raised, then increase the target y

        if (tracked.transform.position.y > trackedLocation.y)
        {
            trackedLocation.y = tracked.transform.position.y    ;

        }


        //rotate the caerma point to match player rotation and position (as adjusted)
        transform.rotation = tracked.transform.rotation;

        transform.position = trackedLocation;



    }
}
