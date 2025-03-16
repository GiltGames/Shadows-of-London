using UnityEngine;

public class MBSGhost : MonoBehaviour
{

    // the ghost tracks the blue tracking fog.
    // it alawys turns to look at the player, but can't rotate to look up or down


    [SerializeField] Transform trnFog;
    [SerializeField] Animator anim;
    [SerializeField] Transform trnPlayer;
    void Update()
    {

        // follows fog
        transform.position = trnFog.position + Vector3.down * 0.5f - transform.forward;   ;
        anim.SetBool("Still", true);
        
        //looks at player
        transform.LookAt(trnPlayer.position);

        //will not look up or down
        transform.eulerAngles = new Vector3(0,transform.rotation.y,0); 
    }
}
