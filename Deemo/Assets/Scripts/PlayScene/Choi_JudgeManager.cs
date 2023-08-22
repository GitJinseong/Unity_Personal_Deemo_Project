using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Choi_JudgeManager : MonoBehaviour
{
    public static Choi_JudgeManager instance;
    private Choi_CollisionDetection note_CollisionDetection;
    private SpriteRenderer note_SpriteRenderer;
    private float judge_Charming = -1.4f;
    private float judge_Normal = -0.5f;
    private float note_PosY;
    private Color charming_Color = new Color(1f, 0.9647f, 0.8549f); // RGB: FFF6DA
    private Color normal_Color = new Color(0.7686f, 0.8392f, 1f);  // RGB: C4D6FF
    private Color miss_Color = new Color(1f, 1f, 1f); // RGB: FFFFFF

    private void Awake()
    {
        instance = this;
    }

    public void CheckJudge(GameObject note)
    {
        note_PosY = note.transform.position.y;
        note_CollisionDetection = note.GetComponent<Choi_CollisionDetection>();
        note_SpriteRenderer = note_CollisionDetection.obj_LightEffect.GetComponent<SpriteRenderer>();
        Debug.Log($"sprite: {note_SpriteRenderer}");
        if (note_CollisionDetection.isHide == false)
        {
            if (note_PosY < judge_Charming)
            {
                note_CollisionDetection.Hide();
                ChangeColor(note_SpriteRenderer, charming_Color);
                Choi_GameManager.instance.AddCharming();
            }
            else if (note_PosY < judge_Normal)
            {
                note_CollisionDetection.Hide();
                ChangeColor(note_SpriteRenderer, normal_Color);
                Choi_GameManager.instance.AddNormal();
            }
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
}
