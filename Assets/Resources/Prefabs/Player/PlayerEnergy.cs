using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    [Header("Attributes")]
    public float energy;
    public float maxEnergy = 10;
    public float increaseRate = 2f;
    public Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        energy = maxEnergy;
        slider.maxValue = maxEnergy;
        slider.value = energy;
    }

    // Update is called once per frame
    void Update()
    {
        // increaseEnergy();
    }

    public void increaseEnergy()
    {
        if (energy < maxEnergy)
        {
            energy += increaseRate * Time.deltaTime;
            slider.value = energy;
        }
    }


    public void decreaseEnergy(float amount)
    {
        if (energy >= 0)
        {
            energy -= amount;
            slider.value = energy;
        }
    }

    public float GetEnergy()
    {
        return energy;
    }
}
