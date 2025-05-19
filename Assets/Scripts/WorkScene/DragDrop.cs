using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour,IEndDragHandler,IDragHandler,IBeginDragHandler
{
    private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;
    private Vector3 originalPosition;
    private Transform originalParent;


    private void Awake()
    {
        
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.position;
        originalParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject.CompareTag("GridCell"))
            {
                rectTransform.position = result.gameObject.transform.position;
                transform.SetParent(result.gameObject.transform); 
                return;
            }
            SCitem otherPlant = result.gameObject.GetComponentInParent<SCitem>();
            SCitem thisPlant = GetComponent<SCitem>();

            if (thisPlant != null && otherPlant != null && thisPlant != otherPlant)
            {
                TryMerge(thisPlant, otherPlant);
                return;
            }
        }

        rectTransform.position = originalPosition;
        transform.SetParent(originalParent);
    }
    
    public void TryMerge(SCitem sourcePlant, SCitem targetPlant)
    {
        if (sourcePlant.CanMergeWith(targetPlant))
        {
            if (sourcePlant.flowers == null || sourcePlant.flowers.nextLevelPlant == null) return;
            Flowers next = sourcePlant.flowers.nextLevelPlant;

            GameObject plantPrefab = next.nextPlantPrefab;
            GameObject upgraded = Instantiate(plantPrefab, targetPlant.transform.position, Quaternion.identity,canvas.transform);

            
            Image img = upgraded.GetComponent<Image>();
            img = next.icon;
            
            Destroy(sourcePlant.gameObject);
            Destroy(targetPlant.gameObject);
        }
        else
        {
            sourcePlant.transform.position = originalPosition;
        }
    }
}
