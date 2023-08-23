using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choi_ComboEffect : MonoBehaviour
{
    private void OnEnable()
    {
        SetActiveRecursively(true);
        StartCoroutine(DeactivateAfterDelay(0.5f));
    }

    private void SetActiveRecursively(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SetActiveRecursively(false);
    }
}