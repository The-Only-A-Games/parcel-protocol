using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Gameobjects & Components")]
    public GameObject projectile;
    public Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shoot();
    }

    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
