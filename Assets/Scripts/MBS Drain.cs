using UnityEngine;

public class MBSDrain : MonoBehaviour
{
    [SerializeField] PlayerMovement mbsPlayer;
    [SerializeField] float fltRangetoSee=15f;
    [SerializeField] bool isVisible;
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

        if (isVisible)
        {
            if ((transform.position - mbsPlayer.transform.position).magnitude > fltRangetoSee)
            {
                isVisible = false;
                gmoGhost.SetActive(false);


            }

        }

       else
        {
            if ((transform.position - mbsPlayer.transform.position).magnitude < fltRangetoSee)
            {
                isVisible = true;
                gmoGhost.SetActive(true);

            }


        }


   

    }

   


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            mbsPlayer.FnUpdateStamina(fltStaminaLoss * Time.deltaTime);


        }
    }


}
