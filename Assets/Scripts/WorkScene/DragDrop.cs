using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
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
        transform.SetParent(canvas.transform); // drag sırasında UI hiyerarşisinden çıkıp en üste gel
        transform.SetAsLastSibling();
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
                transform.SetAsLastSibling();

                SCitem item = GetComponent<SCitem>();
                if (item != null)
                {
                    item.StartSoulProduction();
                    TryTripleMerge(item); 
                }
                return;
            }
        }

        //rectTransform.position = originalPosition;
        transform.SetParent(originalParent);
    }

    private void TryTripleMerge(SCitem sourcePlant)
    {
        Transform parent = sourcePlant.transform.parent;
        if (parent == null) return;

        List<SCitem> matchingPlants = new List<SCitem>();

        foreach (Transform child in parent)
        {
            SCitem scitem = child.GetComponent<SCitem>();
            if (scitem != null && scitem.flowers == sourcePlant.flowers)
            {
                matchingPlants.Add(scitem);
            }
        }

        if (matchingPlants.Count >= 3 && sourcePlant.flowers.nextLevelPlant != null)
        {
            Flowers next = sourcePlant.flowers.nextLevelPlant;
            GameObject plantPrefab = next.nextPlantPrefab;

            GameObject upgraded = Instantiate(plantPrefab, parent.position, Quaternion.identity, parent);
            upgraded.transform.SetAsLastSibling();
            SCitem item = upgraded.GetComponent<SCitem>();
            if (item != null) item.StartSoulProduction();

            Image img = upgraded.GetComponent<Image>();
            //img.sprite = next.icon.sprite;

            // 3 aynı bitkiyi yok et
            for (int i = 0; i < 3; i++)
            {
                Destroy(matchingPlants[i].gameObject);
            }
        }
    }
}
