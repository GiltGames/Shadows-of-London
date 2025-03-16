using UnityEngine;

public class MBSSetUp : MonoBehaviour
{

    // sets up the final way point - selects which boat it is on

    [SerializeField] Transform[] trnPossibleExit;
    [SerializeField] Transform[] trnFinalWayPoint;
    [SerializeField] Transform[] trnBoats;
    [SerializeField] Transform trnEscapeBoat;

    [SerializeField] MBSCriminalUnID[] mbsCriminal;
    [SerializeField] Timer mbsTime;
    [SerializeField] float fltRunforBoatTime =60;
    [SerializeField] Transform[] allWaypoints;
    [SerializeField] WaypointIdentifier[] wayAllwaypoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       

        // selects the boat for departure
        int intExitTmp = Random.Range(0, trnPossibleExit.Length);

      
        
        trnEscapeBoat = trnBoats[intExitTmp];


        // redundant script that set the time for progression through waypoints - now superseded by the criminal waypoints
        for (int i = 0; i < mbsCriminal.Length; i++)
        {
            trnFinalWayPoint[i].position = trnPossibleExit[intExitTmp].position;

         
            mbsCriminal[i].fltTimetoMovetoCriminalWaypoint[mbsCriminal[i].fltTimetoMovetoCriminalWaypoint.Length-1] = fltRunforBoatTime * mbsCriminal[i].fltRuntoBoatMod;

         

         
        }
    }

    
}
