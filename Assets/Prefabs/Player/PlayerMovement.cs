using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 10f;
    public float acceleration = 10f;

    [Header("Dashing")]
    public float dashSpeed = 20f;
    public float dashTime = 0.2f;
    public float dashCoolDown = 1f;
    private bool canDash = true;


    [Header("Components & Gameobjects")]
    public CharacterController ch;
    public Camera camera;

    private Vector3 targetLookPosition;
    private Vector3 targetVelocity;
    private Vector3 velocity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ch = GetComponent<CharacterController>();
        // prefer the main camera, fall back to any Camera in the scene
        camera = Camera.main;
        if (camera == null)
        {
            camera = FindFirstObjectByType<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var lookPoint = LookTowardsMouse(out targetLookPosition);

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


        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {

            Vector3 dashDirection = direction.magnitude >= 0.1f ? direction : transform.forward;
            StartCoroutine(Dash(dashDirection));
        }

        targetVelocity = direction * speed;

        // Smoothing out the direction change
        velocity = Vector3.MoveTowards(velocity, targetVelocity, acceleration * Time.deltaTime);

        ch.Move(velocity * Time.deltaTime);
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
            ch.Move(direction * speed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
}
