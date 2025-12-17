using UnityEngine;

public class EnemyShooter : MonoBehaviour
{

    public float shootingDistance = 25f;
    public float fireRate = 2f;

    private Transform player;
    private float nextFireTime;

    public Transform muzzle;
    public GameObject projectile;

    void Start()
    {
        projectile = Resources.Load<GameObject>("Prefabs/Projectiles/Projectile");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }


        Vector3 direction = (player.position - muzzle.position).normalized;
        Ray ray = new Ray(muzzle.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, shootingDistance))
        {
            Debug.DrawRay(muzzle.position, direction * shootingDistance, Color.red);

            if (hit.transform.CompareTag("Player") && Time.time >= nextFireTime)
            {
                muzzle.LookAt(hit.point);
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, muzzle.position, muzzle.rotation);
        bullet.GetComponent<ProjectileDamage>().SetTag("Player");
        bullet.GetComponent<MoveProjectile>().SetShoot(true);
    }
}
