using UnityEngine;
using UnityEngine.UI;

public class BackgroundOpacityController : MonoBehaviour
{
    public Image target = default; // ���İ��� ������ ��� �̹���
    private Color originalColor; // ���� ������ �����ϱ� ���� ����
    private Color targetColor; // ��ǥ ������ �����ϱ� ���� ����
    private float duration = 0.5f; // ���İ��� ����Ǵµ� �ɸ��� �ð� (�� ����)
    private float time = 0f; // ��� �ð��� �����ϱ� ���� ����
    private float startAlpha = 0f; // ���� ���İ�
    private float targetAlpha = 120f; // ��ǥ ���İ�

    private void Start()
    {
        // ���� ������ �����մϴ�.
        originalColor = target.color;
        // ��ǥ ������ �����մϴ�. (���İ��� 120�� ��)
        targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha / 255f);
    }

    // �̹����� ���İ��� �����ϴ� �޼���
    public void ChangeOpacity()
    {
        time = 0f; // ���� ���� �ð��� �ʱ�ȭ�Ͽ� ���ο� ���İ����� ������ �����մϴ�.
    }

    private void Update()
    {
        // ��� �ð��� ������ŵ�ϴ�.
        time += Time.deltaTime;
        // ������ ���� 0���� 1 ���̰� �ǵ��� Ŭ�����մϴ�.
        float t = Mathf.Clamp01(time / duration);
        // ���İ��� ���� ���İ��� ��ǥ ���İ� ������ ���� ������ �����մϴ�.
        float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
        // �̹����� ������ ���������� �����Ͽ� ���İ��� �����մϴ�.
        target.color = new Color(originalColor.r, originalColor.g, originalColor.b, currentAlpha / 255f);

        if (time >= duration)
        {
            // ���İ� ������ �Ϸ�Ǿ��� �� �߰����� ������ ó���� �� �ֽ��ϴ�.
        }
    }
}
