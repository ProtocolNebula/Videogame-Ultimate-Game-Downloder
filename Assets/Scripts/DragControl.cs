using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragControl : MonoBehaviour {

    public GameObject IconObject;
    public Sprite IconSprite;
    private bool overTrash;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = IconSprite;
        transform.localScale = new Vector3(0.3f, 0.3f, 1);
    }

    private void Update()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; //distance of the plane from the camera
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);

        Debug.Log(transform.localScale);

        if (Input.GetMouseButtonUp(0))
        {
            if (overTrash)
            {
                Destroy(IconObject);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "trash")
        {
            overTrash = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "trash")
        {
            overTrash = false;
        }
    }
}
