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
        if (collision.gameObject.tag == "Player")
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerHealth>().TakeDamage(10);
        }

        if (collision.gameObject.tag == "Enemies")
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
