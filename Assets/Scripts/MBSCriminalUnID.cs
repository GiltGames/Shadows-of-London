using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.AI;

public class MBSCriminalUnID : MonoBehaviour
{
    [SerializeField] MBSBasicNavigationGUy mbsNav;
    [SerializeField] AddToInventory mbsInventory;
    [SerializeField] MBSArrestGuy mbsArrest;
    [SerializeField] MBSArrestUpdateUI mbsArrestUpdateUI;
    [SerializeField] bool isPretendingToBeCrowd;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] GameObject gmoAura;
    [SerializeField] bool isDetectable;
    [SerializeField] int intHintType;
    public bool isArrested;

    public bool isInCustody;
    public bool isTryingtoMakeProgress;

    [SerializeField] Transform[] trnWayPointinWorld;
    [SerializeField] Transform[] trnWayPointinRange;


    [Header("Criminal Move")]
    public Transform trnNewTarget;
    [SerializeField] Transform[] trnWayPointsCriminal;
    public float[] fltTimetoMovetoCriminalWaypoint;
    public int intCriminalProgress;
    [SerializeField] Timer mbsTimer;
    public bool isEscaping;
    public float fltRuntoBoatMod;
    [SerializeField] float fltVariationInTarget = 2;

    [Header("Clue Related")]
    public int intCriminalIndex;
    public GameObject[] gmoHint;
    public int[] intClue;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsNav = GetComponent<MBSBasicNavigationGUy>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        mbsNav.isWanderingMode = true;
        mbsArrest = GetComponent<MBSArrestGuy>();

        mbsTimer = FindFirstObjectByType<Timer>();
/*
        for (int i = 0; i < mbsNav.trnWaypoint.Length; i++)
        {
            trnWayPointinRange[i] = mbsNav.trnWaypoint[i];

        }
*/

        // intClue[i] = i by default but we set up so it can be randmoised
        for (int i = 0; i < intClue.Length; i++)
        {
            intClue[i] = i;
        }





    }

    // Update is called once per frame
    void Update()
    {
        if (!isDetectable)
        {
            FnCheckIfClueFound();
            
        }




    }

    private void OnMouseStay()
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


    public void FnArrest()
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


    public void FnCriminalMoveUpdate()
    {

        intCriminalProgress++;

        if (intCriminalProgress > trnWayPointsCriminal.Length-1)
        {


            //Gets away
            // FnEscape();
            FnGotAway();

        }

        else { 

        trnNewTarget = trnWayPointsCriminal[intCriminalProgress];
        Vector3 fltOffsetTmp = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * fltVariationInTarget;

        agent.SetDestination(trnNewTarget.position + fltOffsetTmp);
        mbsNav.trnCurrentTarget = trnNewTarget;
        mbsNav.vecNavTarget = trnNewTarget.position + fltOffsetTmp;
        

        anim.SetBool("Still", false);
       

        /*for (int i = 0; i < trnWayPointsCriminal.Length; i++)
        {
            if (mbsTimer.timeLeft < fltTimetoMovetoCriminalWaypoint[i])
            {
                trnNewTarget = trnWayPointsCriminal[intCriminalProgress];
                intCriminalProgress = i;

            }


        }


        mbsNav.FnSetWayPoint();
        */

        


        }

      //  isTryingtoMakeProgress = true;
       // trnNewTarget = trnWayPointsCriminal[intCriminalProgress];






    }


    void FnEscape()
    {
        isEscaping = true;
        


    }

    public void FnGotAway()
    {
      mbsInventory.GotAway(intCriminalIndex);
        
        
        mbsNav.isCriminal = false;
        agent.enabled = false;
        gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        /*    No longer used
            if (other.tag == "CriminalWaypoint")
            {
              int indexTmp = other.GetComponent<MBSCriminalWaypoint>().intWaypointIndex;

                if (indexTmp == intCriminalProgress)
                {
                    intCriminalProgress++;
                }

            }
        */
    }

void    FnCheckIfClueFound()
    {
        for (int i = 0; i < intClue.Length; i++)
        {
            if (mbsInventory.enemyOrder[i] == intCriminalIndex)
            {
                FnSwitchOnHint(intCriminalIndex);
                isDetectable = true;
            }
        }
    }



    public void FnSwitchOnHint(int intHintTmp)
    {
        // intClue[i] = i by default but we set up so it can be randmoised
        gmoHint[intClue[intHintTmp]].SetActive(true);

    }

}
