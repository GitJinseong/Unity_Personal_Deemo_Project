using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_PitchController : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        float key = Park_GameManager.instance.key;
        float pitch;

        if (key == 1.5f)
        {
            pitch = 0.5f;
        }
        else if (key == 5.0f)
        {
            pitch = 1.0f;
        }
        else if (key == 9.0f)
        {
            pitch = 1.5f;
        }
        else if (key > 1.5f && key < 5.0f)
        {
            // 1.5와 5.0 사이 값의 경우 계산식을 참고하여 중간 pitch 값 설정
            pitch = (key - 1.5f) * 0.1f + 0.5f;
        }
        else if (key > 5.0f && key < 9.0f)
        {
            // 5.0와 8.5 사이 값의 경우 계산식을 참고하여 중간 pitch 값 설정
            pitch = (key - 5.0f) * 0.1f + 1.0f;
        }
        else
        {
            // 기본값 설정 (원하는 key 값이 없는 경우)
            pitch = 1.0f;
        }

        audioSource.pitch = pitch;
    }
}
