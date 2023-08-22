using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Choi_JudgeButtonController : MonoBehaviour, IPointerClickHandler
{
    public GameObject obj_Notes; // 버튼을 클릭하면 Y값이 가장 낮은 자식 오브젝트를 찾을 부모 오브젝트
    private float min_SearchPosY = 2.0f; // 찾을 노트의 최소 y 값
    // 버튼 클릭 시 실행되는 함수
    public void OnPointerClick(PointerEventData eventData)
    {
        // Y값이 가장 낮은 자식 오브젝트를 찾아서 저장
        GameObject lowestYObject = GetLowestYObject(obj_Notes.transform);
        if (lowestYObject != null)
        {
            Debug.Log($"낮은y: {lowestYObject.transform.position.y}");
            // Y값이 가장 낮은 오브젝트를 매개변수로 하여 판정 함수 호출
            Choi_JudgeManager.instance.CheckJudge(lowestYObject);
        }
    }

    // Y값이 가장 낮은 자식 오브젝트를 찾아서 반환
    GameObject GetLowestYObject(Transform parentTransform)
    {
        GameObject lowestYObject = null;
        float lowestY = float.MaxValue;

        foreach (Transform child in parentTransform)
        {
            GameObject note = child.transform.Find("Note").gameObject;
            if (note.gameObject.activeSelf && note.transform.position.y < lowestY && note.transform.position.y < min_SearchPosY)
            {
                lowestY = note.transform.position.y;
                lowestYObject = note.transform.gameObject;
            }
        }

        return lowestYObject;
    }
}
