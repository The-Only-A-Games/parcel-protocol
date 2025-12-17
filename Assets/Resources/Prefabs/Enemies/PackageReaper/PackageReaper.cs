using UnityEngine;

public class PackageReaper : MonoBehaviour
{
    public float shootingDistance = 25f;
    public float fireRate = 2f;

    private Transform parcel;
    private float nextFireTime;

    public Transform muzzle;
    public GameObject projectile;

    void Start()
    {
        projectile = Resources.Load<GameObject>("Prefabs/Projectiles/Projectile");
    }

    void Update()
    {
        if (parcel == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Parcel");
            if (p != null)
                parcel = p.transform;
            return;
        }

        // Rotate enemy toward parcel
        transform.LookAt(parcel.position);

        Vector3 direction = (parcel.position - muzzle.position).normalized;
        Ray ray = new Ray(muzzle.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, shootingDistance))
        {
            Debug.DrawRay(muzzle.position, direction * shootingDistance, Color.red);

            if (hit.transform.CompareTag("Parcel") && Time.time >= nextFireTime)
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
        bullet.GetComponent<ProjectileDamage>().SetTag("Parcel");
        bullet.GetComponent<MoveProjectile>().SetShoot(true);
    }
}
