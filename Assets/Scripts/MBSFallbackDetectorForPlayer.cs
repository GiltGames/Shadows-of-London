using UnityEngine;

public class MBSFallbackDetectorForPlayer : MonoBehaviour
{
    [SerializeField] MBSArrestGuy mbsArrest;

    // redundant script from when arresting was called by right clicking.
    // this was attached to an extra object around the character intended to be detected even if the player itself was not
    // outdated now the MBSManualArrest logic is used.



    /*   private void OnMouseOver()
     {
       mbsArrest.gmoHighlight.SetActive(true);


         if (Input.GetMouseButtonDown(1) && !mbsArrest.isArrested)
         {

             mbsArrest.FnArrested();
         }
     }

     private void OnMouseExit()
     {
         mbsArrest.gmoHighlight.SetActive(false);
     }

         */
}
