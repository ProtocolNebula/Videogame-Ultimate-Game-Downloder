using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IconControl : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    public int id;
    public GameObject dragIcon;

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

            //GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            Action();
        }
        else
        {
            //is a single click

            //GameManager.instance.UpdateIcons();
            //GetComponent<Image>().color = new Color32(0, 0, 225, 30);

            lastClick = Time.time;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (id > 3)
        {
            GameObject newDragIcon = Instantiate(dragIcon, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.Euler(0,0,0));
            newDragIcon.GetComponent<DragControl>().IconSprite = GetComponent<Image>().sprite;
            newDragIcon.GetComponent<DragControl>().IconObject = gameObject;
        }
    }
}
