using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PickUp))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float walkSpeed = 10;
    public float runSpeed = 20;
    public float speed;
    public float acceleration = 10f;
    private float speedEffect = 0;
    public float gravity = 9.8f;
    private float yVelocity;

    [Header("Dashing")]
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCoolDown = 1f;
    private bool canDash = true;


    [Header("Components & Gameobjects")]
    public CharacterController ch;
    public Camera camera;
    public PlayerHealth playerHealth;
    public PlayerEnergy playerEnergy;
    public Animator animator;
    public bool energyInUse = false;
    private GameObject parcel;

    private Vector3 targetLookPosition;
    private Vector3 targetVelocity;
    private Vector3 velocity;
    private float energy;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ch = GetComponent<CharacterController>();
        camera = FindFirstObjectByType<Camera>();
        speed = walkSpeed;

        playerEnergy = GetComponent<PlayerEnergy>();
        playerHealth = GetComponent<PlayerHealth>();
        energy = playerEnergy.GetEnergy();

        parcel = GameObject.FindGameObjectWithTag("Parcel");
    }

    // Update is called once per frame
    void Update()
    {
        // Reset downward velocity when grounded
        if (ch.isGrounded && yVelocity < 0)
            yVelocity = -2f;


        // Only try to find the parcel when its null
        if (parcel == null)
        {
            parcel = GameObject.FindGameObjectWithTag("Parcel");
        }

        energy = playerEnergy.GetEnergy();


        var lookPoint = LookTowardsMouse(out targetLookPosition);
        // Looks in the direction of the mouse point or position
        if (lookPoint)
        {
            targetLookPosition.y = transform.position.y;
            transform.LookAt(targetLookPosition);
        }


        Vector2 inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 direction = new Vector3(inputs.x, 0f, inputs.y);


        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }


        // DASHING
        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            // Can only perfom dash if energy is enough
            if (playerEnergy.GetEnergy() >= 5)
            {
                playerEnergy.decreaseEnergy(5);
                Vector3 dashDirection = direction.magnitude >= 0.1f ? direction : transform.forward;
                StartCoroutine(Dash(dashDirection));
            }
        }


        // SWITCHING BETWEEN RUNNING AND WALKING
        if (Input.GetKey(KeyCode.Space)) // holding
        {
            if (energy > 0)
            {
                // can run
                speed = runSpeed;
                energyInUse = true;
                // animator.SetBool("running", true);
            }
            else
            {
                // energy drained WHILE holding
                speed = walkSpeed;
                energyInUse = false;
                animator.SetBool("running", false);
            }
        }
        else // not holding Space
        {
            speed = walkSpeed;
            energyInUse = false;
            animator.SetBool("running", false);
        }

        // Applying gravity
        yVelocity -= gravity * Time.deltaTime;
        direction.y = yVelocity;

        float newSpeed = speed - speedEffect;
        targetVelocity = direction * newSpeed;

        // Smoothing out the direction change
        // velocity = Vector3.MoveTowards(velocity, targetVelocity, acceleration * Time.deltaTime);
        velocity = targetVelocity;

        // Controlling the animation
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
        animator.SetFloat("speed", horizontalVelocity.magnitude);

        animator.SetBool("running", speed == runSpeed && horizontalVelocity.magnitude > 0.1f);

        ch.Move(velocity * Time.deltaTime);
        // playerEnergy.increaseEnergy();

        if (energyInUse)
        {
            playerEnergy.decreaseEnergy(0.1f);

            // If affect can be perfomed
            if (parcel != null)
            {
                bool performEffect = parcel.GetComponent<ParcelEffect>().Type == "fragile" && GetComponent<PickUp>().collected;

                if (performEffect) parcel.GetComponent<ParcelEffect>().ApplyEffect();
            }
        }
        else
        {
            playerEnergy.increaseEnergy();
        }
    }

    bool LookTowardsMouse(out Vector3 point)
    {
        if (camera != null)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); // Creates a ray from the camera through the mouse position
            RaycastHit hit; // Contains the hits information

            // Peform the ray
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                point = hit.point;
                return true;
            }
        }

        point = default;
        return false;
    }


    private IEnumerator Dash(Vector3 direction)
    {
        canDash = false;
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            ch.Move(direction * dashSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }


    // Speed effect, used when heavy package is picked up
    public void setSpeedEffect(float amount)
    {
        speedEffect = amount;
    }
}
