using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Run(float t, string name)
    {
        StartCoroutine(DelayForLoadScene(t, name));
    }

    public IEnumerator DelayForLoadScene(float t, string name)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(name);
    }
}
