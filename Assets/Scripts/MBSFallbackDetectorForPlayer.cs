using UnityEngine;

public class MBSFallbackDetectorForPlayer : MonoBehaviour
{
    [SerializeField] MBSArrestGuy mbsArrest;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    private void OnMouseOver()
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


}
