using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleeing : State
{
    private AudioClip[] grunts;
    private AudioSource audioSource;

    public Fleeing(GameObject _ai, Animator _anim, Transform _player, GameObject[] _waypoints) : base(_ai, _anim, _player, _waypoints)
    {
        name = STATE.FLEEING;
        speed = 1.5f;
        grunts = ai.GetComponent<AI>().grunts;
        audioSource = ai.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        // play fleeing animation
        Debug.Log("DebugLog - Fleeing");

        int rng = Random.Range(0, 1);
        audioSource.PlayOneShot(grunts[rng]);

        ai.GetComponent<AI>().exclamAnim.SetTrigger("isFleeing");
        base.Enter();
    }

    public override void Update()
    {
        if (ai.GetComponent<AI>().isHeld == true)
        {
            nextState = new Held(ai, anim, player, waypoints);      // randomly decide when to roam
            stage = EVENT.EXIT;
        }

        RunAway();
        // determine what to do in this state
        if (GetDistanceTo(player.position) > escapeDistance)
        {
            nextState = new Idle(ai, anim, player, waypoints);      // randomly decide when to roam
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        // reset fleeing animation
        ai.GetComponent<AI>().exclamAnim.ResetTrigger("isFleeing");
        base.Exit();
    }
}
