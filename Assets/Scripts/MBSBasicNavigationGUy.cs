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
    [SerializeField] float fltDelayCount;
    public bool isWaiting;
    
  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        trnCurrentTarget = transform;
      
    }

    // Update is called once per frame
    void Update()
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

        agent.SetDestination(trnCurrentTarget.position);
        anim.SetBool("Still", false);




    }

}
