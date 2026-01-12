using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public string tag = "Enemies";
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && tag == "Player")
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerHealth>().TakeDamage(1);
            // DestroyObject();
        }

        if (collision.gameObject.CompareTag("Enemies") && tag == "Enemies")
        {
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<EnemyHealth>().TakeDamage(4);
            DestroyObject();
        }

        Destroy(gameObject);
    }

    public void SetTag(string newTag)
    {
        tag = newTag;
    }

    void DestroyObject()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.transform.parent = null;
            audioSource.Play();
            Destroy(audioSource.gameObject, audioSource.clip.length);
        }

        Destroy(gameObject);
    }
}