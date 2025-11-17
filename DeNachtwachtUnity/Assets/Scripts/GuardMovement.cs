using UnityEngine;
using UnityEngine.AI;

public class GuardMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    private NavMeshAgent agent;
    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = agent.velocity.magnitude;

        animator.SetFloat("Speed", speed);
    }
}
