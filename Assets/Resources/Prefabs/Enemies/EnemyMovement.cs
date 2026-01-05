using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Player Attributes")]
    public float speed = 10;
    public float gravity = 9.8f;
    public float detectionRange = 10f;
    public string tag = "Player";

    [Header("Components")]
    public CharacterController ch;
    public NavMeshAgent agent;
    public Animator animator;


    [Header("Target")]
    public Transform target;

    private Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get components
        ch = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag(tag).GetComponent<Transform>();

        agent.updateRotation = true;
        agent.updatePosition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPos = new Vector2(target.position.x, target.position.z);

        float distance = Vector2.Distance(enemyPos, playerPos);
        Debug.Log($"Chase ${gameObject.name}");
        agent.destination = target.position;
        agent.SetDestination(target.position);

    }

    // When player hit enemy they die
    public void Die()
    {
        Destroy(gameObject);
    }
}
