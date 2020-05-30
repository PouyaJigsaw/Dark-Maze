using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(Collider))]

public class Item : MonoBehaviour
{
    public Sprite inventoryImage;   
    public int index;    
    public GameObject mapIcon;
    void Start()
    {
        
        //Set the map icon transform to gameobject transform with proper values
        mapIcon.transform.position = gameObject.transform.position;
        mapIcon.transform.rotation.Set(90, 0, 0, 0);
        mapIcon.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
    }



    private void OnTriggerEnter(Collider other)
    {
       

        InventoryManager.instance.AddItem(this.gameObject, inventoryImage);

        //object is hidden, show on the map
        gameObject.SetActive(false);
        mapIcon.SetActive(true);

    }

   

}
