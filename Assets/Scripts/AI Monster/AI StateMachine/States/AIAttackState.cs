using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState
{
    private float timer;
    private GameObject attackTarget;
    private Rigidbody attackTargetRB;
    private Transform attackPosition;

    public void Enter(AIAgent agent)
    {
        attackPosition = agent.attackPosition.GetComponent<Transform>();
        agent.canAttack = false;
        agent.currentState.text = "Attack";
        attackTarget = agent.monsterSenses.targetObject;
        if(attackTarget.tag == "Player")
        {
            attackTargetRB = attackTarget.GetComponent<Rigidbody>();
            attackTargetRB.constraints = RigidbodyConstraints.FreezeAll;

        }
        timer = 0;
        //Debug.Log("Enter attack state");
    }

    public void Exit(AIAgent agent)
    {
        Debug.Log("Current player health is = " + attackTarget.GetComponent<PlayerHealth>().currentPlayerHealth);
        attackTargetRB.constraints = RigidbodyConstraints.None;
        attackTarget = null;
        timer = 0;

        //Debug.Log("Exit attack state");
    }

    public AIStateId GetId()
    {
        return AIStateId.Attack;
    }

    public void Update(AIAgent agent)
    {
        attackTarget.transform.position = Vector3.MoveTowards(attackTarget.transform.position, attackPosition.position, agent.grabSpeed * Time.deltaTime);

        timer += Time.deltaTime;
        if (timer >= agent.attackTime)
        {
            if (attackTarget)
            {
                if(attackTarget.GetComponent<PlayerHealth>())
                {
                    attackTarget.GetComponent<PlayerHealth>().DmgUnit(agent.attackDmg);
                    agent.stateMachine.ChangeState(AIStateId.Idle);
                }
                else
                {
                    Debug.LogWarning(attackTarget.name + " has no PlayerHealth sctipt");
                }
            }
            else
            {
                Debug.LogWarning("There is no player to attack");
            }
        }
    }
}
