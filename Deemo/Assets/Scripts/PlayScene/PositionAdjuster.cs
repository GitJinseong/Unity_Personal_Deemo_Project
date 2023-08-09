using UnityEngine;

public class PositionAdjuster : MonoBehaviour
{
    public float spacing = 1.0f; // 오브젝트 간격
    public LayerMask collisionLayer; // 충돌을 체크할 레이어
    public float raycastDistance = 0.5f; // 레이캐스트 거리

    private void Start()
    {
        Transform[] objectsToAdjust = GetComponentsInChildren<Transform>();

        foreach (Transform objTransform in objectsToAdjust)
        {
            Vector3 currentPosition = objTransform.position;
            Vector3 newPosition = currentPosition;

            Vector3 raycastDirection = Vector3.right; // 초기 레이캐스트 방향은 오른쪽으로 설정

            // 오브젝트의 위치에 따라 레이캐스트 방향을 좌우로 조정
            if (currentPosition.x > 0)
            {
                raycastDirection = Vector3.left; // 오른쪽에 위치한 경우 레이캐스트 방향을 왼쪽으로 설정
            }

            RaycastHit hit;

            if (Physics.Raycast(currentPosition, raycastDirection, out hit, raycastDistance, collisionLayer))
            {
                Debug.Log("레이캐스트");
                newPosition.x = hit.point.x + spacing;
                objTransform.position = newPosition;
            }
        }
    }
}
