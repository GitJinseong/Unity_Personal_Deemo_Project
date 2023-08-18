using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_NoteMovement : MonoBehaviour
{
    public Vector3 endCoords = new Vector3(0f, -2f, -6.2f);
    public float moveDuration = 3.0f;

    private Vector3 startCoords = new Vector3(0f, 20f, 30f); // 노트 시작 위치(x는 다른 곳에서 값설정)
    private float startTime;

    private bool isMoving = true; // 이동 중인지 여부를 판단하는 변수

    private void Start()
    {
        startCoords = transform.position; // 현재 위치를 시작 위치로 설정
        endCoords.x = startCoords.x;
        startTime = Time.time;
    }

    private void Update()
    {
        if (isMoving)
        {
            float elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            // 보간된 좌표 계산
            Vector3 interpolatedCoords = Vector3.Lerp(startCoords, endCoords, t);

            // 오브젝트 이동
            transform.position = interpolatedCoords;

            // 도달하면 이동 중지
            if (t >= 1.0f)
            {
                isMoving = false;
            }
        }
    }

    // 다시 이동을 시작하는 함수
    public void ResumeMoving()
    {
        isMoving = true;
        startTime = Time.time;
        startCoords = transform.position;
    }

    // 초기 위치를 변경하고 다시 이동을 시작하는 함수
    public void ResetAndMove()
    {
        startCoords = transform.position; // 초기 위치로 이동하기 전에 startCoords 값을 현재 위치로 업데이트
        startCoords.y = 20f;
        startCoords.z = 30f;
        transform.position = startCoords; // 초기 위치로 이동
        endCoords.x = startCoords.x;
        ResumeMoving(); // 이동 재개
    }
}