using UnityEngine;

public class MBSDrain : MonoBehaviour
{
    [SerializeField] PlayerMovement mbsPlayer;
    [SerializeField] float fltRangetoSee=15f;
    [SerializeField] bool isVisible;
    [SerializeField] float fltRangeHurt =7f;
    [SerializeField] float fltDistance;
    [SerializeField] float fltStaminaLoss= 10f;
    [SerializeField] GameObject gmoGhost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsPlayer = FindFirstObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        fltDistance = (transform.position - mbsPlayer.transform.position).magnitude;

      /*  if (isVisible)
        {
            if (fltDistance > fltRangetoSee)
            {
                isVisible = false;
                gmoGhost.SetActive(false);


            }

        }

       else
        {
            if (fltDistance < fltRangetoSee)
            {
                isVisible = true;
                gmoGhost.SetActive(true);

            }


        }
      */

        if (fltDistance < fltRangeHurt)
        {
            mbsPlayer.FnUpdateStamina(-fltStaminaLoss * Time.deltaTime);
            Debug.Log("Stamina Drain" + fltStaminaLoss * Time.deltaTime);
        }
   

    }

   


  

}
