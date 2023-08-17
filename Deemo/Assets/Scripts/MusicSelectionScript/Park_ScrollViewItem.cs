using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Park_ScrollViewItem : MonoBehaviour
{
    public Image childImage;

    public void ChangeImage(Sprite image)
    {
        childImage.sprite = image;
    }
}
