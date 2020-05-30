using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    //its an important number for mapping objectg locations in scene to the map.
    float coefficientMapping;

    Vector3 map3DSceneTo2DMapPos;
    Vector3 mappingOffset;

    [SerializeField]
    RawImage mapRawImage;

    [SerializeField]
    GameObject items;

    [SerializeField]
    RawImage blackImagePrefab;


    GameObject[] itemArray;
    private void Awake()
    {
        //24? thats the width of the maze, it depends of the size of maze TODO: make a code for all kinds of maze size
        coefficientMapping = (mapRawImage.GetComponent<RectTransform>().rect.width) / 24;
        Item[] itemsInGame = items.GetComponentsInChildren<Item>();
        itemArray = new GameObject[itemsInGame.Length];

        for (int i = 0; i < itemsInGame.Length; i++)
        {
            itemArray[i] = itemsInGame[i].gameObject;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector2 MapPlace;
        print(itemArray[0].transform.position);
        print(itemArray[0].transform.position * coefficientMapping);
        print(Camera.main.WorldToViewportPoint(itemArray[0].transform.position));
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mapRawImage.GetComponent<RectTransform>(), Camera.main.WorldToViewportPoint(itemArray[0].transform.position),
            Camera.main, out MapPlace);
        //RectTransformUtility.
        print(MapPlace);
        print(mapRawImage.GetComponent<RectTransform>().localScale);

       /* BoxCollider2D canvasCollider = GameObject.FindGameObjectWithTag("HUD").AddComponent<BoxCollider2D>();
        BoxCollider2D mapCollider = map.gameObject.AddComponent<BoxCollider2D>();
        Vector3 canvasColliderMinPos =canvasCollider.bounds.min; // check if min is right
        Vector3 mapColliderMinPos = mapCollider.bounds.min;
        mappingOffset = canvasColliderMinPos - mapColliderMinPos;
        */
        //Destroy(canvasCollider);
        //Destroy(mapCollider);

        for (int i = 0; i <itemArray.Length; i++)
        {
            map3DSceneTo2DMapPos = itemArray[i].transform.position * coefficientMapping;
           // Vector2 finalPos = (Vector2)map3DSceneTo2DMapPos + (Vector2)mappingOffset;
            RawImage image = Instantiate(blackImagePrefab, Camera.main.WorldToScreenPoint(itemArray[i].transform.position), Quaternion.identity);
           // image.rectTransform.anchoredPosition = finalPos;
         //   Debug.Log("Mapping Offset of this Image: " + mappingOffset);
          //  Debug.Log("2d Pos of a 3d Transform" + map3DSceneTo2DMapPos);
          //  Debug.Log("Black Image Prefab ** X: " + image.rectTransform.anchoredPosition.x + " Y: " + image.rectTransform.anchoredPosition.y + " supposed X: " + finalPos.x +
           //     " supposed Y: " + finalPos.y);
            image.transform.SetParent(mapRawImage.transform);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
