using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnElements : MonoBehaviour
{
    Vector3 mousePosition;
    bool overCaldero = false;

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseUp()
    {
        if(overCaldero)
        {
            Debug.Log("Puntos");
        }
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("caldero"))
        {
            overCaldero = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("caldero"))
        {
            overCaldero = false;
        }
    }
}