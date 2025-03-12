using UnityEngine;

public class MBSDoor : MonoBehaviour
{
    [SerializeField] float fltMoveSpeed;
        [SerializeField] float fltLowerAngle = 0;
        [SerializeField] float fltUpperAngle = 60;
    [SerializeField] bool isOpening;
    [SerializeField] float fltStartAngle;
    [SerializeField] float fltCurrentAngle;


    [SerializeField] Renderer objectRenderer;
    [SerializeField] Material originalMaterial;
    [SerializeField] Material newMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;


        fltStartAngle = transform.localEulerAngles.y; 
        fltCurrentAngle = transform.localEulerAngles.y;
    


    }

    private void OnMouseEnter()
    {
        objectRenderer.material = newMaterial;


    }

   void OnMouseExit()
    {
        objectRenderer.material = originalMaterial;
    }



    public void FnDoorMove()
    {

        Debug.Log("Door move script called");
        if (!isOpening)
        {
            fltCurrentAngle += fltMoveSpeed;
            


            if ((fltCurrentAngle -fltStartAngle) > fltUpperAngle)
            {
                isOpening = true;
                fltCurrentAngle -= fltMoveSpeed * 2;
            }
            transform.localRotation = Quaternion.Euler(transform.rotation.x, fltCurrentAngle, transform.rotation.z);

        }
        else

        {
            fltCurrentAngle -= fltMoveSpeed;
           


            if ((fltCurrentAngle - fltStartAngle) < fltLowerAngle)
            {
                isOpening = false;
                fltCurrentAngle += fltMoveSpeed * 2;
            }
            transform.localRotation = Quaternion.Euler(transform.rotation.x, fltCurrentAngle, transform.rotation.z);
        }



    }

    

}
