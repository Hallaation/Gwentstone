using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int temp = 0;
    public class_Deck Player1;
    public class_Deck Player2;

    //if the card is draggable the card will drop into whatever lane it is dropped in. 
    //makes the card no longer draggable and adds it into the deck's specific lane list
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<class_Card>().m_bIsDraggable)
        {
            if (temp == 0)
            {
                Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
                if (d != null)
                {
                    d.m_parentReturn = this.transform;
                    temp = this.transform.childCount;
                    eventData.pointerDrag.GetComponent<class_Card>().Slot = transform.GetSiblingIndex() + 1;
                    eventData.pointerDrag.GetComponent<class_Card>().m_state = class_Card.AttackOrDefense.Attack;
                    eventData.pointerDrag.GetComponent<class_Card>().m_bIsDraggable = false;
                    if (this.transform.parent.parent.name == "Player1")
                    {
                        Player1.AttackLane.Add(eventData.pointerDrag);
                    }

                    if (this.transform.parent.parent.name == "Player2")
                    {
                        Player2.AttackLane.Add(eventData.pointerDrag);
                    }

                }
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
