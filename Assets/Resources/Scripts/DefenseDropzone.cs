using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DefenseDropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int temp = 0;

    public class_Deck Player1;
    public class_Deck Player2;

    //drops into panel
    public void OnDrop(PointerEventData eventData)
    {
        //only allow 1 child in the lanes. essentially card slots
        if (eventData.pointerDrag.GetComponent<class_Card>().m_bIsDraggable)
        {
            if (temp == 0)
            {
                //each card is has a draggable script
                Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
                if (d != null)
                {
                    d.m_parentReturn = this.transform;
                    temp = this.transform.childCount;
                    eventData.pointerDrag.GetComponent<class_Card>().Attack = 0;
                    eventData.pointerDrag.GetComponent<class_Card>().m_state = class_Card.AttackOrDefense.Defense;
                    eventData.pointerDrag.GetComponent<class_Card>().m_bIsDraggable = false;
                    Debug.Log(this.transform.parent.parent.name);
                    if (this.transform.parent.parent.name == "Player1")
                    {
                        Player1.DefenseLane.Add(eventData.pointerDrag);

                    }

                    if (this.transform.parent.parent.name == "Player2")
                    {
                        Player2.DefenseLane.Add(eventData.pointerDrag);

                    }

                }
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //on mouse enter if nothing is held nothing happens.
        if (eventData.pointerDrag == null)
        {
            return;
        }

        //make the held object the child of the whatever the mouse eneterd.

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeholderParent = this.transform;
            temp = this.transform.childCount;
        }

    }

    //when the mouse enters the panel this happens
    public void OnPointerExit(PointerEventData eventData)
    {

        //Debug.Log("OnPointerExit");
        //if there is nothing on the pointer it is null and nothing happens
        if (eventData.pointerDrag == null)
        {
            return;
        }

        //reference the Draggable script which whatever the mouse is holding.
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            d.placeholderParent = d.m_parentReturn;
            temp = this.transform.childCount;
        }

    }
}
