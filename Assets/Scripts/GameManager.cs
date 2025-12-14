using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject parcel;
    public Collider mapCollider;

    public Transform player;
    public Transform packageTarget;
    public RectTransform arrowUI;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn();
        packageTarget = parcel.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = packageTarget.position - player.position;

        // Ignore vertical difference
        direction.y = 0;

        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        arrowUI.rotation = Quaternion.Euler(0, 0, -angle);
    }

    void Spawn()
    {
        Bounds bounds = mapCollider.bounds;

        Vector3 randomPos = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.max.y + 10f,
            Random.Range(bounds.min.z, bounds.max.z)
        );

        if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit))
        {
            Vector3 spawnPos = hit.point + Vector3.up * 0.5f; // OFFSET

            parcel = Instantiate(parcel, spawnPos, Quaternion.identity);
        }
    }
}
