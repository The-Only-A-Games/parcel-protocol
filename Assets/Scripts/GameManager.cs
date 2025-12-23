using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    [Header("Pointer Attributes")]
    public Collider mapCollider;
    public Transform targetPoint;
    public RectTransform arrowUI;
    private GameObject parcel;
    private GameObject delivery;

    [Header("Spawn Conditions")]
    public int enemySpawnLimit = 10;
    public float enemySpawnRate = 3f;
    private int currentEnemies = 0;
    private float nextEnemySpawnTime;

    [Header("Parcel Types")]
    public GameObject fragileParcel;
    public GameObject heavyParcel;
    public GameObject standardParcel;

    [Header("Delivery Point types")]
    public GameObject deliveryPoint;

    [Header("Enemy Types")]
    public GameObject packageReaper;
    public GameObject courierHunter;

    [Header("UI Attributes")]
    public int score = 0;

    [Header("UI GameObjects and Components")]
    public Canvas canvas;
    public GameMenu gameMenu;

    [Header("Spawn Validation")]
    public float navMeshSampleRadius = 2f;
    public int spawnAttempts = 25;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = FindAnyObjectByType<Canvas>();
        if (canvas != null) gameMenu = canvas.GetComponent<GameMenu>();

        fragileParcel = Resources.Load<GameObject>("Prefabs/Packages/Fragile/Fragile");
        heavyParcel = Resources.Load<GameObject>("Prefabs/Packages/Heavy/Heavy");
        standardParcel = Resources.Load<GameObject>("Prefabs/Packages/Standard/Standard");
        deliveryPoint = Resources.Load<GameObject>("Prefabs/DeliveryPoints/DeliveryPoint");
        courierHunter = Resources.Load<GameObject>("Prefabs/Enemies/CourierHunter/CourierHunter");
        packageReaper = Resources.Load<GameObject>("Prefabs/Enemies/PackageReaper/PackageReaper");

        Spawn(ChooseParcel());
    }

    void Update()
    {
        Pointer(targetPoint);

        if (canvas != null)
            canvas.GetComponent<GameMenu>().SetScore(score);

        int findParcel = GameObject.FindGameObjectsWithTag("Parcel").Length;
        int findDeliveryPoints = GameObject.FindGameObjectsWithTag("DeliveryPoint").Length;
        bool parcelCollected = player.GetComponent<PickUp>().collected;

        if (findParcel > 0 && findDeliveryPoints > 0)
        {
            if (parcelCollected)
            {
                targetPoint = delivery.transform;
                delivery.GetComponent<DeliveryPoint>().setDelivered(false);
            }
            else
            {
                targetPoint = parcel.transform;
            }
        }
        else if (findParcel <= 0)
        {
            if (delivery != null)
                Destroy(delivery);

            Spawn(ChooseParcel());
        }
        else if (findParcel > 0 && parcelCollected && findDeliveryPoints <= 0)
        {
            Spawn(deliveryPoint);
        }

        if (currentEnemies < enemySpawnLimit && Time.time >= nextEnemySpawnTime)
        {
            SpawnEnemy();
            nextEnemySpawnTime = Time.time + enemySpawnRate;
        }

        if (gameMenu.GetHealth() <= 0 || canvas.GetComponent<Trust>().GetTrust() <= 0)
        {
            gameMenu.GameOver();
        }
    }

    void Pointer(Transform target)
    {
        if (target != null)
        {
            Vector3 direction = target.position - player.transform.position;
            direction.y = 0;

            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            arrowUI.rotation = Quaternion.Euler(0, 0, -angle);
        }
    }


    bool TryGetValidSpawnPosition(out Vector3 validPos)
    {
        Bounds bounds = mapCollider.bounds;

        for (int i = 0; i < spawnAttempts; i++)
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                bounds.center.y,
                Random.Range(bounds.min.z, bounds.max.z)
            );

            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, navMeshSampleRadius, NavMesh.AllAreas))
            {
                validPos = hit.position + Vector3.up * 0.5f;
                return true;
            }
        }

        validPos = Vector3.zero;
        return false;
    }

    void Spawn(GameObject gameObject)
    {
        if (!TryGetValidSpawnPosition(out Vector3 spawnPos))
            return;

        if (gameObject.CompareTag("Parcel"))
        {
            parcel = Instantiate(gameObject, spawnPos, Quaternion.identity);
            targetPoint = parcel.transform;
        }

        if (gameObject.CompareTag("DeliveryPoint"))
        {
            delivery = Instantiate(gameObject, spawnPos, Quaternion.identity);
            delivery.GetComponent<DeliveryPoint>().setParcelName(parcel.name);
        }
    }

    void SpawnEnemy()
    {
        if (!TryGetValidSpawnPosition(out Vector3 spawnPos))
            return;

        GameObject enemyPrefab = ChooseEnemy();
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        currentEnemies++;

        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if (health != null)
            health.onDeath += OnEnemyDeath;
    }

    void OnEnemyDeath()
    {
        currentEnemies--;
    }

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

    GameObject ChooseEnemy()
    {
        int randomChoice = Random.Range(0, 2);

        return randomChoice switch
        {
            0 => courierHunter,
            1 => packageReaper,
            _ => courierHunter,
        };
    }
}
