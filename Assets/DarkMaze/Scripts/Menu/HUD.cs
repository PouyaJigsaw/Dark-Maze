using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUD : MonoBehaviour
{
    [SerializeField]
    Button inventoryButton;

    [SerializeField]
    GameObject InventoryMenu;

    [SerializeField]
    GameObject Map;

    [SerializeField]
    GameObject flashLight;

    [SerializeField]
    GameObject lantern;

    public void HandleInventoryResumeOnClickEvent()
    {

        Time.timeScale = 1;
        InventoryMenu.SetActive(false);
    }
    public void HandleInventoryOnClickEvent()
    {
        Time.timeScale = 0;
        InventoryMenu.SetActive(true);

    }

    public void HandleMapOnClickEvent()
    {
        Time.timeScale = 0;
        Map.SetActive(true);
    }

    public void HandleResumeMapMenuOnClickEvent()
    {
        Time.timeScale = 1;
        Map.SetActive(false);
    }

    public void HandleSwitchOnClickEvent()
    {
        if(flashLight.active)
        {
            flashLight.SetActive(false);
            lantern.SetActive(true);
        }
        else
        {
            flashLight.SetActive(true);
            lantern.SetActive(false);
        }
        
    }
}
