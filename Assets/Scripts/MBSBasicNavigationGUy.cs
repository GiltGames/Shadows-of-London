using UnityEngine;
using UnityEngine.AI;

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
    public bool isWaiting;
    [SerializeField] float fltVariationInTarget=3f;
    public bool isWanderingMode;
    [SerializeField] float fltDistancetoTarget;
    [SerializeField] float fltMoveRange = 30f;

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

    }

    // Update is called once per frame
    void Update()
    {
       
        // only use this script to set destinations if wanderingmode is on, which it is by default 
        if (isWanderingMode)
        {
            // if stationary, count before changing waypoiubt
            fltDistancetoTarget = (trnCurrentTarget.position - transform.position).magnitude;

            if ((vecNavTarget - transform.position).magnitude < fltDistancetoTarget)
            {
                fltDistancetoTarget = (vecNavTarget - transform.position).magnitude;
            }


         

          //  if (fltDelayCount > fltDelayLongstop)
            //{
          //      FnWaypointUpdatequery();
           // }


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
                if (fltDistancetoTarget < fltDistance)
                {
                    fltDelayCount += Time.deltaTime;
               
                if (isCriminal)
                {

                    if (mbsCrim.isEscaping)
                    {
                        mbsCrim.FnGotAway();

                    }
                }

                    if (fltDelayCount > fltDelay)
                    {
                        FnWaypointUpdatequery();
                        isWaiting = false;
                    }

                  

                }
            //}


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

        }

    }

    public void FnWaypointUpdate()
    {

       





            int intNewWaypointTmp = Random.Range(0,trnWaypoint.Length);
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

        if (isCriminal)
        {
            FnCriminalMove();
        }


    }

    public void FnCriminalMove()
    {

        fltRandomSelectiontoAdvance = Random.Range(0f, 1.0f);
        
        if (fltRandomSelectiontoAdvance < fltMoveAdvance)
        {

           mbsCrim.FnCriminalMoveUpdate();



           trnCurrentTarget = mbsCrim.trnNewTarget;
            agent.SetDestination(trnCurrentTarget.position);

        }
    }


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


}
