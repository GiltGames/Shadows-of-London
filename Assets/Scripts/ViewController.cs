
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class ViewController : MonoBehaviour
{
    bool isTopDown = false;

    public CinemachineCamera thirdPCam;
    public CinemachineCamera topdownCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thirdPCam.Priority = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            isTopDown = !isTopDown;
            if(!isTopDown)
            {
                thirdPCam.Priority = 1;
                topdownCam.Priority = 0;

            }
            if(isTopDown)
            {
                topdownCam.Priority = 1;
                thirdPCam.Priority = 0;
            }
        }
    }
}
