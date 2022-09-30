using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : State
{
    private AudioClip[] oinks;
    private AudioSource audioSource;

    public Roaming(GameObject _ai, Animator _anim, Transform _player, GameObject[] _waypoints) : base(_ai, _anim, _player, _waypoints)
    {
        name = STATE.ROAMING;
        speed = 0.75f;
        oinks = ai.GetComponent<AI>().oinks;
        audioSource = ai.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        // play roaming animation
        anim.SetTrigger("isWalking");
        //Debug.Log("DebugLog - Roaming");
        if (ai.GetComponent<AI>().inPen == true)
            currentPoint = Random.Range(0, ai.GetComponent<AI>().penWaypoints.Length - 1);
        else
            currentPoint = Random.Range(0, waypoints.Length - 1);

        base.Enter();
    }

    public override void Update()
    {
        if (ai.GetComponent<AI>().isHeld == true)
        {
            nextState = new Held(ai, anim, player, waypoints);      // randomly decide when to roam
            stage = EVENT.EXIT;
        }

        if (ai.GetComponent<AI>().inPen == true)
            destination = ai.GetComponent<AI>().penWaypoints[currentPoint].transform.localPosition;
        else
        {
            if (CanSeePlayer() == true)
            {
                nextState = new Fleeing(ai, anim, player, waypoints);      // randomly decide when to roam
                stage = EVENT.EXIT;
            }
            destination = waypoints[currentPoint].transform.position;
        }

        MoveTo(destination);

        if (GetDistanceTo(destination) < 1)
        {
            // move to next point
            if (Random.Range(0, 100) < 20f)
            {
                int rng = Random.Range(0, 2);
                audioSource.PlayOneShot(oinks[rng]);

                nextState = new Idle(ai, anim, player, waypoints);      // randomly decide when to roam
                stage = EVENT.EXIT;
            }

            if (ai.GetComponent<AI>().inPen == true)
                currentPoint = Random.Range(0, ai.GetComponent<AI>().penWaypoints.Length - 1);
            else
            {
                if (currentPoint == 0 || currentPoint == 1)
                    currentPoint = Random.Range(2, 6);
                else if (currentPoint == waypoints.Length - 1 || currentPoint == waypoints.Length - 2)
                    currentPoint = Random.Range(10, 14);
                else
                    currentPoint = Random.Range(0, waypoints.Length - 1);
            }
        }
    }

    public override void Exit()
    {
        // reset roaming animation
        anim.ResetTrigger("isWalking");
        base.Exit();
    }
}
