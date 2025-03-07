using UnityEngine;

public class MBSArrestUpdateUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
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
