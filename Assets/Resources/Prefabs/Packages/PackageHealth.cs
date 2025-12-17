using UnityEngine;
using UnityEngine.UI;

public class PackageHealth : MonoBehaviour
{
    [Header("Atrributes")]
    public float health;
    public float maxHealth = 10;
    public Slider slider;

    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (player != null)
            {
                player.GetComponent<PickUp>().collected = false;
            }

            Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        slider.value = health;

    }

    public float GetHealth()
    {
        return health;
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (other.GetComponent<ProjectileDamage>().tag == "Parcel")
            {
                TakeDamage(1);
                Destroy(other.gameObject);
            }
        }
    }
}
