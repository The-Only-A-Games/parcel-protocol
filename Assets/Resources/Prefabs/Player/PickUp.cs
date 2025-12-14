using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform parcelSpawnPoint;

    public bool collected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Parcel"))
        {
            GameObject parcel = other.gameObject;

            if (parcel != null)
            {
                Collider parcelCollider = parcel.GetComponent<Collider>();

                // if (parcelCollider != null) parcelCollider.isTrigger = false;
                parcel.transform.SetParent(parcelSpawnPoint);
                other.transform.SetParent(parcelSpawnPoint.transform, true);

                parcel.transform.localPosition = Vector3.zero;
                parcel.transform.localRotation = Quaternion.identity;

                collected = true;
            }
        }
    }

    public void SetCollected(bool value)
    {
        collected = value;
    }
}
