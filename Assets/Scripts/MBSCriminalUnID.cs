using UnityEngine;
using UnityEngine.AI;

public class MBSCriminalUnID : MonoBehaviour
{
    [SerializeField] MBSBasicNavigationGUy mbsNav;
    [SerializeField] MBSArrestGuy mbsArrest;
    [SerializeField] MBSArrestUpdateUI mbsArrestUpdateUI;
    [SerializeField] bool isPretendingToBeCrowd;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] GameObject gmoAura;
    [SerializeField] bool isDetectable;
    [SerializeField] int intHintType;
    public bool isArrested;
    public int intCriminalIndex;
    public bool isInCustody;
    

   



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsNav = GetComponent<MBSBasicNavigationGUy>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
     
        mbsNav.isWanderingMode = true;
        mbsArrest = GetComponent<MBSArrestGuy>();
       
    }

    // Update is called once per frame
    void Update()
    {
       

        if (isDetectable)
        {
            FnClueGive();
        }

    }

    private void OnMouseEnter()
    {
       
        if (Input.GetMouseButtonDown(1))
        {
            FnArrest();
        }
        

        // highlights not needed now
        /*
        if (isDetectable)
        {
            anim.SetTrigger("Seen");
            agent.SetDestination(transform.position);
            mbsNav.fltDelayCount = 0;
            mbsNav.isWaiting = true;

            gmoAura.SetActive(true);
        }
        */


    }

    private void OnMouseExit()
    {
        gmoAura.SetActive(false);
    }

    void FnClueGive()
    {
        // different effect depending on type of clue
        switch (intHintType)
        {
            case 0:


                break;



        }

    }


    void FnArrest()
    {
        //Update the UI
       

        mbsArrestUpdateUI.FnArrestUpdateUI(intCriminalIndex);
        // Call the arrest animation
      

        //Stop moving....
        /*
        mbsNav.isWaiting = true;
        mbsNav.isWanderingMode = false;
        agent.SetDestination(transform.position);
        */
    }

   


    void FnEvade()
    {



    }

}
