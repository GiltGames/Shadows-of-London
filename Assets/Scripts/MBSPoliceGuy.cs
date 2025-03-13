using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MBSPoliceGuy : MonoBehaviour
{

    public bool isArresting;
    public Transform trnPersonArrested;
    public bool isHasSomeoneInCustody;
    [SerializeField] float fltArrestDistance;
    [SerializeField] MBSBasicNavigationGUy mbsNav;
    [SerializeField] MBSFollowerGuy mbsNavFollow;
    [SerializeField] Transform trnCustodyLocation;
    [SerializeField] float fltRunSpeed;
    [SerializeField] float fltWalkSpeed;
    [SerializeField] TMP_Text txtSpeech;
    [SerializeField] float fltSpeechTime = 1.5f;
    [SerializeField] GameObject gmoSpeech;
    [SerializeField] AudioSource audSource;
    [SerializeField] AudioClip audBangtoRights;
    [SerializeField] AudioClip audOntheCase;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsNav = GetComponent<MBSBasicNavigationGUy>();
        mbsNavFollow = GetComponent<MBSFollowerGuy>();
        audSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
    if (isArresting)
        {

            Vector3 vecArrest = trnPersonArrested.position;
            
            gmoSpeech.SetActive(true);
            txtSpeech.text = "On the case, ma'am";
            StartCoroutine(IEPSpeechOff());


            if (mbsNav != null)
            {

                mbsNav.agent.SetDestination(vecArrest);
                mbsNav.agent.speed = fltRunSpeed;
            }

            if (mbsNavFollow!= null)
            {
                mbsNavFollow.agent.SetDestination(vecArrest);
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
        gmoSpeech.SetActive(true);
        txtSpeech.text = "Got you bang to rights";
        StartCoroutine(IEPSpeechOff());
        

        Vector3 vecCustody = trnCustodyLocation.position;

        // ACTIONS ON THE POLICEMAN 

        if (mbsNav != null)
        {
            mbsNav.anim.SetBool("Run", false);
            mbsNav.agent.SetDestination(vecCustody);
            mbsNav.anim.SetBool("Still", false);
            mbsNav.isWaiting = false;
            mbsNav.isWanderingMode = false;
            mbsNav.agent.speed = fltWalkSpeed;
        }

        if (mbsNavFollow != null)
        {
            mbsNavFollow.anim.SetBool("Run", false);
           
            mbsNavFollow.agent.SetDestination(vecCustody);
            mbsNavFollow.anim.SetBool("Still", false);
            mbsNavFollow.isWaiting = false;
            mbsNavFollow.isWanderingMode = false;
            mbsNavFollow.agent.speed = fltWalkSpeed;
        }
        
        
        trnPersonArrested.GetComponent<MBSArrestGuy>().FnInCustody();


       
        

    }

    
    IEnumerator IEPSpeechOff()
    {


        yield return new WaitForSeconds(fltSpeechTime);

        gmoSpeech.SetActive(false);

    }

}
