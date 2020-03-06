using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HandDropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    private int temp = 0;
    public class_Deck DeckController;

    void Update()
    {
        temp = transform.childCount;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + "was dropped on" + gameObject.name);
        if (temp <= 5)
        {
            Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
            if (d != null)
            {
                d.m_parentReturn = this.transform;
                temp = this.transform.childCount;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("OnPointerEnter");
        if (eventData.pointerDrag == null)
        {
            return;
        }


        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeholderParent = this.transform;
            temp = this.transform.childCount;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        if (eventData.pointerDrag == null)
        {
            return;
        }
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeholderParent = d.m_parentReturn;
            temp = this.transform.childCount;
        }

    }
}
