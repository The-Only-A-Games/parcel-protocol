using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 10f;
    public float distanceLimit = 5f;
    public float travelRate = 0.5f;
    private float distance = 0;
    private bool shoot = false;


    [Header("Gameobjects & Components")]
    public Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        distance += Time.deltaTime * travelRate;

        if (distance >= distanceLimit) Destroy(gameObject);

        // Move in the direction its facing
        if (shoot) rb.linearVelocity = transform.forward * speed;
    }

    public void SetShoot(bool canShoot)
    {
        shoot = canShoot;
    }
}
