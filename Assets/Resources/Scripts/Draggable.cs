using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Transform m_parentReturn = null;
    public Transform placeholderParent = null;

    //placeholder used to know where place card is going to be placed
    GameObject placeholder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        //make empty object make its parent whatever is currently selected
        if (eventData.pointerDrag.GetComponent<class_Card>().m_bIsDraggable)
        {
            placeholder = new GameObject();
            placeholder.transform.SetParent(this.transform.parent);

            LayoutElement le = placeholder.AddComponent<LayoutElement>();
            le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
            le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
            le.flexibleWidth = 0;
            le.flexibleHeight = 0;

            placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());


            m_parentReturn = this.transform.parent;
            placeholderParent = m_parentReturn;

            this.transform.SetParent(this.transform.parent.parent);

            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    //while dragging around set cards location to mouse location
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<class_Card>().m_bIsDraggable)
        {
            this.transform.position = eventData.position;

            //if the current parent isnt the placeholder. set it
            if (placeholder.transform.parent != placeholderParent)
            {
                placeholder.transform.SetParent(placeholderParent);
            }

            int newIndex = placeholderParent.childCount;

            for (int i = 0; i < placeholderParent.childCount; i++)
            {
                if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
                {
                    newIndex = i;

                    if (placeholder.transform.GetSiblingIndex() < newIndex)
                    {
                        newIndex--;
                    }
                    break;
                }
            }
            placeholder.transform.SetSiblingIndex(newIndex);
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        
            this.transform.SetParent(m_parentReturn);
        if (this.GetComponent<class_Card>().m_bIsDraggable)
        {
            this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        }
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            Destroy(placeholder);
        

    }
}
