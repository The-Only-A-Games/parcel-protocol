using UnityEngine;

public class Target : MonoBehaviour
{
    public Camera camera;

    bool addOnce = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = FindFirstObjectByType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        LookTowardsMouse();
    }

    void LookTowardsMouse()
    {
        if (camera != null)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); // Creates a ray from the camera through the mouse position
            RaycastHit hit; // Contains the hits information

            // Peform the ray
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                transform.position = hit.point;

                if (!addOnce)
                {
                    Vector3 pos = transform.position;
                    pos.y += 0.5f;
                    transform.position = pos;
                }
            }
        }
    }
}
