using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    [SerializeField]
    public Image inventoryIcon;   //Only a Reference to update the image of slot when new item added
    public GameObject itemInSlot; //thats for using information of the game object that the slot is showing in inventory
    public GameObject itemPanel;   // that right click panel showing glossary use combine thing, its not active
    RectTransform itemPanelrt;  
    public int itemIndex;  //index of the item stored in the slot
    RectTransform rt;
    
    private void Awake()
    {

        itemPanelrt = itemPanel.GetComponent<RectTransform>();
        rt = GetComponent<RectTransform>();
    
         
        
    }

    private void Start()
    {     
    }


    public void ShowItemPanelScreen()
    {
        if (inventoryIcon.sprite != null)
        {
            
            
            itemPanel.transform.SetAsLastSibling();
            itemPanelrt.anchoredPosition = rt.anchoredPosition;
            itemPanel.SetActive(true);
            

        }
    }

}
