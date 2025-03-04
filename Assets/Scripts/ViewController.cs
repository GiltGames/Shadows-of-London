
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class ViewController : MonoBehaviour
{
    // Vector3 startingPos;
    // Quaternion startingRot;
    // Vector3 topdownPos = new Vector3(0, 3.16f, -0.13f);
    // Quaternion topdownRot = Quaternion.Euler(70,0,0);
    bool isTopDown = false;
    // Transform playerTransform;
    // Vector3 positionOffset;
    // Quaternion rotationOffset;

    public CinemachineCamera thirdPCam;
    public CinemachineCamera topdownCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thirdPCam.Priority = 1;

        // // get offset of camera position gameobject compared to player
        // playerTransform = GetComponentInParent<Transform>();
        // positionOffset = playerTransform.position - transform.position;
        // // get difference between the rotations
        // rotationOffset = Quaternion.Inverse(playerTransform.rotation) * transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            //playerTransform = GetComponentInParent<Transform>();
            isTopDown = !isTopDown;
            if(!isTopDown)
            {
                thirdPCam.Priority = 1;
                topdownCam.Priority = 0;
                // // reset position? 
                // transform.position = Vector3.zero;
                // transform.rotation = new Quaternion(0,0,0,0);

                // // set position to player position plus offset
                // transform.position = playerTransform.position + positionOffset;
                // transform.rotation = playerTransform.rotation * rotationOffset;

            }
            if(isTopDown)
            {
                topdownCam.Priority = 1;
                thirdPCam.Priority = 0;
                // // set position to topdown position plus player position
                // transform.position = topdownPos + playerTransform.position;
                // transform.rotation = topdownRot;
            }
        }
    }
}
