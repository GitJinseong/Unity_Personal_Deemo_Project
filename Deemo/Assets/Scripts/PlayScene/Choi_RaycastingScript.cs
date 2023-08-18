using UnityEngine;

public class Choi_RaycastingScript : MonoBehaviour
{
    private void Update()
    {
        // 현재 위치에서 목표 위치로 레이를 쏩니다.
        Vector3 rayOrigin = transform.position; // 현재 오브젝트의 위치
        Vector3 rayDirection = new Vector3(0.0f, 20.0f, 30.0f).normalized; // 목표 위치로 향하는 방향

        // 레이캐스트를 보냅니다.
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            // 레이가 물체와 충돌한 경우, 충돌 지점을 디버그로 출력합니다.
            Debug.DrawRay(rayOrigin, rayDirection * hitInfo.distance, Color.green);
            Debug.Log("레이가 물체와 충돌했습니다! 충돌 지점: " + hitInfo.point);
        }
        else
        {
            // 레이가 물체와 충돌하지 않은 경우, 끝점을 디버그로 출력합니다.
            Debug.DrawRay(rayOrigin, rayDirection * 100.0f, Color.red);
            Debug.Log("레이가 물체와 충돌하지 않았습니다.");
        }
    }
}
