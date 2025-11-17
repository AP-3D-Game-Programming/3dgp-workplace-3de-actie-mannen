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
    [SerializeField] float sightRange = 3f, attackRange = 2f;
    [SerializeField] float fieldOfView = 45f;
    private float actualSightRange;
    bool playerInSight, playerInAttack;
    public bool PlayerInSight => playerInSight;
    public bool PlayerInAttack => playerInAttack;


    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {

        if (PlayerInSightRange() || PlayerInAttackRange())
            Chase();
        else
            Patrolling();

    }

    private float CheckCrouch()
    {
        if (player.GetComponent<PlayerController>().isCrouching)
            actualSightRange = (float)(sightRange / 2);
        else
            actualSightRange = sightRange;
        return actualSightRange;
    }

    private bool PlayerInSightRange()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        
        if (distanceToPlayer <= CheckCrouch())
        {
            
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer <= 50f)
            {
                
                if (!Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, distanceToPlayer, groundLayer))
                {
                    playerInSight = true;
                    return true;
                }
            }
        }

        // Player not in sight
        playerInSight = false;
        return false;

        //playerInSight = Physics.CheckSphere(transform.position, actualSightRange, playerLayer);
        //if (playerInSight)
        //    return true;
        //return false;
    }

    private bool PlayerInAttackRange()
    {
        playerInAttack = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (playerInAttack)
            return true;
        return false;
    }
    private void Chase()
    {
        agent.speed = 10f;
        agent.angularSpeed = 360f;
        agent.SetDestination(player.transform.position);
    }

    private void Patrolling()
    {
        if (gameManager.gameIsActive && waypoints.Length != 0)
        {
            agent.speed = 3.5f;
            agent.angularSpeed = 120f;
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
        }
    }
}
