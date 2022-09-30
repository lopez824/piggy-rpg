using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public Idle(GameObject _ai, Animator _anim, Transform _player, GameObject[] _waypoints) : base(_ai, _anim, _player, _waypoints)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        // play idle animation
        //Debug.Log("DebugLog - Idle");
        anim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        // determine what to do in this state
        if (ai.GetComponent<AI>().isHeld == true)
        {
            nextState = new Held(ai, anim, player, waypoints);      // randomly decide when to roam
            stage = EVENT.EXIT;
        }

        if (CanSeePlayer() == true && ai.GetComponent<AI>().inPen == false)
        {
            nextState = new Fleeing(ai, anim, player, waypoints);      // randomly decide when to roam
            stage = EVENT.EXIT;
        }

        if (Random.Range(0,1000) < 10f)
        {
            nextState = new Roaming(ai, anim, player, waypoints);      // randomly decide when to roam
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        // reset idle animation
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}
