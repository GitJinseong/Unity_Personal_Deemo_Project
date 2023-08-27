using UnityEngine;

public class Choi_JudgeManager : MonoBehaviour
{
    public static Choi_JudgeManager instance;
    private Choi_CollisionDetection note_CollisionDetection;
    private Choi_Note note_Note;
    private SpriteRenderer note_SpriteRenderer;
    private float judge_CharmingTime;
    private float judge_NormalTime;
    private float judge_MissTime;
    private float speed;
    private float moveDuration;
    private const float DEFAULT_MOVE_DURATION = 4.0f;
    private Color charming_Color = new Color(1f, 0.9647f, 0.8549f); // RGB: FFF6DA
    private Color normal_Color = new Color(0.7686f, 0.8392f, 1f);  // RGB: C4D6FF
    private Color miss_Color = new Color(1f, 1f, 1f); // RGB: FFFFFF

    private void Awake()
    {
        speed = Park_GameManager.instance.speed;
        AdjustMoveDuration();
        instance = this;
    }

    public void CheckJudge(GameObject note)
    {
        note_CollisionDetection = note.GetComponent<Choi_CollisionDetection>();
        note_SpriteRenderer = note_CollisionDetection.obj_LightEffect.GetComponent<SpriteRenderer>();

        if (note_CollisionDetection.isHide == false)
        {
            note_Note = note.GetComponent<Choi_Note>();

            Debug.Log($"노트타이밍: {note_Note.runTime}");

            // 챠밍 판정
            if (note_Note.runTime > judge_CharmingTime && note_CollisionDetection.isJudgeHide == false)
            {
                note_CollisionDetection.Hide();
                ChangeColor(note_SpriteRenderer, charming_Color);
                Choi_GameManager.instance.AddCharming();
            }
            // 노말 판정
            else if (note_Note.runTime > judge_NormalTime && note_CollisionDetection.isJudgeHide == false)
            {
                note_CollisionDetection.Hide();
                ChangeColor(note_SpriteRenderer, normal_Color);
                Choi_GameManager.instance.AddNormal();
            }
            // 미스 판정
            else
            {
                note_CollisionDetection.Hide();
                ChangeColor(note_SpriteRenderer, miss_Color);
                Choi_GameManager.instance.AddMiss();
            }
        }
    }

    public void ChangeColor(SpriteRenderer sprite, Color _color)
    {
        sprite.color = _color;
    }

    private void AdjustMoveDuration()
    {
        if (speed == 5.0f)
        {
            moveDuration = 4.0f;
        }
        else if (speed == 1.0f)
        {
            moveDuration = 10.0f;
        }
        else if (speed == 9.0f)
        {
            moveDuration = 1.0f;
        }
        else if (speed > 1.0f && speed < 9.0f)
        {
            // speed가 1.5보다 크고 8.5보다 작을 때
            // speed가 1.5에서 8.5 사이에 있는 경우에 비례하여 moveDuration 값을 조정
            float normalizedSpeed = (speed - 1.0f) / 9.0f; // 1.0부터 9.0까지의 범위를 0에서 1 사이의 값으로 정규화
            moveDuration = Mathf.Lerp(10.0f, 1.0f, normalizedSpeed); // 1.0에서 10.0 사이로 보간하여 moveDuration 설정
        }

        SetJudgeDuration();
    }

    public void SetJudgeDuration()
    {
        //float difference = 0.0f;

        // 스피드가 기본 스피드 보다 높거나 같을 경우
        if (moveDuration <= DEFAULT_MOVE_DURATION)
        {
            //difference = 0.025f * moveDuration;
            judge_CharmingTime = moveDuration - (0.025f * moveDuration);
            judge_NormalTime = moveDuration - (0.05f * moveDuration);
        }
        // 스피드가 기본 스피드 보다 낮을 경우
        else if (moveDuration > DEFAULT_MOVE_DURATION)
        {
            //difference = 0.019f * moveDuration;
            judge_CharmingTime = moveDuration - (0.019f * moveDuration);
            judge_NormalTime = moveDuration - (0.038f * moveDuration);
        }

        //judge_CharmingTime = moveDuration - 0.1f;
        //judge_NormalTime = moveDuration - 0.2f;
    }
}
