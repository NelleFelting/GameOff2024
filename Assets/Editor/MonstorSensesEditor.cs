using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MonsterSenses))]
public class MonstorSensesEditor : Editor
{   
    private void OnSceneGUI()
    {
        MonsterSenses sense = (MonsterSenses)target;
       
        //Main vision cone
        Vector3 viewAngleLeft = DirectionFromAngle(sense.transform.eulerAngles.y, -sense.visionAngle / 2);
        Vector3 viewAngleRight = DirectionFromAngle(sense.transform.eulerAngles.y, sense.visionAngle / 2);
        Handles.color = Color.yellow;
        Handles.DrawWireArc(sense.transform.position, Vector3.up, Vector3.forward, 360, sense.visionRadius);
        Handles.DrawLine(sense.transform.position, sense.transform.position + viewAngleLeft * sense.visionRadius);
        Handles.DrawLine(sense.transform.position, sense.transform.position + viewAngleRight * sense.visionRadius);

        //Peripheral vision cone
        Vector3 peripheralViewAngleLeft = DirectionFromAngle(sense.transform.eulerAngles.y, -sense.peripheralVisionAngle / 2);
        Vector3 peripheralViewAngleRight = DirectionFromAngle(sense.transform.eulerAngles.y, sense.peripheralVisionAngle / 2);
        Handles.color = Color.blue;
        Handles.DrawWireArc(sense.transform.position, Vector3.up, Vector3.forward, 360, sense.peripheralVisionRadius);
        Handles.DrawLine(sense.transform.position, sense.transform.position + peripheralViewAngleLeft * sense.peripheralVisionRadius);
        Handles.DrawLine(sense.transform.position, sense.transform.position + peripheralViewAngleRight * sense.peripheralVisionRadius);

        //Hearing range
        Handles.color = Color.green;
        Handles.DrawWireArc(sense.transform.position, Vector3.up, Vector3.forward, 360, sense.hearingRadius);

        //Target line
        if (sense.canDetectPlayer)
        {
            Handles.color = Color.red;
            Handles.DrawLine(sense.transform.position, sense.targetObject.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
