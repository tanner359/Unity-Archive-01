using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    CanvasDisplay canvasDisplay;

    InventoryManager inventoryManager;

    public List<GameObject> inventoryList = new List<GameObject>();

    public List<Collider2D> interactableItems = new List<Collider2D>();

    CircleCollider2D playerZone;
    public ContactFilter2D contactFilter;
    public GameObject mainHand, offHand, head, chest, boots, arms;
    

    public int inventoryMax = 20;

    bool inventoryOpen;



    // Start is called before the first frame update
    void Start()
    {
        canvasDisplay = GameObject.Find("Canvas_Display").GetComponent<CanvasDisplay>();
        inventoryManager = GameObject.Find("Canvas_Display").transform.GetChild(0).GetComponent<InventoryManager>();
        playerZone = gameObject.GetComponent<CircleCollider2D>();        
    }
    

    // Update is called once per frame
    void Update()
    {      
        
        checkForInteractables(playerZone);

        if (Input.GetKeyDown(KeyCode.JoystickButton2) && interactableItems.Count > 0 || Input.GetKeyDown(KeyCode.E) && interactableItems.Count > 0){ // pick up item and adds to inventory
            pickUp();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Q)){
            DropItem(inventoryList[0].gameObject);
        }
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            InventoryOpenClose();
        }
    }



    public void InventoryOpenClose()
    {
        if (!inventoryOpen)
        {
            gameObject.GetComponent<Player_Controller>().enabled = false;
            inventoryOpen = true;
            inventoryManager.gameObject.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<Player_Controller>().enabled = true;
            inventoryOpen = false;
            inventoryManager.gameObject.SetActive(false);
        }
    }

    public void pickUp(){ // pick up closest item
        if(inventoryList.Count != 20)
        {
            inventoryList.Add(getClosestItem(gameObject.transform, interactableItems));
            inventoryManager.updateInventory(inventoryList);
        }
        else
        {
            Debug.Log("Inventory Full");
        }
    }
    public void DropItem(GameObject itemToDrop){
        itemToDrop.AddComponent<Rigidbody2D>();
        itemToDrop.GetComponent<Collider2D>().isTrigger = false;
        itemToDrop.GetComponent<Transform>().SetParent(null);
        inventoryList.RemoveAt(0);
    }
    public void checkForInteractables(Collider2D playerCollider){ // check for interactables
        playerCollider.OverlapCollider(contactFilter, interactableItems);
        if(interactableItems.Count > 0){
            canvasDisplay.displayInteractText(getClosestItem(gameObject.GetComponent<Transform>(), interactableItems).transform, "E");
        }
        else{
            canvasDisplay.HideText();
        }
    }
    public GameObject getClosestItem(Transform playerPos, List<Collider2D> interactableItems){ // returns back the closest item
        GameObject closestItem = interactableItems[0].gameObject;
        float minDistance = Vector2.Distance(playerPos.position, interactableItems[0].gameObject.transform.position);
        for (int i = 0; i < interactableItems.Count; i++){
            if(minDistance > Vector2.Distance(playerPos.position, interactableItems[i].gameObject.transform.position)){
                minDistance = Vector2.Distance(playerPos.position, interactableItems[i].gameObject.transform.position);
                closestItem = interactableItems[i].gameObject;
            }
        }
        return closestItem;
    }
}
