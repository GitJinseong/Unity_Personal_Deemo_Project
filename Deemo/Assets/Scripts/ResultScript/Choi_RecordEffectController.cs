using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Choi_RecordEffectController : MonoBehaviour
{
    public GameObject obj_NewRecord;
    public GameObject obj_FullCombo;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"신규기록 : {Park_GameManager.isNewRecord}");
        if (Choi_GameManager.instance.CheckIsFullCombo() == true)
        {
            obj_FullCombo.SetActive(true);
        }
        if (Park_GameManager.isNewRecord == true)
        {
            Park_GameManager.isNewRecord = false;
            StartCoroutine(SetVisibleObject(obj_NewRecord, 0.5f));
        }
    }

    private IEnumerator SetVisibleObject(GameObject obj, float t)
    {
        yield return new WaitForSeconds(t);
        obj.SetActive(true);
    }
}
