using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip buttonClickSound;
    public float volume = 1.0f; // 사운드 크기를 조절할 변수

    // 오디오 클립만 사용하면 볼륨 조절못해서 오디오 소스를 통해
    // 재생하게 변경함.
    private AudioSource audioSource;

    private void Awake()
    {
        // AudioSource 컴포넌트를 추가하고 가져옵니다.
        audioSource = gameObject.AddComponent<AudioSource>();
        // AudioClip을 설정합니다.
        audioSource.clip = buttonClickSound;

        // AudioSource가 null이 아닌 경우, playOnAwake 속성을 false로 설정합니다.
        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    public void PlayButtonClickSound()
    {
        // 버튼 클릭 소리를 재생하고 사운드 크기를 설정합니다.
        if (buttonClickSound != null)
        {
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}