using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public CollectSc collect;
    public SCitem item;
    private Image img;
    public int soul;
    public GameObject soulFullEffect;
    public GameObject activeParticle;
    public GameObject canvas;

    void Start()
    {
        collect = GetComponentInChildren<CollectSc>();
        img = GetComponentInChildren<Image>();
        canvas = GameObject.Find("Canvas");
    }

    void Update()
    {
        if (transform.hasChanged)
        {
            item = GetComponentInChildren<SCitem>();
            if (item != null)
            {
                collect.SetSCItem(item);
                soul = item.storedSoulAmount;

                float fillPercent = (float)item.storedSoulAmount / item.flowers.soulPoint;

                img.color = Color.Lerp(Color.white, Color.green, fillPercent);
                
                if(item.storedSoulAmount >= item.flowers.soulPoint)
                {

                    if (activeParticle==null)
                    {
                        
                        activeParticle = Instantiate(soulFullEffect, this.gameObject.transform.position, this.gameObject.transform.rotation,canvas.transform);
                        activeParticle.transform.localRotation = Quaternion.Euler(-90, 0, 0);


                    }
                }
                else if(item.storedSoulAmount < item.flowers.soulPoint)
                {
                    if (activeParticle != null)
                    {
                        Destroy(activeParticle);
                        activeParticle = null;

                    }
                }
                
            }
            else
            {
                collect.SetSCItem(null);
                Destroy(activeParticle);
                soul = 0;
                img.color = Color.white;
            }


         }
        
    }
    
}
