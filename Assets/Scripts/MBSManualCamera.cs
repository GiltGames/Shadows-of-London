using UnityEngine;

public class MBSManualCamera : MonoBehaviour
{
    [SerializeField] Transform trnCameraTrackPoint;
    [SerializeField] Transform trnViewed;
    [SerializeField] Vector3 vecOffset;
    [SerializeField] float fltTolerance;
    [SerializeField] float fltMarginofError;
    [SerializeField] float fltTrackSpeed;
    [SerializeField] float fltTrackSlowSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void LateUpdate()
    
    {
        vecOffset = trnCameraTrackPoint.position - transform.position;
       //allow a small margin so the camera doesn't jiggle allthe time
        
        if (vecOffset.magnitude > fltMarginofError)
        {
            //if a long way out, moved in quickly

            if (vecOffset.magnitude > fltTolerance)
            {
                transform.position += vecOffset.normalized * fltTrackSpeed * Time.deltaTime;



            }


            //if closes, move in slowly
            else
            {
                transform.position += vecOffset.normalized * fltTrackSlowSpeed * Time.deltaTime;

            }



        }

        transform.LookAt(trnViewed.position);


    }
}
