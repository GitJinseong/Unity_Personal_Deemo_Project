using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choi_RhythmGameManager : MonoBehaviour
{
    public List<GameObject> noteList = new List<GameObject>();
    public Button button;

    private void Start()
    {
        // 노트 생성 및 리스트에 추가하는 로직을 구현합니다.
        // 예시로 노트를 생성하고 noteList에 추가하는 로직을 작성합니다.

        // 버튼 클릭 이벤트를 등록합니다.
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        // 버튼이 눌렸을 때 가장 가까운 노트를 찾아서 판정합니다.
        float closestDistance = float.MaxValue;
        GameObject closestNote = null;

        foreach (GameObject note in noteList)
        {
            float distance = Vector3.Distance(note.transform.position, button.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNote = note;
            }
        }

        // closestNote가 null이 아니라면 가장 가까운 노트가 있음을 의미합니다.
        if (closestNote != null)
        {
            // 판정 로직을 수행합니다.
            PerformJudgment(closestNote);
        }
    }

    private void PerformJudgment(GameObject note)
    {
        // 판정 로직을 구현합니다.
        // 예를 들어, closestNote에 해당하는 노트를 제거하거나 점수를 증가시키는 등의 동작을 수행할 수 있습니다.
    }
}