using UnityEngine;

public class HeavyPackage : ParcelEffect
{
    public override string Type => "heavy";

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }



    // Update is called once per frame
    void Update()
    {
        ApplyEffect();
    }


    // Slows down player when picked up
    public override void ApplyEffect()
    {
        if (player != null)
        {
            if (player.GetComponent<PickUp>().collected)
            {
                player.GetComponent<PlayerMovement>().setSpeedEffect(10);
            }
            else
            {
                player.GetComponent<PlayerMovement>().setSpeedEffect(0);
            }
        }
    }
}
