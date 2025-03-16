using UnityEngine;

public class MBSFogDrift : MonoBehaviour
{
    // attached to fog to allow it to drift
    // fog type 1 tracks the player - this goes round the ghost
    // fog type 0 moves according to the direction set in the inspector


    public Vector3 vecFogDrift;
    [SerializeField] float fltFogSpeed;
    public float vecFogExpand=1;
    [SerializeField] Transform trnPlayer;
    [SerializeField] int intFogType;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trnPlayer = FindFirstObjectByType<PlayerMovement>().transform;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (intFogType ==1)
        {
            vecFogDrift = (trnPlayer.position - transform.position).normalized * fltFogSpeed;
            vecFogDrift.y = 0;


        }


            transform.Translate(vecFogDrift * Time.deltaTime,Space.World);
            transform.localScale *= vecFogExpand * Time.deltaTime;
        




    }
}
