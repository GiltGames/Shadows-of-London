using UnityEngine;

public class MBSSetUp : MonoBehaviour
{
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

        for (int i = 0; i < mbsCriminal.Length; i++)
        {
            trnFinalWayPoint[i].position = trnPossibleExit[intExitTmp].position;

            float interval = (mbsTime.timeLimit - (fltRunforBoatTime * mbsCriminal[i].fltRuntoBoatMod)) / mbsCriminal[i].fltTimetoMovetoCriminalWaypoint.Length;
            mbsCriminal[i].fltTimetoMovetoCriminalWaypoint[mbsCriminal[i].fltTimetoMovetoCriminalWaypoint.Length-1] = fltRunforBoatTime * mbsCriminal[i].fltRuntoBoatMod;

            for (int j = 0; j < mbsCriminal[i].fltTimetoMovetoCriminalWaypoint.Length - 1; j++)
            {

                mbsCriminal[i].fltTimetoMovetoCriminalWaypoint[j] = mbsTime.timeLimit -
                   Mathf.Lerp(interval * j, interval * (j + 1), Random.Range(0, 1.0f));

            }

            //sets final waypoint time to runto boat time

         
        }
    }

    
}
