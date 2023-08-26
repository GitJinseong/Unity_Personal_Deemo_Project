using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_NoteMovement : MonoBehaviour
{
    public Vector3 endCoords = new Vector3(0f, -2f, -6.2f);
    public float moveDuration = 4.0f;

    private Vector3 startCoords = new Vector3(0f, 20f, 30f); // 노트 시작 위치(x는 다른 곳에서 값설정)
    private float startTime;

    private bool isMoving = true; // 이동 중인지 여부를 판단하는 변수
    public float speed;

    private void Start()
    {
        speed = Park_GameManager.instance.speed;
        AdjustMoveDuration();
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

    private void AdjustMoveDuration()
    {
        if (speed == 5.0f)
        {
            moveDuration = 4.0f;
        }
        else if (speed == 1.5f)
        {
            moveDuration = 10.0f;
        }
        else if (speed == 8.5f)
        {
            moveDuration = 1.0f;
        }
        else if (speed > 1.5f && speed < 8.5f)
        {
            // speed가 1.5보다 크고 8.5보다 작을 때
            // speed가 1.5에서 8.5 사이에 있는 경우에 비례하여 moveDuration 값을 조정
            float normalizedSpeed = (speed - 1.5f) / 7.0f; // 1.5부터 8.5까지의 범위를 0에서 1 사이의 값으로 정규화
            moveDuration = Mathf.Lerp(10.0f, 1.0f, normalizedSpeed); // 1.0에서 10.0 사이로 보간하여 moveDuration 설정
        }
    }

}