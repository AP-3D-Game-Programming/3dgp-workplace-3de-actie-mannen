using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    [SerializeField] LayerMask groundLayer, playerLayer;

    public Transform[] waypoints;
    private int _currentWaypointIndex;
    [SerializeField] float _speed = 2f;

    [SerializeField] float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;

    //State change
    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttackRange;

    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSight && !playerInAttackRange) Patrolling();
        if (playerInSight && !playerInAttackRange) Chase();
        //if (playerInSight && playerInAttackRange) Attack();

    }

    private void Chase()
    {
        agent.SetDestination(player.transform.position);
    }

    //private void Attack()

    private void Patrolling()
    {
        if (gameManager.gameIsActive)
        {
            if (_waiting)
            {
                _waitCounter += Time.deltaTime;
                if (_waitCounter < _waitTime)
                    return;

                _waiting = false;
                agent.SetDestination(waypoints[_currentWaypointIndex].position);
            }
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                _waiting = true;
                _waitCounter = 0f;

                _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            }
///Redundant code based on just waypoints and not on MavMesh
            //Transform wp = waypoints[_currentWaypointIndex];
            //if (Vector3.Distance(transform.position, wp.position) < 0.01f)
            //{
            //    transform.position = wp.position;
            //    _waitCounter = 0f;
            //    _waiting = true;

            //    _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            //}
            //else
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, wp.position, _speed * Time.deltaTime);
            //    transform.LookAt(wp.position);
            //}
        }
    }
}
