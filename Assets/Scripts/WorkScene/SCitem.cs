using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;


public class SCitem : MonoBehaviour
{
    public Flowers flowers; 
    public bool isInSlot = false;
    public int storedSoulAmount = 0;
    public int soulSpeed;
    public Canvas canvas;
    private Coroutine soulCoroutine;

    public void StartSoulProduction()
    {
        if (!isInSlot)
        {
            isInSlot = true;
            soulCoroutine = StartCoroutine(CreateSoulLoop());
        }
    }
    

    IEnumerator CreateSoulLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(flowers.soulTimer);

            if (storedSoulAmount < flowers.soulPoint)
            {
                storedSoulAmount += soulSpeed;

                if (storedSoulAmount > flowers.soulPoint)
                {
                    storedSoulAmount = flowers.soulPoint;
                }
            }
        }
    }

    public void CollectSouls()
    {

        if (storedSoulAmount > 0)
        {
            SoulManager soulManager = FindAnyObjectByType<SoulManager>();
            soulManager.AddSoul(storedSoulAmount);
            storedSoulAmount = 0;
        }
    }

}
