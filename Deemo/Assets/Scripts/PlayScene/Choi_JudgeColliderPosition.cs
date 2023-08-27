using UnityEngine;

public class Choi_JudgeColliderPosition : MonoBehaviour
{
    private static readonly float DefaultResolutionHeight = 1440f;
    private static readonly float DefaultPosY_1440 = -21.5f;
    private static readonly float DefaultPosY_1668 = -24.5f;

    public static float CalculateColliderPosY(float resolutionHeight)
    {
        if (resolutionHeight == 1440)
        {
            return DefaultPosY_1440;
        }
        else if (resolutionHeight == 1668)
        {
            return DefaultPosY_1668;
        }
        else
        {
            // 선형 보간을 통해 값을 계산
            float t = (resolutionHeight - DefaultResolutionHeight) / (1668f - 1440f);
            return DefaultPosY_1440 + t * (DefaultPosY_1668 - DefaultPosY_1440);
        }
    }

    private void Start()
    {
        float colliderPosY_1440 = CalculateColliderPosY(1440);
        float colliderPosY_1668 = CalculateColliderPosY(1668);
        float colliderPosY_1880 = CalculateColliderPosY(1880);

        Debug.Log($"1440: {colliderPosY_1440}");
        Debug.Log($"1668: {colliderPosY_1668}");
        Debug.Log($"1880: {colliderPosY_1880}");
    }
}
