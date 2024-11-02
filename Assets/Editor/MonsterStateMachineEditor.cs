using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AIAgent))]
public class MonsterStateMachineEditor : Editor
{
    private void OnSceneGUI()
    {
        AIAgent agent = (AIAgent)target;

        //Shows the patrol range of the monster
        Handles.color = Color.magenta;
        Handles.DrawWireCube(agent.transform.position, new Vector3(agent.walkRange * 2, 0 ,agent.walkRange * 2));

        //Attack range
        Vector3 attackAngleLeft = DirectionFromAngle(agent.transform.eulerAngles.y, -agent.attackAngle / 2);
        Vector3 attackAngleRight = DirectionFromAngle(agent.transform.eulerAngles.y, agent.attackAngle / 2);
        Handles.color = Color.red;
        Handles.DrawWireArc(agent.transform.position, Vector3.up, Vector3.forward, 360, agent.attackRadius);
        Handles.DrawLine(agent.transform.position, agent.transform.position + attackAngleLeft * agent.attackRadius);
        Handles.DrawLine(agent.transform.position, agent.transform.position + attackAngleRight * agent.attackRadius);


    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
 
