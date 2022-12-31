using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DamageHandler : MonoBehaviour
{
    private static int swingBaseDmg = 2;
    private static int lungBaseDmg = 4;
    private static float critSeverity = 1.5f;

    Player_Stats playerStats;
    Player_Inventory playerInventory;
    CanvasDisplay canvasDisplay;
    Player_Controller playerController;

    public ContactFilter2D filter;
    public List<Collider2D> enemyHB = new List<Collider2D>();

    int weaponDamage;
    int weaponKnockback;

    private void Start()
    {
        playerStats = gameObject.GetComponent<Player_Stats>();
        playerInventory = gameObject.GetComponent<Player_Inventory>();
        playerController = gameObject.GetComponent<Player_Controller>();
        canvasDisplay = GameObject.Find("Canvas_Display").GetComponent<CanvasDisplay>();
    }

    public void LungeAttack()
    {
        weaponKnockback = playerController.getDirection() * playerInventory.mainHand.GetComponent<Item_Stats>().knockback;
        weaponDamage = playerInventory.mainHand.GetComponent<Item_Stats>().damage;
        int finalDamage = (lungBaseDmg * weaponDamage) + (int)(playerStats.power * Random.Range(0.3f, 0.4f));
        if (playerInventory.mainHand.GetComponent<Collider2D>().OverlapCollider(filter, enemyHB) > 0)
        {
            for(int i = 0; i < enemyHB.Count; i++)
            {         
                if(getRandomNum(0,100) > playerStats.critChance)
                {
                    enemyHB[i].gameObject.GetComponent<Creature_Stats>().health -= finalDamage;
                    enemyHB[i].gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(weaponKnockback, 5f), ForceMode2D.Impulse);
                    canvasDisplay.displayDamageText(Color.yellow, finalDamage, 1.5f, enemyHB[i].transform);
                }
                else if(getRandomNum(0, 100) <= playerStats.critChance)
                {
                    enemyHB[i].gameObject.GetComponent<Creature_Stats>().health -= (int)(finalDamage * critSeverity);
                    enemyHB[i].gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(weaponKnockback+10, 5f), ForceMode2D.Impulse);
                    canvasDisplay.displayDamageText(Color.red, (int)(finalDamage * critSeverity), 1.5f, enemyHB[i].transform);
                }              
            }           
        }
    }
    public void SwingAttack()
    {
        
        weaponKnockback = playerController.getDirection() * playerInventory.mainHand.GetComponent<Item_Stats>().knockback;
        weaponDamage = playerInventory.mainHand.GetComponent<Item_Stats>().damage;       
        int finalDamage = (swingBaseDmg * weaponDamage) + (int)(playerStats.power * Random.Range(0.3f, 0.4f));
        if (playerInventory.mainHand.GetComponent<Collider2D>().OverlapCollider(filter, enemyHB) > 0)
        {
            int randomNum = getRandomNum(0, 100);
            for (int i = 0; i < enemyHB.Count; i++)
            {             
                if (randomNum > playerStats.critChance)
                {
                    enemyHB[i].gameObject.GetComponent<Creature_Stats>().health -= finalDamage;
                    enemyHB[i].gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(weaponKnockback, 10f), ForceMode2D.Impulse);
                    canvasDisplay.displayDamageText(Color.yellow, finalDamage, 1.5f, enemyHB[i].transform);
                }
                else if (randomNum <= playerStats.critChance)
                {
                    enemyHB[i].gameObject.GetComponent<Creature_Stats>().health -= (int)(finalDamage * critSeverity);
                    enemyHB[i].gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(weaponKnockback+10, 10f), ForceMode2D.Impulse);
                    canvasDisplay.displayDamageText(Color.red, (int)(finalDamage * critSeverity), 1.5f, enemyHB[i].transform);
                }               
            }
        }
    }
    public int getRandomNum(int min, int max)
    {
        return Random.Range(min, max);
    }    
}
