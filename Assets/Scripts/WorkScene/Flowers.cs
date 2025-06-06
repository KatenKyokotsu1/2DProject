using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flowers : ScriptableObject
{
    public string name;
    public Image icon;
    public Flowers nextLevelPlant;
    public GameObject nextPlantPrefab;
    public int soulPoint;
    public int level;
    public int soulTimer;
}
