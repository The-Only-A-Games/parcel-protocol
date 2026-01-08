using UnityEngine;

public class MusicStartPoint : MonoBehaviour
{
    public AudioSource audioSource;
    public float time = 10f;

    void Start()
    {
        audioSource.time = time;
        audioSource.Play();
    }
}
