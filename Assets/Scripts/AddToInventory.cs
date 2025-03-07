using UnityEngine.UI;
using UnityEngine;
using TMPro;
using NUnit.Framework;

public class AddToInventory : MonoBehaviour
{
    public Image[] mugShots;
    public Image[] clues;
    public int[] enemyOrder;
    public int nextSlot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextSlot = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // this function is called from RaycastCube script
    public int AddClue(int clueNum, Sprite clueSprite, int enemyInt)
    {
        // clues[nextSlot] is set to the sprite on the object (scriptable object?)
        clues[nextSlot].sprite = clueSprite;
        clues[nextSlot].gameObject.SetActive(true);
        // mugShots[nextSlot] reveals question mark icon
        mugShots[nextSlot].gameObject.SetActive(true);
        Debug.Log(mugShots[nextSlot]);
        // enemy associated with clue is assigned the int nextSlot so its position is stored
        enemyOrder[nextSlot] = enemyInt;
        Debug.Log(enemyOrder[nextSlot]);
        // add 1 to nextSlot
        nextSlot += 1;

        return nextSlot;
    }

    // if enemy is captured, mugShots[enemy.enemyslot] is set to enemy.Image
    // this function is called from MBSArrestUpdateUI script
    public void AddEnemy(Sprite mugshot, int enemyInt)
    {
        // find enemyInt position in enemyOrder[]
        int enemyPos = -1;
        for (int i = 0; i < enemyOrder.Length; i++)
        {
            if (enemyOrder[i] == enemyInt) 
            {
                enemyPos = i;
                break;
            }
        }

        // for debugging...
        if (enemyPos == -1)
        {
            Debug.Log("Enemy not found");
        } 
        else{
            Debug.Log("enemyInt:" + enemyInt + " enemyPos: " + enemyPos);
        }
        
        mugShots[enemyPos].sprite = mugshot;

    }
    


}
