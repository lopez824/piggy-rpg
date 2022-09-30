using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airborne : State
{
    private AudioClip[] squeals;
    private AudioSource audioSource;

    public Airborne(GameObject _ai, Animator _anim, Transform _player, GameObject[] _waypoints) : base(_ai, _anim, _player, _waypoints)
    {
        name = STATE.THROWN;
        squeals = ai.GetComponent<AI>().squeals;
        audioSource = ai.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        // play thrown animation
        Debug.Log("DebugLog - Airborne");
        anim.Play("Airborne State");

        int rng = Random.Range(0, 1);
        audioSource.PlayOneShot(squeals[rng]);
        base.Enter();
    }

    public override void Update()
    {
        // determine what to do in this state
        if (ai.GetComponent<AI>().onLand == true)
        {
            nextState = new Idle(ai, anim, player, waypoints);      // randomly decide when to roam
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        // reset thrown animation
        anim.SetTrigger("isLanding");
        base.Exit();
    }
}
