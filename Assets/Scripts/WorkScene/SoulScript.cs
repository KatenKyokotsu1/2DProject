using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
public class SoulScript : MonoBehaviour, IPointerClickHandler
{
    private Vector3 direction;

    public int flowerPoint;
    public void OnPointerClick(PointerEventData eventData)
    {
        SoulManager soulManager = FindAnyObjectByType<SoulManager>();
        soulManager.AddSoul(flowerPoint);

        Destroy(this.gameObject);
    }

    private void Start()
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = Random.Range(-0.5f, 0.5f);
        direction = new Vector3(x, y, 0);

        transform.DOMove(transform.position + direction, 0.3f);
        
    }
}
