using UnityEditor;
using UnityEngine;

public class Choi_TitleScene_ExampleUsage : MonoBehaviour
{
    private Choi_DelayManager delayManager = default;
    private void Start()
    {
        delayManager = GetComponent<Choi_DelayManager>();
        RegisterDelayedActions();
    }

    // 다양한 작업을 등록하고 각각 다른 딜레이를 설정하는 함수
    // 위의 액션의 딜레이가 끝난 후 다음 액션의 딜레이를 실행한다.
    private void RegisterDelayedActions()
    {
        // 기본 로고 실행
        delayManager.AddDelayedAction(2.0f, () => Choi_TitleSceneTasks.instance.StartRayarkLogo());

        // 배경 실행
        delayManager.AddDelayedAction(4.0f, () => Choi_TitleSceneTasks.instance.StartBackgrounds());

        // 디모 로고 실행
        delayManager.AddDelayedAction(8.0f, () => Choi_TitleSceneTasks.instance.StartDeemoLogo());

        // 터치 투 스타트 문구 실행
        delayManager.AddDelayedAction(3.0f, () => Choi_TitleSceneTasks.instance.StartTouchToStart());

    }
}