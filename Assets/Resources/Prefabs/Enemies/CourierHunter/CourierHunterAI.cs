using UnityEngine;
using UnityEngine.AI;

public class CourierHunterAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public Animator animator;

    [Header("Settings")]
    public float updateRate = 0.2f;
    public float stopDistance = 1.5f;

    private float nextUpdateTime;

    void Awake()
    {
        if (!agent)
            agent = GetComponent<NavMeshAgent>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj)
            player = playerObj.transform;

        agent.stoppingDistance = stopDistance;
    }

    void Update()
    {
        if (player == null)
            return;

        // Throttle path updates
        if (Time.time < nextUpdateTime)
            return;

        nextUpdateTime = Time.time + updateRate;

        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;

        float distance = Vector3.Distance(transform.position, player.position);

        // Move only if outside stopping distance
        if (distance > stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(targetPos);
            animator.SetBool("chase", true);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("chase", false);
        }
    }
}
