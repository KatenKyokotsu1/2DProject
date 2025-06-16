using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject Shop;
    private void Start()
    {
        Shop.SetActive(false);
    }

    public void OpenShop()
    {
        Shop.SetActive(true);
    }
    public void CloseShop()
    {
        Shop.SetActive(false);

    }
    public void TryPurchase(Flowers flower)
    {
        SoulManager manager = FindAnyObjectByType<SoulManager>();
        GameObject gameScene = GameObject.Find("Canvas");
        if (manager.totalSoul >= flower.price)
        {
            manager.RemoveSoul(flower.price);
            Instantiate(flower.prefab, Vector3.zero, Quaternion.identity,gameScene.transform);
        }
        else
        {
            Debug.Log("Yetersiz ruh puaný!");
        }
    }
}
