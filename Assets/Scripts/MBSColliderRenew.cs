using UnityEngine;

public class MBSColliderRenew : MonoBehaviour
{
    // redundant script that rest the collider on an object every few seconds -

    // this was used while the rightclick to arrest function was in play - now superceded by the MBSManualArrest logic


    [SerializeField] Collider collider;
    [SerializeField] Collider originalCollider;
    [SerializeField] float fltInterval=3    ;
    [SerializeField] float fltCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider>();
        originalCollider = collider;

    }

    // Update is called once per frame
    void Update()
    {
        fltCounter += Time.deltaTime;

        if (fltCounter > fltInterval)
        {
            fltCounter = 0;
            collider.enabled = false;
            collider.enabled = true;
            collider = originalCollider;

        }


    }
}
