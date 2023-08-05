using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFinder : MonoBehaviour
{
    public static ObjectFinder instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // 오브젝트의 이름을 받아 해당 오브젝트를 찾아서 반환한다.
    public GameObject FindObjectByName(string objectName)
    {
        foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>())
        {
            if (obj.name == objectName)
                return obj;
        }

        return null;
    }
}