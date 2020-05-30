using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLayer : MonoBehaviour
{
    int wallPillarFloorLayerMask;
    int lightLayerMask;

    GameObject objectToChangeLayer;

    Ray layerChangerRay;
    RaycastHit layerChangerHit;
    private void Awake()
    {
        wallPillarFloorLayerMask |= LayerMask.NameToLayer("Wall"); // 10
        wallPillarFloorLayerMask |= LayerMask.NameToLayer("Pillar"); // 11
        wallPillarFloorLayerMask |= LayerMask.NameToLayer("Floor"); // 9

        lightLayerMask |= LayerMask.NameToLayer("Wall Light"); // 16
        lightLayerMask |= LayerMask.NameToLayer("Floor Light"); // 17
        lightLayerMask |= LayerMask.NameToLayer("Pillar Light"); // 15

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall") || other.CompareTag("Pillar") || other.CompareTag("Floor"))
        {
            objectToChangeLayer = other.gameObject;
            layerChangerRay = new Ray(gameObject.transform.position, Vector3.Normalize(objectToChangeLayer.transform.position - gameObject.transform.position));
              
            if(Physics.Raycast(layerChangerRay, out layerChangerHit, wallPillarFloorLayerMask))
            {
                if (layerChangerHit.collider.gameObject == objectToChangeLayer)
                {
                    Debug.Log(objectToChangeLayer + " " + objectToChangeLayer.layer);
                    switch (objectToChangeLayer.layer)
                    {
                        case 10:
                            objectToChangeLayer.layer = 16;
                            break;
                        case 11:
                            objectToChangeLayer.layer = 15;
                            break;
                        case 9:
                            objectToChangeLayer.layer = 17;
                            break;
                        default:
                            break;
                    }

                }

            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        switch(other.gameObject.layer)
        {
            case 16:
                other.gameObject.layer = 10;
                break;
            case 15:
                other.gameObject.layer = 11;
                break;
            case 17:
                other.gameObject.layer = 9;
                break;
            default:
                break;
        }
    }
}
