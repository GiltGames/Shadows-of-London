using UnityEngine;
using UnityEngine.AI;

public class MBSFollowerGuy : MonoBehaviour
{

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

            if (mbsParentNav.isWaiting)
            {
                anim.SetBool("Still", true);
                agent.SetDestination(trnCurrentTarget.position + trnCurrentTarget.forward * fltDistance);
                transform.LookAt(trnCurrentTarget);
            }
            else
            {
                anim.SetBool("Still", false);
                agent.SetDestination(trnCurrentTarget.position - trnCurrentTarget.forward * fltDistance);
            }

        }
    }
}
