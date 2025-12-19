using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Gameobjects & Components")]
    public GameObject projectile;
    public Transform spawnPoint;

    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.canvas.GetComponent<GameMenu>().IsPaused) return;

        shoot();
    }

    private void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(projectile, spawnPoint.position, spawnPoint.rotation);
            bullet.GetComponent<ProjectileDamage>().SetTag("Enemies");
            bullet.GetComponent<MoveProjectile>().SetShoot(true);
        }
    }
}
