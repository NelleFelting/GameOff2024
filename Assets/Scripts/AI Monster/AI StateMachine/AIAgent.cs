using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Threading;
using UnityEngine.UIElements;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AIStateId initialState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    public AIAgentConfig config;
    [HideInInspector] public MonsterSenses monsterSenses;
    public LayerMask groundLayer;

    public float walkRange;
    public int attackDmg;
    public float attackRadius;
    [Range(0, 360)]
    public float attackAngle;
    public float attackTime;

    public TMP_Text currentState;

    private float timer = 0f;
    public float attackCooldown;
    [HideInInspector] public bool canAttack;
    public GameObject attackPosition;
    public float grabSpeed;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        monsterSenses = this.GetComponent<MonsterSenses>();

        navMeshAgent = this.GetComponent<NavMeshAgent>();

        stateMachine = new AIStateMachine(this);
        stateMachine.RegisterState(new AIChasePlayerState());
        stateMachine.RegisterState(new AIIdleState());
        stateMachine.RegisterState(new AIStalkState());
        stateMachine.RegisterState(new AIPatrolState());
        stateMachine.RegisterState(new AIAttackState());
        stateMachine.ChangeState(initialState);
    }

    // Update is called once per frame
    void Update()
    {
        if(!canAttack)
        {
            timer += Time.deltaTime;
            if (timer >= attackCooldown)
            {
                canAttack = true;
                timer = 0f;
            }
        }
        stateMachine.Update();
    }
}
