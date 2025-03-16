using UnityEngine;

public class MBSFaceCamera : MonoBehaviour
{
  
    // this is attached to the text above the characters to make sure it faces the camera
    
    [SerializeField] Transform camCamera;


    void Update()
    {
        transform.LookAt(camCamera);
    }
}
