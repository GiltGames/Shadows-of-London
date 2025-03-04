using UnityEngine;
public class RaycastPlayer : MonoBehaviour
{
    public Camera playerCamera;
    public float lineRange = 50f;
    public static Vector3 hitPosition;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit, lineRange))
            {
                hitPosition = hit.point;
                //Debug.Log("Hit position " + hit.point);
                Debug.Log("Hit object: " + hit.collider.name);
            }
        }
    }
}
