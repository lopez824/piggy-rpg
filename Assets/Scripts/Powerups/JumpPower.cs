using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPower : PiggyPower
{
    public override void activate()
    {
        EnablePlayerJump();
    }

    private void OnTriggerEnter(Collider other)
    {
        activate();
    }
}
