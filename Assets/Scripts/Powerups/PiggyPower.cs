using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiggyPower : MonoBehaviour
{
    private Player player;
    private GameObject waterWall;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        waterWall = GameObject.FindGameObjectWithTag("WaterWall");
    }

    public virtual void activate() { }

    protected void EnablePlayerJump()
    {
        player.AcquireJumpAbility();
    }

    protected void EnablePlayerSwim()
    {
        waterWall.SetActive(false);
    }
}
