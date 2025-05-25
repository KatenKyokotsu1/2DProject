using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSc : MonoBehaviour
{
    public SCitem scItem;

    void Start()
    {
        // Parent'a çýk
        Transform parent = transform.parent;

        // Parent altýndaki tüm SCitem'leri ara
        scItem = parent.GetComponentInChildren<SCitem>();

        if (scItem == null)
        {
            Debug.LogWarning("SCitem bulunamadý!");
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
