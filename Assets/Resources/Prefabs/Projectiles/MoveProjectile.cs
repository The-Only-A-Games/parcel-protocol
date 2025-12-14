using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 10f;


    [Header("Gameobjects & Components")]
    public Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // Move in the direction its facing
        rb.AddForce(transform.forward * speed, ForceMode.Acceleration);
    }
}
