using UnityEngine;

public class PlayerPickingUp : MonoBehaviour
{
    Animator anim;
    public bool isPickingUp = false;
    //AddToInventory addToInvScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isPickingUp);
        if(Input.GetKeyDown(KeyCode.F))
        {
            isPickingUp = true;
            PlayerPickUp();
            isPickingUp = false;
        }
    }

    // function to be called when evidence is clicked on
    // only when player is close enough - where to implement?
    void PlayerPickUp()
    {
        isPickingUp = true;
        anim.SetTrigger("isPickingUp");
    }
}
