using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStateId
{
    ChasePlayer,
    Idle,
    Stalk,
    Patrol,
    Attack
}

public interface AIState
{
    AIStateId GetId();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
}
