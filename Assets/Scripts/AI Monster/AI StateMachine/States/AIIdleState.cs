using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AIIdleState : AIState
{
    private float timer = 0.0f;
    private float maxTime;

    public void Enter(AIAgent agent)
    {
        agent.currentState.text = "Idle";
        maxTime = Random.Range(0, 6);
        //Debug.Log("Enter idle state");
    }

    public void Exit(AIAgent agent)
    {
        timer = 0.0f;
        //Debug.Log("Exit idle state");
    }

    public AIStateId GetId()
    {
        return AIStateId.Idle;
    }

    public void Update(AIAgent agent)
    {
        if(agent.monsterSenses.canDetectPlayer)
        {
            agent.stateMachine.ChangeState(AIStateId.ChasePlayer);
        }

        timer += Time.deltaTime;
        if (timer > maxTime)
        {
            agent.stateMachine.ChangeState(AIStateId.Patrol); 
        }
    }
}
