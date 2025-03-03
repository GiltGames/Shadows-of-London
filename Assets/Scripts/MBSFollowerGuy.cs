using UnityEngine;
using UnityEngine.AI;

public class MBSFollowerGuy : MonoBehaviour
{

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] Transform trnCurrentTarget;
    [SerializeField] float fltDistance;
    [SerializeField] bool isWaiting;
    [SerializeField] MBSBasicNavigationGUy mbsParentNav;


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
