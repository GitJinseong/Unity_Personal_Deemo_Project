using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayManager : MonoBehaviour
{
    // 딜레이와 함께 실행될 작업들을 담을 클래스
    private class DelayedAction
    {
        public float Delay { get; } // 딜레이 시간
        public System.Action Action { get; } // 실행될 작업

        public DelayedAction(float delay, System.Action action)
        {
            Delay = delay;
            Action = action;
        }
    }

    // 대기열로 사용할 리스트
    private readonly Queue<DelayedAction> actionQueue = new Queue<DelayedAction>();

    // 대기열에 작업 추가
    public void AddDelayedAction(float delay, System.Action action)
    {
        actionQueue.Enqueue(new DelayedAction(delay, action));
    }

    // 코루틴으로 대기열의 작업 실행
    private IEnumerator RunDelayedActions()
    {
        while (actionQueue.Count > 0)
        {
            DelayedAction delayedAction = actionQueue.Dequeue();
            yield return new WaitForSeconds(delayedAction.Delay);
            delayedAction.Action?.Invoke();
        }
    }

    // DelayManager 객체가 생성될 때 코루틴 실행
    private void Start()
    {
        StartCoroutine(RunDelayedActions());
    }
}