using Unity.Mathematics;
using UnityEngine;

public class HelperScript
{
    public static float Deviate(float center, float deviation)
    {
        return UnityEngine.Random.Range(center - deviation, center + deviation);
    }

    public static float RemapFloat(float value, float2 InMinMax, float2 OutMinMax)
    {
        return OutMinMax.x + (value - InMinMax.x) * (OutMinMax.y - OutMinMax.x) / (InMinMax.y - InMinMax.x);
    }

    public static float OneMinus(float value)
    {
        return 1 - value;
    }

    public static float Reciprocal(float value)
    {
        return 1 / value;
    }
}