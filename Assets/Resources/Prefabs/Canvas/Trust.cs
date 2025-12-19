using UnityEngine;
using UnityEngine.UI;

public class Trust : MonoBehaviour
{
    [Header("Atrributes")]
    public int trust;
    public int maxTrust = 10;
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trust = maxTrust;
        slider.maxValue = maxTrust;
        slider.value = trust;
    }

    public void LooseTrust(int amount)
    {
        trust -= amount;
        slider.value = trust;

    }

    public int GetTrust()
    {
        return trust;
    }
}
