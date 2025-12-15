using UnityEngine;

public abstract class ParcelEffect : MonoBehaviour
{
    protected GameObject player;
    public GameObject Player => player;
    public abstract string Type { get; }
    public abstract void ApplyEffect();
}
