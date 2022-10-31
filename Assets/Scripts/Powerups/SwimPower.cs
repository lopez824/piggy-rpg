using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimPower : PiggyPower
{
    public override void activate()
    {
        EnablePlayerSwim();
    }

    private void OnTriggerEnter(Collider other)
    {
        activate();
    }
}
