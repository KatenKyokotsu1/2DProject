using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulManager : MonoBehaviour
{
    public int totalSoul;
    public Text pointText;
    void Start()
    {
        pointText.text = "Soul = " + totalSoul.ToString();

    }

    void Update()
    {
        
    }
    public void AddSoul(int flowerPoint)
    {
        
        totalSoul += flowerPoint;
        pointText.text = "Soul = " + totalSoul.ToString();
    }
    public void RemoveSoul(int flowerPoint)
    {
        totalSoul -= flowerPoint;
        pointText.text = "Soul = " + totalSoul.ToString();

    }
}
