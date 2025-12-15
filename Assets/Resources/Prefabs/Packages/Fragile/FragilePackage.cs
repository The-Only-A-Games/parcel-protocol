using System;
using UnityEngine;

public class FragilePackage : ParcelEffect
{
    public override string Type => "fragile";


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void ApplyEffect()
    {
        GetComponent<PackageHealth>().TakeDamage(0.1f);

        if (GetComponent<PackageHealth>() == null) player.GetComponent<PickUp>().collected = false;
    }
}
