using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconControl : MonoBehaviour, IPointerClickHandler
{

    public int id;

    float lastClick = 0f;
    float interval = 0.4f;

    public void Action()
    {
        GameManager.instance.IconAction(id);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((lastClick + interval) > Time.time)
        {
            //is a double click
            Action();
        }
        else
        {
            //is a single click
            print(1);

            lastClick = Time.time;
        }
    }
}
