using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{


    public static SaveLoadManager instance;

    //list of things to save
    public Transform player;
    int inventoryLength;
    InventorySlot[] inventorySlots;
 
    private void Awake()
    {
        if (instance == null)
            instance = this;

        inventorySlots = InventoryManager.instance.inventorySlots;
        inventoryLength = inventorySlots.Length;

       
    }
    private void Start()
    {

        LoadGame();

    }
    public void SaveGame()
    {
        //save player position
        PlayerPrefs.SetFloat("x", player.position.x);
        PlayerPrefs.SetFloat("y", player.position.y);
        PlayerPrefs.SetFloat("z", player.position.z);

        //save inventory items
        for(int i = 0; i < inventoryLength; i++)
        {       
                PlayerPrefs.SetInt("slot " + i + " index", inventorySlots[i].itemIndex);
               
        }


        

    }
    public void LoadGame()
    {
       
        float x = PlayerPrefs.GetFloat("x");
        float y = PlayerPrefs.GetFloat("y");
        float z = PlayerPrefs.GetFloat("z");

        //Active false then true ==> for bug of navmesh
        player.gameObject.SetActive(false);
        player.transform.position = new Vector3(x,y,z);
        player.gameObject.SetActive(true);

  
        for (int i = 0; i < inventoryLength; i++)
        {
           
            int temp = PlayerPrefs.GetInt("slot " + i + " index");
          
            if (temp != 0)
            {
                GameObject tempItem = InventoryManager.instance.ReturnItem(temp);
                inventorySlots[i].itemInSlot = tempItem;
                inventorySlots[i].inventoryIcon.enabled = true;
                inventorySlots[i].inventoryIcon.sprite = tempItem.GetComponent<Item>().inventoryImage;
                inventorySlots[i].itemIndex = temp;
                tempItem.SetActive(false);
                tempItem.GetComponent<Item>().mapIcon.SetActive(true);
            }
            else
            {
                inventorySlots[i].itemIndex = 0;
            }
        }
        
    }
}
