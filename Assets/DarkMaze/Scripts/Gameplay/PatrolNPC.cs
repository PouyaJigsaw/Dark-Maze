using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolNPC : MonoBehaviour
{

    [SerializeField]
    bool patrolWaiting;

    [SerializeField]
    float totalWaitTime = 3f;

    [SerializeField]
    float switchProbability = 0.2f;

    [SerializeField]
    List<PatrolWaypoint> patrolPoints;


    NavMeshAgent navMeshAgent;
    int currentPatrolIndex;
    bool traveling;
    bool waiting;
    bool patrolForward;
    float waitTimer;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        
        if(navMeshAgent == null)
        {
            Debug.LogError("The nav mesh component isnt attached to " + this.name);

        }
        else
        {
            if(patrolPoints != null && patrolPoints.Count >= 2)
            {
                currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient Patrol Points for basic patrolling behaviour.");

            }
        }
    }

    private void SetDestination()
    {
      if(patrolPoints != null)
        {
            Vector3 targetVector = patrolPoints[currentPatrolIndex]
                .transform.position;
            navMeshAgent.SetDestination(targetVector);
            traveling = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (traveling && navMeshAgent.remainingDistance <= 1.0f)
        {
            traveling = false;


            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        if(waiting)
        {
            waitTimer += Time.deltaTime;
            
            if (waitTimer >= totalWaitTime)
            {
                waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void ChangePatrolPoint()
    {
       if(UnityEngine.Random.Range(0f, 1f) <= switchProbability)
        {
            patrolForward = !patrolForward;
        }

       if(patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
        else
        {
            if(--currentPatrolIndex < 0)
            {
                currentPatrolIndex = patrolPoints.Count - 1;
            }
        }
    }
}
