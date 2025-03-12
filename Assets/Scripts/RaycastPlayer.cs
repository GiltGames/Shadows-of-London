using System.Collections;
using UnityEngine;
public class RaycastPlayer : MonoBehaviour
{
    Camera playerCamera;
    public float lineRange = 50f;
    public static Vector3 hitPosition;
    AddToInventory addToInventoryScript;
    Animator anim;
    public PlayerSpeech playerSpeechScript;
   

    void Start()
    {
        playerCamera = Camera.main;
        anim = GetComponentInChildren<Animator>();
        addToInventoryScript = FindFirstObjectByType<AddToInventory>();
        playerSpeechScript = GetComponentInChildren<PlayerSpeech>();
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

            // Is the boject hit evidence

            if (hit.transform.GetComponent<RaycastCube>() != null) 
            {
                RaycastCube clueObject = hit.transform.GetComponent<RaycastCube>();
                Debug.Log("there is a clue");
                EvidenceProperties evidenceProps = hit.transform.GetComponent<EvidenceProperties>();
                StartCoroutine(PickUpItem(clueObject, evidenceProps));
                
                if(evidenceProps.clueDescription != null)
                {
                    if (playerSpeechScript == null)
                    {
                        Debug.Log("cant get player speech script");
                    }
                    else {
                    playerSpeechScript.ChangePlayerSpeech(evidenceProps.clueDescription);
                    }
                }
                                
            }

            // Is the boject hit a door?
            if (hit.transform.GetComponent<MBSDoor>() != null)
                {
                    MBSDoor mbsDoor = hit.transform.GetComponent<MBSDoor>();
                    mbsDoor.FnDoorMove();
                }

            // check for anything else we want to here


        }
    }

    IEnumerator PickUpItem(RaycastCube clueObject, EvidenceProperties evidenceProps)
    {
        anim.SetTrigger("isPickingUp");
        yield return new WaitForSeconds(2.1f);

        // nextSlot is assigned return value of AddClue()
        // AddClue() needs evidenceValue, clueIcon and enemyInt
        addToInventoryScript.nextSlot = addToInventoryScript.AddClue(clueObject.evidenceValue, evidenceProps.clueIcon, evidenceProps.enemyInt);
        Destroy(clueObject.gameObject);

    }

}
