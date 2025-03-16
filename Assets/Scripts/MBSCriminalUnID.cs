using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class MBSCriminalUnID : MonoBehaviour
{
    // this script is attached to each criminal and controls most of the criminal-specific logic


    [SerializeField] MBSBasicNavigationGUy mbsNav;
    [SerializeField] AddToInventory mbsInventory;
    [SerializeField] MBSArrestGuy mbsArrest;
    [SerializeField] MBSArrestUpdateUI mbsArrestUpdateUI;
    [SerializeField] bool isPretendingToBeCrowd;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] GameObject gmoAura;
    public bool isDetectable;
    [SerializeField] int intHintType;
    public bool isArrested;

    public bool isInCustody;
    public bool isTryingtoMakeProgress;

    [SerializeField] Transform[] trnWayPointinWorld;
    [SerializeField] Transform[] trnWayPointinRange;


    [Header("Criminal Move")]
    public Transform trnNewTarget;
    [SerializeField] Transform[] trnWayPointsCriminal;
    public float[] fltTimetoMovetoCriminalWaypoint;
    public int intCriminalProgress;
    [SerializeField] Timer mbsTimer;
    public bool isEscaping;
    public float fltRuntoBoatMod;
    [SerializeField] float fltVariationInTarget = 2;

    [Header("Clue Related")]
    public int intCriminalIndex;
    public GameObject[] gmoHint;
    public int[] intClue;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sets up navigation paramenters.
        // quite a lot of the logic is on the MBSBasicNavigationGuy Script

        mbsNav = GetComponent<MBSBasicNavigationGUy>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        mbsNav.isWanderingMode = true;
        mbsArrest = GetComponent<MBSArrestGuy>();


        // not needed now - used when time to move to the next waypoint was set by script - now they just move through in order
        mbsTimer = FindFirstObjectByType<Timer>();


/*
        for (int i = 0; i < mbsNav.trnWaypoint.Length; i++)
        {
            trnWayPointinRange[i] = mbsNav.trnWaypoint[i];

        }
*/

        // intClue[i] = i by default but we set up so it can be randmoised
        for (int i = 0; i < intClue.Length; i++)
        {
            intClue[i] = i;
        }





    }

    // Update is called once per frame
    void Update()
    {
        // each frame checks to see if their clue has been found - stops running when the clue is found

        if (!isDetectable)
        {
            FnCheckIfClueFound();
            
        }




    }

   

   

   


    public void FnArrest()
    {
        //Update the UI


        mbsArrestUpdateUI.FnArrestUpdateUI(intCriminalIndex);
        // Call the arrest animation


        //Stop moving....
        /*
        mbsNav.isWaiting = true;
        mbsNav.isWanderingMode = false;
        agent.SetDestination(transform.position);
        */
    }




    void FnEvade()
    {

        //No longer used

    }


    public void FnCriminalMoveUpdate()
    {

        // called when the criminal reaches its criminal waypoint
        // this increments the progress and sets the next criminal waypoint
        // unless it is the last one, in which case the criminal gets away and the UI is updated

        // the criminal waypoints are fixed for each criminal and are set in the inspector

        intCriminalProgress++;

        Debug.Log(transform.name + "progress updated to " + intCriminalProgress);

        // check if the cirminal has reached their final waypoint
        if (intCriminalProgress > trnWayPointsCriminal.Length-1)
        {


            //Gets away
            // FnEscape();
            FnGotAway();

        }

        else
        { 


            //sets destination to next criminal waypoint

        trnNewTarget = trnWayPointsCriminal[intCriminalProgress];
        Vector3 fltOffsetTmp = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * fltVariationInTarget;

      
        mbsNav.trnCurrentTarget = trnNewTarget;
        mbsNav.vecNavTarget = trnNewTarget.position + fltOffsetTmp;
          agent.SetDestination(mbsNav.vecNavTarget);

        anim.SetBool("Still", false);
       

        
        


        }

      



    }


    void FnEscape()
    {
        //redundant - function not called now
        
        isEscaping = true;
        


    }

    public void FnGotAway()
    {
        //updates the UI if the criminal gets away and hides the criminal object

      mbsInventory.GotAway(intCriminalIndex);
        
        
        mbsNav.isCriminal = false;
        agent.enabled = false;
        gameObject.SetActive(false);

    }

   

void    FnCheckIfClueFound()
    {
       // checks to see if the cirmianls clue is found
       // criminal and clue identified by the intCirminalIndex 
        
        for (int i = 0; i < intClue.Length; i++)
        {
            if (mbsInventory.enemyOrder[i] == intCriminalIndex)
            {
                FnSwitchOnHint(intCriminalIndex);
                isDetectable = true;
            }
        }
    }



    public void FnSwitchOnHint(int intHintTmp)
    {
        // intClue[i] = i by default but we set up so it can be randmoised


        gmoHint[intClue[intHintTmp]].SetActive(true);

    }

}
