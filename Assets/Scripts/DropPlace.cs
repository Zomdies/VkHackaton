using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlace : MonoBehaviour, IDropHandler ,IPointerEnterHandler, IPointerExitHandler
{   
    
    public void OnDrop(PointerEventData eventData){
        DragPlayScript card = eventData.pointerDrag.GetComponent<DragPlayScript>();
        if (card){
            card.DefaultParent = transform;
        }
    }
public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        DragPlayScript card = eventData.pointerDrag.GetComponent<DragPlayScript>();

        if (card)
            card.DefaultTempCardParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        DragPlayScript card = eventData.pointerDrag.GetComponent<DragPlayScript>();

        if (card && card.DefaultTempCardParent == transform)
            card.DefaultTempCardParent = card.DefaultParent;
    }
    
}
