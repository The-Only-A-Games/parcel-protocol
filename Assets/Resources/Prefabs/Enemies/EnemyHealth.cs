using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Atrributes")]
    public float health;
    public float maxHealth = 10;
    public Slider slider;
    public Action onDeath;
    public GameManager gameManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        slider.value -= amount;

    }

    public float GetHealth()
    {
        return health;
    }

    void Die()
    {
        gameManager.score++;
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
