using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MBSArrestGuy : MonoBehaviour
{
    [SerializeField] MBSPoliceGuy[] mbsPoliceGuy;
    [SerializeField] float fltDistance;
    [SerializeField] Transform trnClosestPolice;
    [SerializeField] MBSPoliceGuy mbsClosestPolice;
    [SerializeField] MBSBasicNavigationGUy mbsClosePoliceNav;
    [SerializeField] MBSFollowerGuy mbsClosePoliceNavFollow;
   [SerializeField] MBSBasicNavigationGUy mbsNav;
    [SerializeField] MBSFollowerGuy mbsFollower;
    [SerializeField] Transform trnCustodyLocation;
  
    [SerializeField] bool isArrested;
    [SerializeField] TMP_Text txtSpeech;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        mbsPoliceGuy = Object.FindObjectsByType<MBSPoliceGuy>(FindObjectsSortMode.None);    
        mbsNav = GetComponent<MBSBasicNavigationGUy>();

        if (mbsNav == null)
        {
            mbsFollower = GetComponent<MBSFollowerGuy>();


        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {



        if (Input.GetMouseButtonDown(1) && !isArrested)
        {

            FnArrested();
        }
    }

    public void FnArrested()
    {
        fltDistance = 1000;

        foreach (MBSPoliceGuy policeman in mbsPoliceGuy)
        {
            bool isArrestingTmp = policeman.GetComponent<MBSPoliceGuy>().isArresting;

            Vector3 vectortmp = (policeman.transform.position- transform.position);
            if ( vectortmp.magnitude < fltDistance && !isArrestingTmp)
            {
                trnClosestPolice = policeman.transform;

                fltDistance = vectortmp.magnitude;

            }
            
            


        }

        mbsClosestPolice = trnClosestPolice.GetComponent<MBSPoliceGuy>();
        
        
        mbsClosePoliceNav = trnClosestPolice.GetComponent <MBSBasicNavigationGUy>();
        mbsClosePoliceNavFollow = trnClosestPolice.GetComponent<MBSFollowerGuy>();
        if (mbsClosePoliceNav != null)
        {
            mbsClosePoliceNav.isWanderingMode = false;
            mbsClosePoliceNav.anim.SetBool("Still", false);
            mbsClosePoliceNav.anim.SetBool("Run", true);
           
        }

        if (mbsClosePoliceNavFollow != null)
        {
            mbsClosePoliceNavFollow.isWanderingMode = false;
            mbsClosePoliceNavFollow.anim.SetBool("Still", false);
            mbsClosePoliceNavFollow.anim.SetBool("Run", true);
          
        }

        mbsClosestPolice.isArresting = true;
        mbsClosestPolice.trnPersonArrested = transform;

        txtSpeech.text = "Arrested by "+ trnClosestPolice.name;

        isArrested = true;


    }

    public void FnInCustody()
    {
        
        
      

        if (mbsNav != null)
        {
            mbsNav.agent.SetDestination(trnCustodyLocation.position);
            mbsNav.anim.SetBool("Still", false);

            mbsNav.isWaiting = true;
            mbsNav.isWanderingMode = false;
            
        }

        if (mbsFollower != null)
        {
            mbsFollower.agent.SetDestination(trnCustodyLocation.position);
            mbsFollower.anim.SetBool("Still", false);
            mbsFollower.isWanderingMode = false;
            mbsFollower.isWaiting = false;
           

        }

       
    }


}
