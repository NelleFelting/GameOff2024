using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : AIState
{
    private Vector3 destPoint;
    private Vector3 raycastPoint;
    private bool walkPointSet;

    public void Enter(AIAgent agent)
    {
        agent.currentState.text = "Patrol";
        walkPointSet = false;
        //Debug.Log("Enter patrol state");
    }

    public void Exit(AIAgent agent)
    {
        //Debug.Log("Exit patrol state");
    }

    public AIStateId GetId()
    {
        return AIStateId.Patrol;
    }

    public void Update(AIAgent agent)
    {
        //move to random position then switch back to idle
        Patrol(agent);
    }

    private void Patrol(AIAgent agent)
    {
        if (!agent.monsterSenses.canDetectPlayer)
        {
            if (!walkPointSet)
            {
                SearchForDest(agent);
            }
            if (walkPointSet)
            {
                agent.navMeshAgent.SetDestination(destPoint);
            }
            if (Vector3.Distance(agent.transform.position, destPoint) < agent.config.stoppingDistance)
            {
                walkPointSet = false;
                agent.stateMachine.ChangeState(AIStateId.Idle);
            }
        }
        else
        {
            walkPointSet = false;
            agent.stateMachine.ChangeState(AIStateId.ChasePlayer);
        }
    }

    void SearchForDest(AIAgent agent)
    {
        float z = Random.Range(-agent.walkRange, agent.walkRange);
        float x = Random.Range(-agent.walkRange, agent.walkRange);

        destPoint = new Vector3(agent.transform.position.x + x, agent.transform.position.y, agent.transform.position.z + z);
        raycastPoint = new Vector3(destPoint.x, destPoint.y + 1, destPoint.z);

        RaycastHit hit;


        if (Physics.Raycast(destPoint, Vector3.down, out hit, 10f, agent.groundLayer))
        {
            
            walkPointSet = true;
        }
    }
}
