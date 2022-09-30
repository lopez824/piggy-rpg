using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent agent;
    private float escapeDistance = 5f;
    private float visualDistance = 5f;
    private float visualAngle = 30f;
    private bool isResting = true;
    private bool isFleeing = false;

    public Material fleeMat;
    public Material wanderMat;
    public Material originalMat;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        Idle();
    }

    public void Idle()
    {
        //gameObject.GetComponent<MeshRenderer>().material = originalMat;
        isResting = true;
        StartCoroutine(Resting());
    }

    private IEnumerator Resting()
    {
        //Debug.Log("DebugLog - Resting");
        float randomDuration = Random.Range(2, 5);
        yield return new WaitForSeconds(randomDuration);
        if (agent.hasPath == true)
            if (agent.remainingDistance < 1)
                agent.SetDestination(transform.position);
        isResting = false;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        if (direction.magnitude < visualDistance && angle < visualAngle)
            return true;
        return false;
    }

    public bool HasEscaped()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.magnitude > escapeDistance)
            return true;
        return false;
    }

    public void Seek(Vector3 location)
    {
        //gameObject.GetComponent<MeshRenderer>().material = wanderMat;
        //Debug.Log("DebugLog - Seeking: " + location);
        agent.SetDestination(location);
    }

    public void Flee(Vector3 location)
    {
        //Debug.Log("DebugLog - Fleeing");
        Vector3 fleeDirection = location - transform.position;
        agent.SetDestination(transform.position - fleeDirection);       // reflects direction to player
    }

    public void Evade()
    {
        //gameObject.GetComponent<MeshRenderer>().material = fleeMat;
        Vector3 playerDirection = player.transform.position - transform.position;
        float lookAhead = playerDirection.magnitude / (agent.speed + 2);        // need to add player speed
        Flee(player.transform.position + player.transform.forward * lookAhead);

        if (HasEscaped() == true)
        {
            isFleeing = false;
        }
    }

    public void Wander()
    {
        float randomX = Random.Range(-5, 5);
        float randomZ = Random.Range(-5, 5);
        Vector3 randomLocation = new Vector3(randomX, 0, randomZ);
        Seek(randomLocation);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (CanSeePlayer())
            isFleeing = true;
        if (isFleeing)
            Evade();
        if (!isResting && isFleeing == false)
        {
            if (!agent.hasPath)
                Wander();
            else
                Idle();
        }
    }
}
