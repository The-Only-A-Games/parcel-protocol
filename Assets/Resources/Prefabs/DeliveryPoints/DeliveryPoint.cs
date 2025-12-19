using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    public float deliveryTime = 2;
    private float elapsTime = 0f;
    private bool delivered = false;


    public Transform parcelPosition;
    public Transform parcel;
    public GameManager gameManager;
    private string parcelName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delivered)
        {
            elapsTime += Time.deltaTime;
        }
        else
        {
            elapsTime = 0f;
        }

        // Deliver parcel when delivery is reached
        if (elapsTime >= deliveryTime)
        {
            DeliverScore();
            Destroy(gameObject);
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parcel = other.transform.Find("ParcelSpawn/" + parcelName);

            // Setting pickup to false
            other.gameObject.GetComponent<PickUp>().SetCollected(false);

            Debug.Log(parcel);
            if (parcel != null)
            {
                // parcel.transform.SetParent(parcelPosition);

                // parcel.transform.localPosition = Vector3.zero;
                // parcel.transform.localRotation = Quaternion.identity;

                parcel.SetParent(parcelPosition);
                parcel.localPosition = Vector3.zero;
                parcel.localRotation = Quaternion.identity;
                delivered = true;
            }
        }
    }

    public void setParcelName(string name)
    {
        parcelName = name;
    }

    public void setDelivered(bool value)
    {
        delivered = value;
    }

    void DeliverScore()
    {
        if (parcelName == "Standard(Clone)") gameManager.score += 5;

        if (parcelName == "Fragile(Clone)") gameManager.score += 10;

        if (parcelName == "Heavy(Clone)") gameManager.score += 15;
    }
}
