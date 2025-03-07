using UnityEngine;

public class MBSDrain : MonoBehaviour
{
    [SerializeField] PlayerMovement mbsPlayer;
    [SerializeField] float fltStaminaLoss= 0.1f;
    [SerializeField] GameObject gmoGhost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mbsPlayer = FindFirstObjectByType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gmoGhost.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        gmoGhost.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            mbsPlayer.FnUpdateStamina(fltStaminaLoss * Time.deltaTime);


        }
    }


}
