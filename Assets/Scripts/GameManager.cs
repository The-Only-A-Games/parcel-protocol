using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;


    [Header("Pointer Attributes")]
    public Collider mapCollider;
    public Transform targetPoint;
    public RectTransform arrowUI;
    private GameObject parcel;
    private GameObject delivery;



    [Header("Parcel Types")]
    public GameObject fragileParcel;
    public GameObject heavyParcel;
    public GameObject standardParcel;

    [Header("Delivery Point types")]
    public GameObject deliveryPoint;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        // Getting game objects
        fragileParcel = Resources.Load<GameObject>("Prefabs/Packages/Fragile/Fragile");
        heavyParcel = Resources.Load<GameObject>("Prefabs/Packages/Heavy/Heavy");
        standardParcel = Resources.Load<GameObject>("Prefabs/Packages/Standard/Standard");
        deliveryPoint = Resources.Load<GameObject>("Prefabs/DeliveryPoints/DeliveryPoint");

        // Initial spawn
        Spawn(ChooseParcel());
    }

    // Update is called once per frame
    void Update()
    {
        // Points at Target Point
        Pointer(targetPoint);

        int findParcel = GameObject.FindGameObjectsWithTag("Parcel").Length;
        int findDeliveryPoints = GameObject.FindGameObjectsWithTag("DeliveryPoint").Length;
        bool parcelCollected = player.GetComponent<PickUp>().collected;

        // If both parcel and delivery points exist in the scene
        if (findParcel > 0 && findDeliveryPoints > 0)
        {
            if (parcelCollected)
            {
                targetPoint = delivery.transform;
            }
            else
            {
                targetPoint = parcel.transform;
            }

        }
        else if (findParcel <= 0) // If the parcel does not exist in the scene
        {
            if (delivery != null)
            {
                Destroy(delivery);
            }
            Spawn(ChooseParcel());
        }
        else if (findParcel > 0 && parcelCollected && findDeliveryPoints <= 0) // If parcel is collected and delivery point does not exist spawn the delivery point
        {
            Spawn(deliveryPoint);
        }



    }

    // Points to parkages or delivery points
    void Pointer(Transform target)
    {
        if (target != null)
        {
            Vector3 direction = target.position - player.transform.position;

            // Ignore vertical difference
            direction.y = 0;

            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            arrowUI.rotation = Quaternion.Euler(0, 0, -angle);
        }
    }

    // Gets a random position on the map
    Vector3 SpawnPosition()
    {
        Bounds bounds = mapCollider.bounds;

        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.max.y + 10f,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    // Spawns gameobjects and sets their targetPoint
    void Spawn(GameObject gameObject)
    {
        if (Physics.Raycast(SpawnPosition(), Vector3.down, out RaycastHit hit))
        {
            Vector3 spawnPos = hit.point + Vector3.up * 0.5f; // OFFSET

            if (gameObject.CompareTag("Parcel"))
            {
                parcel = Instantiate(gameObject, spawnPos, Quaternion.identity);
                // parcel.name.Replace("(Clone)", "").Trim();
            }

            if (gameObject.CompareTag("DeliveryPoint"))
            {
                delivery = Instantiate(gameObject, spawnPos, Quaternion.identity);
                delivery.GetComponent<DeliveryPoint>().setParcelName(parcel.name);
                // delivery.name.Replace("(Clone)", "").Trim();
            }
            targetPoint = parcel.transform;
        }
    }


    // Randomly chooses wich package to spawn
    GameObject ChooseParcel()
    {
        int randomChoice = Random.Range(0, 3);

        return randomChoice switch
        {
            0 => standardParcel,
            1 => fragileParcel,
            2 => heavyParcel,
            _ => standardParcel,
        };
    }
}
