using UnityEngine;
using UnityEngine.AI;

public class MBSCriminalUnID : MonoBehaviour
{
    [SerializeField] MBSBasicNavigationGUy mbsNav;
    [SerializeField] bool isPretendingToBeCrowd;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] GameObject gmoAura;
    [SerializeField] bool isDetectable;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsNav = GetComponent<MBSBasicNavigationGUy>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        isPretendingToBeCrowd = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPretendingToBeCrowd)
        {
            mbsNav.enabled = false;


        }




    }

    private void OnMouseEnter()
    {
        if (isDetectable)
        {
            anim.SetTrigger("Seen");
            agent.SetDestination(transform.position);
            mbsNav.fltDelayCount = 0;
            mbsNav.isWaiting = true;

            gmoAura.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        gmoAura.SetActive(false);
    }

}
