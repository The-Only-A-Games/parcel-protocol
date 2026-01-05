using UnityEngine;
using UnityEngine.AI;

public class PackageFollow : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public Transform parcel;
    public Animator animator;

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
        // Try to get parcel when respawned
        if (parcel == null)
            FindParcel();


        if (Time.time < nextUpdateTime)
            return;

        nextUpdateTime = Time.time + updateRate;

        Transform target = GetChaseTarget();
        if (target == null)
            return;

        Vector3 targetPos = target.position;

        // Keep enemy on ground (ignore target height)
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

    Transform GetChaseTarget()
    {
        // If parcel exists and is NOT carried, chase parcel
        if (parcel != null && !IsParcelCarried())
            return parcel;

        // Otherwise chase player
        return player;
    }

    void FindParcel()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Parcel");
        if (p != null)
            parcel = p.transform;
    }

    bool IsParcelCarried()
    {
        // Check if parcel is parented to player
        return parcel != null && parcel.parent == player;
    }
}
