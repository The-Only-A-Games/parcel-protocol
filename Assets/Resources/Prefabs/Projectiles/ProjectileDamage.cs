using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public string tag = "Enemies";

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
        }

        if (collision.gameObject.CompareTag("Enemies") && tag == "Enemies")
        {
            Debug.Log("Enemy Hit");
        }

        Destroy(gameObject);
    }

    public void SetTag(string newTag)
    {
        tag = newTag;
    }
}
