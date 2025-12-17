using UnityEngine;
using UnityEngine.AI;

public class CourierHunterAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;

    [Header("Settings")]
    public float updateRate = 0.2f;   // How often to update destination
    public float stopDistance = 1.5f;

    private float nextUpdateTime;

    void Awake()
    {
        if (!agent)
            agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (Time.time < nextUpdateTime)
            return;

        nextUpdateTime = Time.time + updateRate;

        if (player == null)
            return;

        Vector3 targetPos = player.position;

        // Keep enemy on ground (ignore target height)
        targetPos.y = transform.position.y;

        agent.stoppingDistance = stopDistance;
        agent.SetDestination(targetPos);
    }
}
