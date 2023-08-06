using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip buttonClickSound;
    public float volume = 1.0f; // ���� ũ�⸦ ������ ����

    // ����� Ŭ���� ����ϸ� ���� �������ؼ� ����� �ҽ��� ����
    // ����ϰ� ������.
    private AudioSource audioSource;

    private void Start()
    {
        // AudioSource ������Ʈ�� �߰��ϰ� �����ɴϴ�.
        audioSource = gameObject.AddComponent<AudioSource>();
        // AudioClip�� �����մϴ�.
        audioSource.clip = buttonClickSound;
    }

    public void PlayButtonClickSound()
    {
        // ��ư Ŭ�� �Ҹ��� ����ϰ� ���� ũ�⸦ �����մϴ�.
        if (buttonClickSound != null)
        {
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}