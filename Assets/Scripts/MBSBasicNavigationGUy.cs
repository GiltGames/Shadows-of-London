using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class MBSBasicNavigationGUy : MonoBehaviour
{
    [Header ("Navigation")]
    public Transform[] trnWaypoint;
    [SerializeField] MBSCriminalUnID mbsCrim;
    public NavMeshAgent agent;
    public Animator anim;
    public Transform trnCurrentTarget;
    [SerializeField] float fltDistance;
    [SerializeField] float fltChanceIdle;
    [SerializeField] float fltDelay;
    [SerializeField] float fltDelayLongstop;
    public float fltDelayCount;
    [SerializeField] float fltDelayTimer;
    public bool isWaiting;
    [SerializeField] float fltVariationInTarget=3f;
    public bool isWanderingMode;
    [SerializeField] float fltDistancetoTarget;
    [SerializeField] float fltMoveRange = 30f;

    [Header("Waypoints")]
    [SerializeField] Transform[] trnWayPointinWorld;
    [SerializeField] WaypointIdentifier[] wayPointinWorld;
    [SerializeField]
    float[] fltDistancetoWaypoint;
    [SerializeField] Transform[] trnWayPointinRange;



    [Header ("Crimnal Variables")]
    public bool isCriminal;
    public bool[] isCriminalWaypointReached;
   
    [SerializeField] float fltMoveAdvance =.3f;
    [SerializeField] float fltRandomSelectiontoAdvance;

    public Vector3 vecNavTarget;


    [Header("NeerdoWell")]
    public bool isNeerdoWell;
    
  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        trnCurrentTarget = transform;
        isWanderingMode = true;
        mbsCrim = GetComponent<MBSCriminalUnID>();

        if (GetComponent<MBSCriminalUnID>()  != null )
        {
            isCriminal = true;

        }

        //find identifier for all waypoints
       wayPointinWorld = FindObjectsByType<WaypointIdentifier>(FindObjectsSortMode.None);
        fltDistancetoWaypoint = new float[wayPointinWorld.Length];

        FnSetWayPoint();

        //set five way points into list
        /*
        for (int i = 0; i < trnWaypoint.Length; i++)
        {
            trnWayPointinRange[i] = wayPointinWorld[i].transform;

        }
        */

    }

    // Update is called once per frame
    void Update()
    {
     


        // only use this script to set destinations if wanderingmode is on, which it is by default 
        if (isWanderingMode)
        {
            //distance to target

            fltDistancetoTarget = (trnCurrentTarget.position - transform.position).magnitude;

            if ((vecNavTarget - transform.position).magnitude < fltDistancetoTarget)
            {
                fltDistancetoTarget = (vecNavTarget - transform.position).magnitude;
            }


            if (isCriminal)
            {
                FnCriminalMoveFrame();

            }

            else
            {
                FnNonCriminalMoveFrame();

            }



         //   if (!isCriminal)
       //     {
                
        //    }

         /*   if (isWaiting)
            {
                if (fltDelayCount > fltDelay)
                {
                    FnWaypointUpdatequery();
                    isWaiting = false;
                }

            }
            else
            {
         */
            /*    if (fltDistancetoTarget < fltDistance)
                {
                    fltDelayCount += Time.deltaTime;
               
                if (isCriminal)
                {

                   
                }

                    if (fltDelayCount > fltDelay)
                    {
                        FnWaypointUpdatequery();
                        isWaiting = false;
                    }

                  

                } */
            //}


        }
    }


    void FnNonCriminalMoveFrame()
    {
        if (isWaiting)
        {
            fltDelayTimer += Time.deltaTime;

            if (fltDelayTimer > fltDelayLongstop)
            {
                fltDelayTimer = 0;
                FnWaypointUpdatequery();
                isWaiting = false;
            }
        }

        else
        {
            if (fltDistancetoTarget <fltDistance)
            {

                FnWaypointUpdatequery();
                isWaiting = false;

            }


        }

    }

    void FnCriminalMoveFrame()
    {

   

        if (fltDistancetoTarget < fltDistance)
        {
     Debug.Log(transform.name + " changes waypoint script");
        Debug.Log(transform.name + " old way" + trnCurrentTarget.name);
            if (mbsCrim.isEscaping)
            {
                mbsCrim.FnGotAway();
                gameObject.SetActive(false);
            }

            mbsCrim.FnCriminalMoveUpdate();
            Debug.Log(transform.name + " new way" + trnCurrentTarget.name);
        }
    }




    public void FnWaypointUpdatequery()

    {

        if (Random.Range(0, 1f) < fltChanceIdle)
        {
          
            agent.SetDestination(transform.position);
            trnCurrentTarget = transform;
            fltDelayCount = 0;
            anim.SetBool("Still",true);


        }
        else
        {
            //isWaiting = false;
            FnWaypointUpdate();
            fltDelayCount = 0;
            fltDelayTimer = 0;
        }

    }

    public void FnWaypointUpdate()
    {


        if (!isCriminal)
        {

            int intNewWaypointTmp = Random.Range(0, trnWaypoint.Length);
            trnCurrentTarget = trnWaypoint[intNewWaypointTmp];

            fltDistancetoTarget = (trnCurrentTarget.position - transform.position).magnitude;

            // if its not close enough, wander about a bit
            if (fltDistancetoTarget > fltMoveRange)
            {
                trnCurrentTarget = transform;
            }

            // set waypoint with variation

            Vector3 fltOffsetTmp = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * fltVariationInTarget;

            agent.SetDestination(trnCurrentTarget.position + fltOffsetTmp);
            anim.SetBool("Still", false);

            // possible override to location if character is a criminal
        }
        else
        {
            // FnCriminalMove();

            mbsCrim.FnCriminalMoveUpdate();
        }


    }

 //  public void FnCriminalMove()
  //  {

     //   fltRandomSelectiontoAdvance = Random.Range(0f, 1.0f);
        
    //    if (fltRandomSelectiontoAdvance < fltMoveAdvance)
     //   {

         //  mbsCrim.FnCriminalMoveUpdate();



      //     trnCurrentTarget = mbsCrim.trnNewTarget;
     //       agent.SetDestination(trnCurrentTarget.position);

       // }
  //  }


    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.tag == "OOB")

            {
               
                
                FnWaypointUpdate();
            }

        }


    }


    public void FnSetWayPoint()
    {
        // sets the five closest waypoints into the script for navigation 
       for (int i = 0;i<wayPointinWorld.Length; i++)
        {
            fltDistancetoWaypoint[i] = (wayPointinWorld[i].transform.position - transform.position).magnitude;

        }

        bool isNotSorted = true;
       while (isNotSorted)
        {
            isNotSorted = false;
            for (int i = 0; i<wayPointinWorld.Length-1;i++)

            {
                if (fltDistancetoWaypoint[i] > fltDistancetoWaypoint[i + 1])
                        {
                   float swapTmp = fltDistancetoWaypoint[i]; 
                    fltDistancetoWaypoint[i] = fltDistancetoWaypoint[i+1];
                    fltDistancetoWaypoint[i+1] = swapTmp;
                    isNotSorted = true;

                    WaypointIdentifier swapTmp2 = wayPointinWorld[i];
                    wayPointinWorld[i] = wayPointinWorld[i+1];
                    wayPointinWorld[i+1] = swapTmp2;    


                }

            }
            Debug.Log(transform.name + " is being sorted");
        }

        Debug.Log(transform.name + " is NOW sorted");




        for (int i = 0; i < trnWaypoint.Length; i++)
        {
            trnWaypoint[i] = wayPointinWorld[i].transform;

        }



    }


  
}
