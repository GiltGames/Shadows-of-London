using UnityEngine;
using UnityEngine.AI;

public class MBSFollowerGuy : MonoBehaviour
{
    // script attached to members of the crowd that follow other crowd members

    // if this is re-written, this script could be combiend into the basic navigation script

    // followers use navmesh but track a target assigned in the inspector



    public NavMeshAgent agent;
    public Animator anim;
    [SerializeField] Transform trnCurrentTarget;
    [SerializeField] float fltDistance;
    public bool isWaiting;
    [SerializeField] MBSBasicNavigationGUy mbsParentNav;
    public bool isWanderingMode;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        mbsParentNav = trnCurrentTarget.GetComponent<MBSBasicNavigationGUy>();






    }

    // Update is called once per frame
    void Update()
    {
        if (isWanderingMode)
        {

            // animation and tracking set by what the parent is doing

            if (mbsParentNav.isWaiting)
            {
                anim.SetBool("Still", true);

                // waits - position is parent plus offest

                agent.SetDestination(trnCurrentTarget.position + trnCurrentTarget.forward * fltDistance);
                transform.LookAt(trnCurrentTarget);
            }
            else
            {
                // aims to walk to the point ahead of the parent

                anim.SetBool("Still", false);
                agent.SetDestination(trnCurrentTarget.position - trnCurrentTarget.forward * fltDistance);
            }

        }
    }
}
