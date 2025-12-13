using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform parcelSpawnPoint;

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
                parcel.transform.SetParent(parcelSpawnPoint);

                parcel.transform.localPosition = Vector3.zero;
                parcel.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
