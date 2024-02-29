using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public GameObject[] waypoints = new GameObject[4];
    public GameObject waypointTargetObject;
    public Vector3 waypointTargetPosition;
    public float moveSpeed = 1; 
    NavMeshAgent agent;
    public enum EnemyAIState
    {
        Patrol,
        Chase,
        Attack
    }

    public EnemyAIState currentState;
    public GameObject player;

    public float distanceToPlayer;
    public void ChangeState(EnemyAIState desiredState)
    {
        currentState = desiredState;
    }
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        currentState = EnemyAIState.Patrol;
        player = GameObject.FindGameObjectWithTag("Player");
        waypointTargetObject = waypoints[4];
        waypointTargetPosition = waypointTargetObject.transform.position;
        agent.destination = waypointTargetPosition;

    }

    public void RandomWaypointTarget()
    {
        int i = Random.Range(0, 4);
        waypointTargetObject = waypoints[i];
        waypointTargetPosition = waypointTargetObject.transform.position;
        agent.destination = waypointTargetPosition;
        Debug.Log("Now moving to waypoint " + i);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeState(EnemyAIState.Chase);
            Debug.Log("Switched state to Chase");
        }

        if (other.gameObject == waypointTargetObject)
        {
            RandomWaypointTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeState(EnemyAIState.Patrol);
            RandomWaypointTarget();
            Debug.Log("Switched state to Patrol");

        }
    }

    public void Patrol()
    {
        moveSpeed = 1f;
        agent.destination = waypointTargetPosition;
    }

    public void Chase()
    {
        waypointTargetObject = player;
        waypointTargetPosition = player.transform.position;
        moveSpeed = 2f;
        agent.destination = waypointTargetPosition;
        
    }

    public void CheckDistance()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (distanceToPlayer <= 1f)
        {
            ChangeState(EnemyAIState.Attack);
            Debug.Log("Changed state to Attack");
        }
    }
    public void AttackPlayer()
    {
        Debug.Log("Attacked player");
    }
    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        
        switch (currentState)
        {
            case EnemyAIState.Patrol:
                Patrol();
                break;
            case EnemyAIState.Chase:
                Chase();
                break;
            case EnemyAIState.Attack:
                AttackPlayer();
                break;
        }
    }
}

