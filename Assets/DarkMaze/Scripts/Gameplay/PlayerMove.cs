using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMove : MonoBehaviour
{

    //public navMesh
    Vector3 mousePosition;
    NavMeshAgent navMeshAgent;
   // Animator animator;
    Rigidbody rb;
    Camera ViewCamera;

    //its for animator
    bool isWalk;

    [SerializeField]
    Transform cube;

    Vector3 desPos;
    //ray and raycasthit for convertin touch screen position to world position
    Ray screenToWorldRay;
    RaycastHit moveToHit;
    RaycastHit rotateHit;

    // ray start from player to touch pos to check if there is a wall (and not going to pos)
    Ray playerToPosCheckWallRay;
    RaycastHit playerToPosCheckWallHit;

    //Using Navmesh Hit -- Azmayeshi
    NavMeshHit navMeshHit;
    Vector3 touchPos;

    [SerializeField]
    Light playerLight;

    [SerializeField]
    Vector3 direction;

    float inputHold = 0;
    float inputRotate = 0;
    bool lastFrameInputTouched;
    bool oneTimeLookAt = true;

    int WallAndPillarLayerMask;
    int RotateLayerMask;
    Touch touch;
    private bool rotating = false;
    [SerializeField] private float rotateRelation;
    [SerializeField] [Range(0f, 1f)] private float slerpRelation; 

    private void Awake()
    {
        Application.targetFrameRate = 60;
      
    }
    // Start is called before the first frame update
    void Start()
    {

        slerpRelation = 0.5f;

        WallAndPillarLayerMask |= (1 << LayerMask.NameToLayer("Wall"));
        WallAndPillarLayerMask |= (1 << LayerMask.NameToLayer("Pillar"));
        RotateLayerMask |= (1 << LayerMask.NameToLayer("Rotate"));



        Input.multiTouchEnabled = false;
        lastFrameInputTouched = false;
        isWalk = false;
        //animator = GetComponent<Animator>();

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
        if (navMeshAgent == null)
        {
            Debug.LogError("the nav mesh component is not attached to " + this.name);
        }
        else
        {
            //  SetDestination();
        }

        ViewCamera = Camera.main;
        direction = (Vector3.up * 3 + Vector3.back) * 5;
    }

    void Update()
    {
     

      if (isWalk && navMeshAgent.remainingDistance == 0)
        {
          isWalk = false;

        }

        if (Input.touchCount == 0)
        {
            rotating = false;
        }
        else if (Input.touchCount > 0)
        {
            
                 touch = Input.GetTouch(0);
                screenToWorldRay = Camera.main.ScreenPointToRay(touch.position);

               
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        {
                           // Vector3 playerPositionScreen = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
                            StartCoroutine(MoveToDestination());
                            break;
                        }

                     case TouchPhase.Stationary:
                         {
                            inputHold += Time.deltaTime;
                            inputRotate += Time.deltaTime;
                             rotating = true;

                        //if (oneTimeLookAt)
                        //{

                        //    LookAt();
                        //    oneTimeLookAt = false;
                        //}
                        break;
                         }


                 
                    case TouchPhase.Moved:
                        {
                            inputRotate += Time.deltaTime;
                            if (inputRotate > 0.15f)
                                {
                                    LookAt();                    
                                }                     
                        break;                        
                        }


                    case TouchPhase.Ended:
                        TouchEndPhase();
                        break;

                    case TouchPhase.Canceled:
                        TouchEndPhase();
                        break;

                }

            }

            //animator.SetBool("walk", isWalk);
    }

    private void LookAt()
    {

        if (Physics.Raycast(screenToWorldRay, out rotateHit, 100, RotateLayerMask))
        {
            
            cube.position = rotateHit.point;
            Vector3 targetPos = new Vector3(rotateHit.point.x, 0f, rotateHit.point.z);
            Vector3 playerPos = new Vector3(transform.position.x, 0f, transform.position.z);
            Vector3 relativePos = targetPos - playerPos;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
          
        }
    }

    private void TouchEndPhase()
    {
        inputHold = 0;
        inputRotate = 0;
        oneTimeLookAt = true;
        rotating = false;
    }

    private IEnumerator MoveToDestination()
    {

        
        if (Physics.Raycast(screenToWorldRay, out moveToHit, 100))
        {

            cube.position = moveToHit.point;
            Vector3 touchPointWorld = new Vector3(moveToHit.point.x, moveToHit.point.y, moveToHit.point.z);
            float distance = Vector3.Distance(touchPointWorld, this.transform.position);
            Vector3 direction = (touchPointWorld - this.transform.position);



            if (distance < playerLight.range && !checkWallRay(direction) && moveToHit.collider.tag == "Floor")
            {
                desPos = moveToHit.point;
                yield return new WaitForSeconds(0.2f);


                if(!rotating)
                {
                    navMeshAgent.SetDestination(desPos);
                    isWalk = true;

                }


            }


        }
    }
  

    bool checkWallRay(Vector3 rayDirection)
{
        
                playerToPosCheckWallRay = new Ray(gameObject.transform.position, rayDirection);
            
        if (Physics.Raycast(playerToPosCheckWallRay, out playerToPosCheckWallHit, rayDirection.magnitude, WallAndPillarLayerMask))
                {        
            //Debug.Log("Pos: " + playerToPosCheckWallHit.point);
              
            if (playerToPosCheckWallHit.collider.tag == "Wall" || playerToPosCheckWallHit.collider.tag == "Pillar")
                    {
                        return true;
                    
                    }
                }

                 return false;
    }
        private void LateUpdate()
        {
            if (ViewCamera != null) {

                RaycastHit hit;
                Debug.DrawLine(transform.position, transform.position + direction, Color.red);
                Vector3 desiredPosition = transform.position + direction;
                Vector3 smoothedPosition = Vector3.Lerp(ViewCamera.transform.position, desiredPosition, 0.5f);
                ViewCamera.transform.position = smoothedPosition;
                ViewCamera.transform.LookAt(transform.position);
            }
        }


}
