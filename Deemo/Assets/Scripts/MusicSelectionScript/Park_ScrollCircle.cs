using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Park_ScrollCircle : MonoBehaviour
{
    public Transform content; // Content transform to be scrolled
    public float radius = 100.0f; // Radius of the circular scroll

    private float currentAngle = 0.0f;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragDelta = eventData.delta;

        currentAngle += dragDelta.x * 0.1f; // Adjust sensitivity as needed

        float posX = Mathf.Sin(currentAngle) * radius;
        float posY = Mathf.Cos(currentAngle) * radius;

        content.localPosition = new Vector3(posX, posY, 0);
    }
}
