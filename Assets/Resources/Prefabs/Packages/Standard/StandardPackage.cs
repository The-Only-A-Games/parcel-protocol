using UnityEngine;

public class StandardPackage : ParcelEffect
{
    public override string Type => "standard";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void ApplyEffect()
    {

    }
}
