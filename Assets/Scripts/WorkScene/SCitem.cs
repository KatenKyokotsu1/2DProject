using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SCitem : MonoBehaviour
{
    public Flowers flowers;
    private Image img;
    public void Upgrade()
    {
        if (flowers.nextLevelPlant != null)
        {
            flowers = flowers.nextLevelPlant;
            img =GetComponent<Image>();
            img = flowers.icon;
        }
    }

    public bool CanMergeWith(SCitem other)
    {
        return flowers == other.flowers;
    }

}
