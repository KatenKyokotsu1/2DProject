using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flowers : ScriptableObject
{
    public string flowerName;
    public Image icon;
    public Flowers nextLevelPlant;
    public GameObject nextPlantPrefab;
    public GameObject prefab;
    public int price;
    public int soulPoint;
    public int level;
    public int soulTimer;
    public int soulSpeed;

}
