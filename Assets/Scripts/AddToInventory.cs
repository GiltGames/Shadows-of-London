using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

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

    
    public void AddEnemy()
    {
        // if enemy is captured, mugShots[enemy.enemyslot] is set to enemy.Image

    }
    


}
