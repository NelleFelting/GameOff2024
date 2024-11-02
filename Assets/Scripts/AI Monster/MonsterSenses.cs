using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterSenses : MonoBehaviour
{
    public float visionRadius = 0f;
    [Range(0, 360)]
    public float visionAngle;

    public float peripheralVisionRadius = 0f;
    [Range(0, 360)]
    public float peripheralVisionAngle = 0f;

    public float hearingRadius = 0f;

    [HideInInspector] public float detectRadius = 0f;


    //public GameObject playerRef;
    private Rigidbody playerRB;

    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstructionMask;

    private bool canSeePlayer;
    private bool canHearPlayer;
    [HideInInspector] public bool canDetectPlayer;
    [HideInInspector] public Vector3 lastKnownPlayerPosition;
    [HideInInspector] public GameObject targetObject;

    private Vector3 directionToTarget;
    private float distanceToTarget;
    private Transform target;

    [SerializeField] bool monsterCanSee;
    [SerializeField] bool monsterCanHear;

    private void Start()
    {
        //playerRef = GameObject.FindGameObjectWithTag("Player");
        //playerRB = playerRef.GetComponent<Rigidbody>();
        StartCoroutine(SensesRoutine());

        if(!monsterCanSee && !monsterCanHear)
        {
            Debug.LogWarning(this.name + " cannot see or hear!");
        }
        if (monsterCanSee)
        {
            if (peripheralVisionRadius >= visionRadius)
            {
                Debug.LogWarning(this.name + " peripheral vision is >= normal vision, may cause unexpected results");
            }
        }       
    }

    private IEnumerator SensesRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return wait;
            SensesCheck();
        }
    }

    private void SensesCheck()
    {

        //if both sight and hearing are enabled chooses the largest detection radius and makes an overlap sphere using the radius to check if there is anything in the range with the "target mask"
        if (monsterCanSee && monsterCanHear)
        {
            if (visionRadius >= hearingRadius)
            {

                detectRadius = visionRadius;
            }
            else
            {
                detectRadius = hearingRadius;
            }
        }
        else if (monsterCanSee && !monsterCanHear)
        {
            detectRadius = visionRadius;
        }
        else if (!monsterCanSee && monsterCanHear)
        {
            detectRadius = hearingRadius;
        }

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, detectRadius, targetMask);

        //if there is an object in range with the target mask check if the object is in eyesight or is audible
        if (rangeChecks.Length != 0 )
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                if (rangeChecks[i].gameObject.tag == "Player")
                {
                    //gets the object detected by the physics sphere's transform, direction and distance
                    target = rangeChecks[i].transform;
                    targetObject = rangeChecks[i].gameObject;
                    directionToTarget = (target.position - transform.position).normalized;
                    distanceToTarget = Vector3.Distance(transform.position, target.position);
                    playerRB = target.gameObject.GetComponent<Rigidbody>();
                }
                else
                {
                    Debug.Log(rangeChecks[i] + " is not the player");
                }
            }          

            //if the monster is able to see, check if the object is in the monsters FOV
            if (monsterCanSee)
            {
                if ((Vector3.Angle(transform.forward, directionToTarget) < visionAngle / 2 && distanceToTarget <= visionRadius) || 
                    (Vector3.Angle(transform.forward, directionToTarget) < peripheralVisionAngle / 2 && distanceToTarget <= peripheralVisionRadius))
                {
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        canSeePlayer = true;
                    }
                    else
                    {
                        canSeePlayer = false;
                    }
                }
                else
                {
                    canSeePlayer = false;
                }
            }

            //if the monster can hear, check if the player is making noise within the hearing range
            if(monsterCanHear)
            {
                if(playerRB.velocity.magnitude > 5 && distanceToTarget <= hearingRadius)
                {                    
                    canHearPlayer = true;
                }
                else
                {
                    canHearPlayer = false;
                }
            }

            if (canSeePlayer || canHearPlayer)
            {
                canDetectPlayer = true;
                lastKnownPlayerPosition = target.position;
            }
            else
            {
                canDetectPlayer = false;
                targetObject = null;
            }
        }
        else if (canDetectPlayer)
        {
            canDetectPlayer = false;
            targetObject = null;
        }
    }
    //Draw the radius for the physics overlap sphere
    //private void OnDrawGizmos()
    //{
    //    Handles.color = Color.black;
    //    Handles.DrawWireArc(transform.position, Vector3.up, Vector3.forward, 360, detectRadius);
    //}
}
