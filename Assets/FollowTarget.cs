using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    public Transform[] targets;
    private int currenttarget;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        currenttarget = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = targets[currenttarget].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currenttarget = (currenttarget + 1) % targets.Length;
            agent.destination = targets[currenttarget].position;
        }
    }
}
