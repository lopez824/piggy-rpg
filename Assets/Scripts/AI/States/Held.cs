using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Held : State
{
    private AudioClip oink;
    private AudioSource audioSource;

    public Held(GameObject _ai, Animator _anim, Transform _player, GameObject[] _waypoints) : base(_ai, _anim, _player, _waypoints)
    {
        name = STATE.HELD;
        oink = ai.GetComponent<AI>().oinks[3];
        audioSource = ai.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        // play held animation
        Debug.Log("DebugLog - Held");

        audioSource.PlayOneShot(oink);
        ai.GetComponent<AI>().onLand = false;
        base.Enter();
    }

    public override void Update()
    {
        // determine what to do in this state
        if (ai.GetComponent<AI>().isHeld == false)
        {
            nextState = new Airborne(ai, anim, player, waypoints);      // randomly decide when to roam
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        // reset held animation
        // anim.ResetTrigger();
        base.Exit();
    }
}
