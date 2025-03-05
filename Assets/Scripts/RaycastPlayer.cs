using UnityEngine;
public class RaycastPlayer : MonoBehaviour
{
    Camera playerCamera;
    public float lineRange = 50f;
    public static Vector3 hitPosition;
    AddToInventory addToInventoryScript;
    Animator anim;

    void Start()
    {
        playerCamera = Camera.main;
        anim = GetComponentInChildren<Animator>();
        addToInventoryScript = FindFirstObjectByType<AddToInventory>();
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

            RaycastCube clueObject = hit.transform.GetComponent<RaycastCube>();
            EvidenceProperties evidenceProps = hit.transform.GetComponent<EvidenceProperties>();
            if(clueObject != null)
            {
                anim.SetTrigger("isPickingUp");
                Debug.Log("there is a clue");
                addToInventoryScript.nextSlot = addToInventoryScript.AddClue(clueObject.evidenceValue, evidenceProps.clueIcon, evidenceProps.enemyInt);
                Destroy(clueObject.gameObject);
            }
        }
    }
}
