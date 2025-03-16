using UnityEngine;

public class MBSOOB : MonoBehaviour
{
    
    // if a character wander into the OOB trigger, the navdestination is reset
    // doesn't apply to criminals



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<MBSBasicNavigationGUy>() != null)
                {

            if (!other.transform.GetComponent<MBSBasicNavigationGUy>().isCriminal)
            {
                other.transform.GetComponent<MBSBasicNavigationGUy>().FnWaypointUpdate();
            }



        }
    }




}
