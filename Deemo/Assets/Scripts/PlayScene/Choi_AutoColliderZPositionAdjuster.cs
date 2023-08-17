using UnityEngine;

public class Choi_AutoColliderZPositionAdjuster : MonoBehaviour
{
    public float originalZPosition = -647f; // 기본 z 좌표 값

    private void Start()
    {
        // 현재 화면의 해상도를 가져옴
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        // 기기마다 다른 z 좌표 조정값을 계산
        float zAdjustment = CalculateZAdjustment(screenWidth, screenHeight);

        // 새로 조정된 z 좌표로 콜라이더의 z 좌표를 설정
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            Vector3 newColliderPosition = boxCollider.center;
            newColliderPosition.z = originalZPosition + zAdjustment;
            boxCollider.center = newColliderPosition;
        }
    }

    private float CalculateZAdjustment(int screenWidth, int screenHeight)
    {
        // 화면 해상도에 따른 z 좌표 조정값 계산 로직을 작성하세요.
        // 예를 들어, screenWidth와 screenHeight 값을 사용해서 조정값을 계산할 수 있습니다.
        // 아래는 임시로 예시로 작성한 코드입니다. 실제 값은 조정이 필요할 수 있습니다.

        // 갤럭시 노트 10의 해상도인 2280x1080을 기준으로 조정값을 계산합니다.
        int galaxyNote10Width = 2280;
        int galaxyNote10Height = 1080;
        float galaxyNote10ZAdjustment = -554f;

        // 갤럭시 노트 10의 해상도에 맞게 조정값을 계산합니다.
        if (screenWidth == galaxyNote10Width && screenHeight == galaxyNote10Height)
        {
            return galaxyNote10ZAdjustment;
        }
        else
        {
            // 다른 기기의 경우에 대한 조정값을 일반적으로 추측하여 계산합니다.
            // 예를 들어, 일반적으로 해상도가 1920x1080인 경우에 대해 조정값을 계산할 수 있습니다.
            int commonWidth = 1920;
            int commonHeight = 1080;
            float commonZAdjustment = -500f; // 추측한 값

            if (screenWidth == commonWidth && screenHeight == commonHeight)
            {
                return commonZAdjustment;
            }
            else
            {
                // 기타 기기에 대한 조정값을 추가로 추측하여 계산합니다.
                return 0f; // 기본값은 0으로 설정
            }
        }
    }
}
