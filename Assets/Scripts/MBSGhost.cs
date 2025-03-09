using UnityEngine;

public class MBSGhost : MonoBehaviour
{
    [SerializeField] Transform trnFog;
    [SerializeField] Animator anim;
    [SerializeField] Transform trnPlayer;
    void Update()
    {
        transform.position = trnFog.position + Vector3.down * 0.5f - transform.forward;   ;
        anim.SetBool("Still", true);
        transform.LookAt(trnPlayer.position);
        transform.eulerAngles = new Vector3(0,transform.rotation.y,0); 
    }
}
