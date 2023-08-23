using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park_PlaySceneRotation : MonoBehaviour
{
    private Quaternion originalRotation;
    public Quaternion EndRotation;
    public float duration;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.localRotation;

        StartCoroutine(RotationChange());
    }

    private IEnumerator RotationChange()
    {
        yield return new WaitForSeconds(delay);

        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float time = Mathf.Clamp01(timeElapsed / duration);

            // Quaternion.Lerp를 사용하여 회전 값을 보간
            transform.rotation = Quaternion.Lerp(originalRotation, EndRotation, time);

            yield return null;
        }
    }
}
