using UnityEngine;

public class Choi_PlayerDataManager : MonoBehaviour
{
    // 스코어 저장하는 함수
    public static void SaveScore(int index, string key, float score)
    {
        string tempKey = key + "_" + index; // EasyBestScore_1 과 같은 형태의 키 값으로 변경
        // 새로운 스코어가 기존 스코어 보다 높을 경우 저장
        Debug.Log($"기존: {GetScore(tempKey)}, 현재: {score}");
        if (GetScore(tempKey) < score)
        {
            Debug.Log("스코어높다");
            Park_GameManager.isNewRecord = true;
            PlayerPrefs.SetFloat(tempKey, score); // PlayerPrefab에 값 저장
            PlayerPrefs.Save(); // 변경 사항 저장
        }
    }

    // 스코어 호출 후 반환하는 함수
    public static float GetScore(string key)
    {
        return PlayerPrefs.GetFloat(key); // PlayerPrefab에 저장된 값을 호출 후 반환
    }

    // 배열 형태로 스코어 받아오는 함수
    public static float[] GetScoreForAll(string type)
    {
        int totalIndex = Park_GameManager.instance.musicInformation["List"].Count - 1;
        float[] tempScores = new float[totalIndex];
        string tempKey = "";
        for (int i = 0; i < totalIndex; i++)
        {
            tempKey = type + "_" + i; // EasyBestScore_1 과 같은 형태의 키 값으로 변경
            tempScores[i] = GetScore(tempKey);
        }

        return tempScores;
    }
}
