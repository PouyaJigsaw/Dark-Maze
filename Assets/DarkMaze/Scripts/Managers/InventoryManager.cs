using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
  
    public static int inventorySlotFlag = 0;   //store the number of slot that new item should be added to that
    public GameObject InventoryMenu;   //this Reference (with an empty Script) is CRUCIAL for getting the children component => dont delete it
    public GameObject itemPanel;   // when you touch item in inventory screen this panel appears = shows combine, use, glossary
    public InventorySlot[] inventorySlots;
    GameObject[] items;
    Ray ray;  //for deactivating item panel when not clicked
    RaycastHit hit;

 
    private void Awake()
    {
        if (instance == null)
            instance = this;

        inventorySlots = InventoryMenu.GetComponentsInChildren<InventorySlot>();
        items = GameObject.FindGameObjectsWithTag("Item");
   
    }



    

    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        // check when to deactive panel (like clicking on other things or clicking resume button)
        if(itemPanel.activeSelf)
        {
            if(Input.GetMouseButtonDown(0))
            {
                itemPanel.SetActive(false);
            }
        }

    }

    public void AddItem(GameObject item, Sprite inventoryImage)
    {
        //Add the item prefab  to that slot(itemInSlot), update the image of that slot and update the slot flag      
       inventorySlots[inventorySlotFlag].itemInSlot = item;
       inventorySlots[inventorySlotFlag].inventoryIcon.enabled = true;
       inventorySlots[inventorySlotFlag].inventoryIcon.sprite = inventoryImage;
       inventorySlots[inventorySlotFlag].itemIndex = item.GetComponent<Item>().index;
       inventorySlotFlag++;

    }

    public GameObject ReturnItem(int index)
    {

        for(int i = 0; i < items.Length; i++)
        {
            
            if(items[i].GetComponent<Item>().index == index)
            {
                return items[i];
                break;
            }
        }

        return null;
    }
    

}
