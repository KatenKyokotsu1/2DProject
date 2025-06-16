using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSc : MonoBehaviour
{
    public SCitem scItem;

    public void SetSCItem(SCitem item)
    {
        scItem = item;
    }

    public void OnClickCollect()
    {
        if (scItem != null)
        {
            scItem.CollectSouls();
        }
    }
}
