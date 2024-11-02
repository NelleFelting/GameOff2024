using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIChasePlayerState : AIState
{
    private float timer = 0.0f;
    private Vector3 direction;
    private float distanceToAttack;

    public void Enter(AIAgent agent)
    {
        agent.currentState.text = "Chase player";
        //Debug.Log("Enter chase player state");
    }

    public void Exit(AIAgent agent)
    {
        //Debug.Log("Exit chase player state");
    }

    public AIStateId GetId()
    {
        return AIStateId.ChasePlayer;
    }

    public void Update(AIAgent agent)
    {
        direction = (agent.monsterSenses.lastKnownPlayerPosition - agent.navMeshAgent.destination);
        distanceToAttack = Vector3.Distance(agent.transform.position, agent.monsterSenses.lastKnownPlayerPosition);

        if (!agent.enabled)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.monsterSenses.lastKnownPlayerPosition;
        }

        if (timer < 0.0f)
        {
            direction.y = 0;
            if (direction.sqrMagnitude > agent.config.minDistance * agent.config.minDistance)
            {
                if(agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.monsterSenses.lastKnownPlayerPosition;
                }
            }
            timer = agent.config.maxTime;
        }

        if ((Vector3.Angle(agent.transform.forward, direction) < agent.attackAngle) && (distanceToAttack < agent.attackRadius) && agent.monsterSenses.canDetectPlayer == true && agent.canAttack)
        {
            agent.stateMachine.ChangeState(AIStateId.Attack);
        }

        if (agent.monsterSenses.canDetectPlayer == false && (Vector3.Distance(agent.transform.position, agent.monsterSenses.lastKnownPlayerPosition) < agent.config.stoppingDistance))

        {
            agent.stateMachine.ChangeState(AIStateId.Idle);
        }
    }
}
