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
    public bool isTryingtoMakeProgress;

    [SerializeField] Transform[] trnWayPointinWorld;
    [SerializeField]
    Transform[] trnWayPointinRange;


    [Header ("Criminal Move")]
    public Transform trnNewTarget;
    [SerializeField] Transform[] trnWayPointsCriminal;
    public float[] fltTimetoMovetoCriminalWaypoint;
    public int intCriminalProgress;
    [SerializeField] Timer mbsTimer;
    public bool isEscaping;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsNav = GetComponent<MBSBasicNavigationGUy>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
     
        mbsNav.isWanderingMode = true;
        mbsArrest = GetComponent<MBSArrestGuy>();

        mbsTimer = FindFirstObjectByType<Timer>(); 

        for (int i = 0; i<mbsNav.trnWaypoint.Length; i++)
        {
            trnWayPointinRange[i] = mbsNav.trnWaypoint[i];

        }
       

       
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


    public void FnCriminalMoveUpdate()
    {
        



        for (int i = 0; i < trnWayPointsCriminal.Length; i++)
        {
            if (mbsTimer.timeLeft < fltTimetoMovetoCriminalWaypoint[i])
            {
                trnNewTarget = trnWayPointsCriminal[intCriminalProgress];
                intCriminalProgress = i;

            }

            
        }


        foreach (Transform possibleWayPoint in trnWayPointinWorld)
        {
            float dist = (possibleWayPoint.position - transform.position).magnitude;

            if (dist < (trnWayPointinRange[0].position - transform.position).magnitude)
            {
                trnWayPointinRange[0] = possibleWayPoint;

            }

            else if (dist < (trnWayPointinRange[1].position - transform.position).magnitude)
            {
                trnWayPointinRange[1] = possibleWayPoint;

            }

            else if (dist < (trnWayPointinRange[2].position - transform.position).magnitude)
            {
                trnWayPointinRange[2] = possibleWayPoint;

            }
            else if (dist < (trnWayPointinRange[3].position - transform.position).magnitude)
            {
                trnWayPointinRange[3] = possibleWayPoint;

            }

            else if (dist < (trnWayPointinRange[4].position - transform.position).magnitude)
            {
                trnWayPointinRange[4] = possibleWayPoint;

            }


        }

        for (int i = 0; i < trnWayPointinRange.Length; i++)
        {
            mbsNav.trnWaypoint[i] = trnWayPointinRange[i];

        }

        if (intCriminalProgress == trnWayPointsCriminal.Length-1)
        {


            //Gets away
            FnEscape();
        }

        isTryingtoMakeProgress = true;
        trnNewTarget = trnWayPointsCriminal[intCriminalProgress];



        
        
        
    }


    void FnEscape()
    {
        isEscaping = true;


    }

    public void FnGotAway()
    {
        Destroy(gameObject);

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


}
