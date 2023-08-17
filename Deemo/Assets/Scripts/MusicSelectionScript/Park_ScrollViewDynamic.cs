using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Park_ScrollViewDynamic : MonoBehaviour
{
    public Transform scrollViewContent;

    public GameObject prefab;

    public List<Sprite> listImg;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sprite listImg_ in listImg)
        {
            GameObject newListImg = Instantiate(prefab, scrollViewContent);

            if (newListImg.TryGetComponent<Park_ScrollViewItem>(out Park_ScrollViewItem item))
            {
                item.ChangeImage(listImg_);
            }
        }
    }
}
