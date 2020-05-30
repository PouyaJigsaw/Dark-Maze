using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerdab : MonoBehaviour
{

    public RespawnWaypoint[] respawnPlaces;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = RandomRespawnPlace().transform.position;
            other.gameObject.SetActive(true);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    

   RespawnWaypoint RandomRespawnPlace()
    {
        int randomNum = Random.Range(0, respawnPlaces.Length);                         
            return respawnPlaces[randomNum];        
    }


}
