using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Run(float t, string name)
    {
        StartCoroutine(DelayForLoadScene(t, name));
    }

    public void asyncLoadScene(string name)
    {
        // 비동기 씬 로딩 시작
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);
        asyncLoad.allowSceneActivation = false; // 씬 로딩 완료 후 자동 활성화 막기

        // 로딩이 완료될 때까지 대기
        StartCoroutine(WaitForSceneLoad(asyncLoad));
    }

    public IEnumerator DelayForLoadScene(float t, string name)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(name);
    }

    private IEnumerator WaitForSceneLoad(AsyncOperation asyncLoad)
    {
        while (!asyncLoad.isDone)
        {
            // 로딩 진행 상황을 표시하거나 추가 작업 수행 가능
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 0.9f는 로딩이 90%까지 완료되었다는 의미
            Debug.Log("Loading progress: " + progress * 100 + "%");

            // 조건을 만족하면 씬 활성화
            if (progress >= 1.0f)
            {
                yield return new WaitForSeconds(1.0f);
                asyncLoad.allowSceneActivation = true; // 씬 활성화 허용
            }

            // 다음 프레임까지 대기
            yield return null;
        }

        // 로딩 완료 후 추가 작업 수행 가능
        Debug.Log("Scene loading complete!");
    }

}
