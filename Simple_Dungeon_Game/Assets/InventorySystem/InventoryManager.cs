using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Player_Inventory playerInventory;
    public List<GameObject> slots;  
    GameObject itemHolding;
    public GameObject slot;
    bool inventoryOpen;
    bool isHoldingItem;




    private void Update()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (isHoldingItem)
        {
            slot = FindSlot();
            itemHolding.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0) && slot != null && slot.transform.childCount == 1)
            {             
                placeObject();
            }
            else if(Input.GetMouseButtonDown(0) && slot == null) // drop item
            {
                dropObject();
            }
        }
        if (Input.GetMouseButtonUp(0) && slot != null)
        {
            slot.transform.GetChild(0).GetComponent<Button>().enabled = true;
        }
        
    }

    public void updateInventory(List<GameObject> inventoryItems)
    {
        for (int k = 0; k < slots.Count; k++)
        {
            if (slots[k].transform.childCount == 1)
            {
                inventoryItems[inventoryItems.Count-1].transform.parent = slots[k].transform;
                inventoryItems[inventoryItems.Count-1].SetActive(false);
                slots[k].transform.GetChild(0).GetComponent<Image>().sprite = inventoryItems[inventoryItems.Count-1].GetComponent<SpriteRenderer>().sprite;
                slots[k].transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().color = inventoryItems[inventoryItems.Count - 1].GetComponent<Item_Stats>().itemRarity;
                slots[k].transform.GetChild(0).GetComponentInChildren<Text>().text = inventoryItems[inventoryItems.Count-1].name;
                slots[k].transform.GetChild(0).GetComponent<Image>().color = new Color(slots[k].transform.GetChild(0).GetComponent<Image>().color.r, slots[k].transform.GetChild(0).GetComponent<Image>().color.g, slots[k].transform.GetChild(0).GetComponent<Image>().color.b, 100);
                k = 9999;
            }
        }
    }
    public void moveObject(GameObject slot)
    {
        if (!isHoldingItem && slot.transform.childCount > 1)
        {
            slot.transform.GetChild(0).GetComponent<Image>().sprite = null;
            slot.transform.GetChild(0).GetComponent<Image>().color = new Color(slot.transform.GetChild(0).GetComponent<Image>().color.r, slot.transform.GetChild(0).GetComponent<Image>().color.g, slot.transform.GetChild(0).GetComponent<Image>().color.b, 0);
            slot.transform.GetChild(0).GetComponentInChildren<Text>().text = "";
            itemHolding = slot.transform.GetChild(1).gameObject;
            itemHolding.transform.parent = null;
            itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 10;
            isHoldingItem = true;
            itemHolding.gameObject.SetActive(true);
            itemHolding.GetComponent<Rigidbody2D>().simulated = false;
            itemHolding.transform.eulerAngles = new Vector3(0, 0, 0);       
            if (slot.transform.GetChild(0).transform.GetChild(0).transform.childCount > 0)
            {
                slot.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    public void placeObject()
    {
        if (FindSlot().gameObject.CompareTag("Inventory Slot"))
        {
            slot.transform.GetChild(0).GetComponent<Button>().enabled = false;
            itemHolding.transform.parent = slot.transform;
            itemHolding.SetActive(false);
            slot.transform.GetChild(0).GetComponent<Image>().sprite = itemHolding.GetComponent<SpriteRenderer>().sprite;
            slot.transform.GetChild(0).GetComponentInChildren<Text>().text = itemHolding.name;
            slot.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Text>().color = itemHolding.GetComponent<Item_Stats>().itemRarity;
            slot.transform.GetChild(0).GetComponent<Image>().color = new Color(slot.transform.GetChild(0).GetComponent<Image>().color.r, slot.transform.GetChild(0).GetComponent<Image>().color.g, slot.transform.GetChild(0).GetComponent<Image>().color.b, 100);
            isHoldingItem = false;
            itemHolding = null;
        }
        else if (FindSlot().gameObject.CompareTag(itemHolding.gameObject.tag))
        {
            slot.transform.GetChild(0).GetComponent<Button>().enabled = false;
            slot.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            itemHolding.transform.parent = slot.transform;
            itemHolding.SetActive(false);
            slot.transform.GetChild(0).GetComponent<Image>().sprite = itemHolding.GetComponent<SpriteRenderer>().sprite;
            slot.transform.GetChild(0).transform.GetChild(0).GetComponentInChildren<Text>().color = itemHolding.GetComponent<Item_Stats>().itemRarity;
            slot.transform.GetChild(0).GetComponentInChildren<Text>().text = itemHolding.name;
            slot.transform.GetChild(0).GetComponent<Image>().color = new Color(slot.transform.GetChild(0).GetComponent<Image>().color.r, slot.transform.GetChild(0).GetComponent<Image>().color.g, slot.transform.GetChild(0).GetComponent<Image>().color.b, 100);
            isHoldingItem = false;
            itemHolding = null;
        }
        
    }
    public void dropObject()
    {
        playerInventory.inventoryList.Remove(itemHolding);
        itemHolding.SetActive(true);
        itemHolding.GetComponent<Rigidbody2D>().simulated = true;
        itemHolding.transform.parent = null;
        itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 0;
        itemHolding.transform.position = new Vector3(itemHolding.transform.position.x, itemHolding.transform.position.y, -2);
        isHoldingItem = false;
        itemHolding = null;
    }

    public GameObject FindSlot()
    {
        
        GameObject slot = null;
        List<GameObject> slots =  new List<GameObject>();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if(gameObject.transform.GetChild(i).gameObject.layer == (15) || gameObject.transform.GetChild(i).gameObject.layer == (16))
            {
                slots.Add(gameObject.transform.GetChild(i).gameObject);
            }
        }
        float minDistance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), slots[0].transform.position);
        slot = slots[0].gameObject;
        for (int i = 0; i < slots.Count; i++)
        {           
            if(Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), slots[i].transform.position) < minDistance)
            {
                Debug.Log("slot: " + slots[i].transform.position + "mouse: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
                slot = slots[i];
                minDistance = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), slots[i].transform.position);
            }
        }
        if(minDistance > 3)
        {
            return null;
        }
        else
        {
            return slot;
        }      
    }  
}
