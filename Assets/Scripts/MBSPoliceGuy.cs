using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MBSPoliceGuy : MonoBehaviour
{

    public bool isArresting;
    public Transform trnPersonArrested;
    [SerializeField] bool isHasSomeoneInCustody;
    [SerializeField] float fltArrestDistance;
    [SerializeField] MBSBasicNavigationGUy mbsNav;
    [SerializeField] MBSFollowerGuy mbsNavFollow;
    [SerializeField] Transform trnCustodyLocation;
    [SerializeField] float fltRunSpeed;
    [SerializeField] float fltWalkSpeed;
    [SerializeField] TMP_Text txtSpeech;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsNav = GetComponent<MBSBasicNavigationGUy>();
        mbsNavFollow = GetComponent<MBSFollowerGuy>();
        
    }

    // Update is called once per frame
    void Update()
    {
    if (isArresting)
        {

            txtSpeech.text = "On the case, ma'am";
            if (mbsNav != null)
            {

                mbsNav.agent.SetDestination(trnPersonArrested.position);
                mbsNav.agent.speed = fltRunSpeed;
            }

            if (mbsNavFollow!= null)
            {
                mbsNavFollow.agent.SetDestination(trnPersonArrested.position);
                mbsNavFollow.agent.speed = fltRunSpeed;

            }



            float fltDistanceTmp = (trnPersonArrested.position - transform.position).magnitude;

            if (fltDistanceTmp < fltArrestDistance)
            {
                FnTakeintoCustody();

            }


        }
        

    }


    void FnTakeintoCustody()
    {
        isArresting = false;
        isHasSomeoneInCustody = true;
        txtSpeech.text = "Got you bang to rights";

        // ACTIONS ON THE POLICEMAN 

        if (mbsNav != null)
        {
            mbsNav.anim.SetBool("Run", false);
            mbsNav.agent.SetDestination(trnCustodyLocation.position);
            mbsNav.anim.SetBool("Still", false);
            mbsNav.isWaiting = false;
            mbsNav.isWanderingMode = false;
            mbsNav.agent.speed = fltWalkSpeed;
        }

        if (mbsNavFollow != null)
        {
            mbsNavFollow.anim.SetBool("Run", false);
           
            mbsNavFollow.agent.SetDestination(trnCustodyLocation.position);
            mbsNavFollow.anim.SetBool("Still", false);
            mbsNavFollow.isWaiting = false;
            mbsNavFollow.isWanderingMode = false;
            mbsNavFollow.agent.speed = fltWalkSpeed;
        }
        
        
        trnPersonArrested.GetComponent<MBSArrestGuy>().FnInCustody();


       
        

    }

}
