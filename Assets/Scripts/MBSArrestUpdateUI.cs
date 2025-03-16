using UnityEngine;

public class MBSArrestUpdateUI : MonoBehaviour
{
   
    //Script attached to each criminal
    // updated the UI when the criminal is taken into custody
    // called from the MBSArrestGuy script

    
    public Sprite mugshot;
    int enemyInt;
    AddToInventory addToInvScript;

    void Start()
    {
        enemyInt = GetComponent<MBSCriminalUnID>().intCriminalIndex;
        addToInvScript = FindFirstObjectByType<AddToInventory>();
    } 

    public void FnArrestUpdateUI(int IntCriminalCaught)

    { 
        addToInvScript.AddEnemy(mugshot, enemyInt);
    
    }

}
