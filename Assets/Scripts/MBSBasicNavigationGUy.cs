using UnityEngine;
using UnityEngine.AI;

public class MBSBasicNavigationGUy : MonoBehaviour
{
    [Header ("Navigation")]
    [SerializeField] Transform[] trnWaypoint;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] Transform trnCurrentTarget;
    [SerializeField] float fltDistance;
    [SerializeField] float fltChanceIdle;
    [SerializeField] float fltDelay;
    public float fltDelayCount;
    public bool isWaiting;
    [SerializeField] float fltVariationInTarget=3f;
    public bool isWanderingMode;
    
  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        trnCurrentTarget = transform;
        isWanderingMode = true;

    }

    // Update is called once per frame
    void Update()
    {
       
        // only use this script to set destinations if wanderingmode is on, which it is by default 
        if (isWanderingMode)
        {
            // if stationary, count before changing waypoiubt


            fltDelayCount += Time.deltaTime;


            if (isWaiting)
            {
                if (fltDelayCount > fltDelay)
                {
                    FnWaypointUpdatequery();
                }

            }
            else
            {
                if ((trnCurrentTarget.position - transform.position).magnitude < fltDistance)
                {
                    FnWaypointUpdatequery();

                }
            }


        }
    }

    
    public void FnWaypointUpdatequery()

    {

        if (Random.Range(0, 1f) < fltChanceIdle)
        {
            isWaiting = true;
            agent.SetDestination(transform.position);
            fltDelayCount = 0;
            anim.SetBool("Still",true);
        }
        else
        {
            isWaiting = false;
            FnWaypointUpdate();

        }

    }

    public void FnWaypointUpdate()
    {
        
        
        int intNewWaypointTmp = Random.Range(0,trnWaypoint.Length); 

        trnCurrentTarget = trnWaypoint[intNewWaypointTmp];

        Vector3 fltOffsetTmp = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * fltVariationInTarget;

        agent.SetDestination(trnCurrentTarget.position + fltOffsetTmp);
        anim.SetBool("Still", false);




    }

}
