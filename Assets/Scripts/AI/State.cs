using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public enum STATE { ROAMING, IDLE, THROWN, HELD, FLEEING};
    public enum EVENT { ENTER, UPDATE, EXIT};

    public STATE name;
    protected EVENT stage;
    protected GameObject ai;
    protected Animator anim;
    protected Transform player;
    protected GameObject[] waypoints;
    protected State nextState;

    public Vector3 destination;
    public int currentPoint = 0;
    public float escapeDistance = 3f;
    public float visualDistance = 2f;
    public float visualAngle = 30f;
    public float speed = 1f;
    public float turnSpeed = 5f;

    public State(GameObject _ai, Animator _anim, Transform _player, GameObject[] _waypoints)
    {
        ai = _ai;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
        waypoints = _waypoints;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public void MoveTo(Vector3 location)
    {
        RotateTo(location);
        ai.transform.Translate(0,0, speed * Time.deltaTime);
    }

    public void RotateTo(Vector3 location)
    {
        Vector3 targetDirection = location - ai.transform.position;
        Vector3 currentDirection = ai.transform.forward;        // we want to use our forward direction

        Debug.DrawRay(ai.transform.position, currentDirection, Color.red, 2f);     // useful for visualizing vectors
        Debug.DrawRay(ai.transform.position, targetDirection, Color.blue, 2f);

        Quaternion lookAtPoint = Quaternion.LookRotation(targetDirection);
        ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, lookAtPoint, Time.deltaTime * turnSpeed);
    }

    public float GetDistanceTo(Vector3 location)
    {
        float distance = Vector3.Distance(location, ai.transform.position);
        return distance;
    }

    public void RunAway()
    {
        Vector3 playerDirection = player.transform.position - ai.transform.position;
        float lookAhead = playerDirection.magnitude / (speed + 2);        // need to add player speed
        Vector3 fleeLocation = player.transform.position + player.transform.forward * lookAhead;
        Vector3 fleeDirection = fleeLocation - ai.transform.position;
        MoveTo(ai.transform.position - fleeDirection);
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - ai.transform.position;
        float angle = Vector3.Angle(direction, ai.transform.forward);
        if (direction.magnitude < visualDistance && angle < visualAngle)
            return true;
        return false;
    }

    public bool HasEscaped()
    {
        Vector3 direction = player.position - ai.transform.position;
        if (direction.magnitude > escapeDistance)
            return true;
        return false;
    }

    public State Process()
    {
        if (stage == EVENT.ENTER)
            Enter();
        if (stage == EVENT.UPDATE)
            Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
}
