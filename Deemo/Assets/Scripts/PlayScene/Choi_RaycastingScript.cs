using UnityEngine;

public class Choi_RaycastingScript : MonoBehaviour
{
    private void Update()
    {
        // ���� ��ġ���� ��ǥ ��ġ�� ���̸� ���ϴ�.
        Vector3 rayOrigin = transform.position; // ���� ������Ʈ�� ��ġ
        Vector3 rayDirection = new Vector3(0.0f, 20.0f, 30.0f).normalized; // ��ǥ ��ġ�� ���ϴ� ����

        // ����ĳ��Ʈ�� �����ϴ�.
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            // ���̰� ��ü�� �浹�� ���, �浹 ������ ����׷� ����մϴ�.
            Debug.DrawRay(rayOrigin, rayDirection * hitInfo.distance, Color.green);
            Debug.Log("���̰� ��ü�� �浹�߽��ϴ�! �浹 ����: " + hitInfo.point);
        }
        else
        {
            // ���̰� ��ü�� �浹���� ���� ���, ������ ����׷� ����մϴ�.
            Debug.DrawRay(rayOrigin, rayDirection * 100.0f, Color.red);
            Debug.Log("���̰� ��ü�� �浹���� �ʾҽ��ϴ�.");
        }
    }
}
