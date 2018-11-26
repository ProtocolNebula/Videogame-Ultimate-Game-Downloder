using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class doubleClickControl : MonoBehaviour, IPointerClickHandler
{
    float lastClick = 0f;
    float interval = 0.4f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((lastClick + interval) > Time.time)
        {
            //is a double click
            print(2);
        }
        else
        {
            //is a single click
            print(1);

            lastClick = Time.time;
        }
    }
}
