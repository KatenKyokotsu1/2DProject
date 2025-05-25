using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSc : MonoBehaviour
{
    public SCitem scItem;

    void Start()
    {
        // Parent'a ��k
        Transform parent = transform.parent;

        // Parent alt�ndaki t�m SCitem'leri ara
        scItem = parent.GetComponentInChildren<SCitem>();

        if (scItem == null)
        {
            Debug.LogWarning("SCitem bulunamad�!");
        }
    }

    public void OnClickCollect()
    {
        if (scItem != null)
        {
            scItem.CollectSouls();
        }
    }
}
