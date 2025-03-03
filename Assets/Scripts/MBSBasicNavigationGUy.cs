using UnityEngine;
using UnityEngine.AI;

public class MBSBasicNavigationGUy : MonoBehaviour
{
    [Header ("Navigation")]
    [SerializeField] Transform[] trnWayPoint;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    
  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
