using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;


public class SCitem : MonoBehaviour
{
    public Flowers flowers; // Bitki bilgileri (i�inde soulPoint vs.)
    public bool isInSlot = false;
    public float delay = 3f; // Ne kadar s�rede 1 soul �retilece�i
    public int storedSoulAmount = 0;
    public int maxSoulAmount = 10; // Maksimum birikecek soul say�s�
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
            yield return new WaitForSeconds(delay);

            if (storedSoulAmount < maxSoulAmount)
            {
                storedSoulAmount += flowers.soulPoint;
                storedSoulAmount = Mathf.Min(storedSoulAmount, maxSoulAmount);
            }
        }
    }

    public void CollectSouls()
    {
        Debug.Log("0 dan k���k");

        if (storedSoulAmount > 0)
        {
            Debug.Log("0 dan b�y�k");
            SoulManager soulManager = FindAnyObjectByType<SoulManager>();
            soulManager.AddSoul(storedSoulAmount);
            storedSoulAmount = 0;
        }
    }

}
