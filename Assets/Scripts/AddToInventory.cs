using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class AddToInventory : MonoBehaviour
{
    public Image[] mugShots;
    public Image[] clues;
    public int[] enemyOrder;
    public int nextSlot = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddClue()
    {
        // clues[nextSlot] is set to the sprite on the object (scriptable object?)
        //clues[nextSlot].sprite = clue.clueSprite;
        // mugShots[nextSlot] reveals question mark icon
        mugShots[nextSlot].enabled = true;
        // enemy associated with clue is assigned the int nextSlot so its position is stored (in MBSArrestUpdateUI?)
            // int enemyNum = clue.enemyInt
            // enemyOrder[nextSlot] = enemyNum;
        // add 1 to nextSlot
        nextSlot ++;
    }

    
    public void AddEnemy()
    {
        // if enemy is captured, mugShots[enemy.enemyslot] is set to enemy.Image
    }
    


}
