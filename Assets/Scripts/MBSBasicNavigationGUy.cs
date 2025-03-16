using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;

public class MBSBasicNavigationGUy : MonoBehaviour
{
    // This script is attached to most of the crowd members and all the criminals.
    // it controls movement between waypoints - in the case of criminals the target is set in the MBSCriminalUnID scripts but the checks on reaching destinations are here


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
        
        
        //set five way points into list

        FnSetWayPoint();


    }

    // Update is called once per frame
    void Update()
    {
     


        // only use this script to set destinations if wanderingmode is on, which it is by default 
        // wanderingmode is turned off when the character is taken into custody

        if (isWanderingMode)
        {
            //distance to target

            fltDistancetoTarget = (trnCurrentTarget.position - transform.position).magnitude;

            // the actual navigation point is a random point close to the waypoint, set each time. This is checked too and if the character is closer to that than the waypoint, that distance is used

            if ((vecNavTarget - transform.position).magnitude < fltDistancetoTarget)
            {
                fltDistancetoTarget = (vecNavTarget - transform.position).magnitude;
            }

            // movement instructions if criminal
            if (isCriminal)
            {
                FnCriminalMoveFrame();

            }


            //movement instructon if not criminal
            else
            {
                FnNonCriminalMoveFrame();

            }


        }
    }


    void FnNonCriminalMoveFrame()
    {

        // if the character is waiting to move, check the timer and when it gets high enough, check to move againg

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

        // if character is moving, the desinaation update happens when it is close enough to the destination

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

        // if the criminal is close enough  call the update function in the MBSCriminalUnID script.

        

   

        if (fltDistancetoTarget < fltDistance)
        {
     Debug.Log(transform.name + " changes waypoint script");
        Debug.Log(transform.name + " old way" + trnCurrentTarget.name);

            // redundant check for the last waypoint - now done in MBSCriminalUnID script

            if (mbsCrim.isEscaping)
            {
                mbsCrim.FnGotAway();
                gameObject.SetActive(false);
            }
//set agent to go to self before rest
            agent.SetDestination(transform.position);

            //calls MBSCriminalUnID for update function

            mbsCrim.FnCriminalMoveUpdate();
            Debug.Log(transform.name + " new way" + trnCurrentTarget.name);
        }
    }




    public void FnWaypointUpdatequery()

    {

        // checks to see if the character waits or moves to a new waypoint

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

        //selects a new waypoint from one of the 5 available - the cloeses 5


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

            
        }
       

    }




    private void OnTriggerEnter(Collider other)
    {

        // there are some out of bounds triggers - if the character hits one, then it selects a new waypoint to move to 

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

        // this is only run once for crowd members 

        // it was also used at one point to reset random waypoitns for criinals but that is no longer needed as they follow a strict list now.



        // reads all waypoints and puts the distances in an array

       for (int i = 0;i<wayPointinWorld.Length; i++)
        {
            fltDistancetoWaypoint[i] = (wayPointinWorld[i].transform.position - transform.position).magnitude;

        }

       //runs through the array. 
       // if at any stage the distance to a waypoint is greater than that to the next waypoint, then the two are swapped (as are their distances)
       // and the isNotSorted bool is set to true.
       // this is re-run until isNotSorted remains false which means they are all in the right order

        bool isNotSorted = true;
       while (isNotSorted)
        {
            isNotSorted = false;
            for (int i = 0; i<wayPointinWorld.Length-1;i++)

            {
                if (fltDistancetoWaypoint[i] > fltDistancetoWaypoint[i + 1])
                        {
                    // swap

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



        // puts the closest 5 into an array for later use
        for (int i = 0; i < trnWaypoint.Length; i++)
        {
            trnWaypoint[i] = wayPointinWorld[i].transform;

        }



    }


  
}
