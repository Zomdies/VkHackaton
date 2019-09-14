using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPlayScript : MonoBehaviour ,IDragHandler,IBeginDragHandler, IEndDragHandler
{   
    Camera MainCamera;
    Vector3 offSet;
    public Transform DefaultParent,DefaultTempCardParent;
    GameObject TempCardGO;
    public GameObject click;
    void AudioKlick() {
		click.GetComponent<AudioSource>().Play();
	}
    private void Awake() {
        MainCamera = Camera.allCameras[0];
        TempCardGO = GameObject.Find("TempCardGO");
    }
    
    public void OnBeginDrag(PointerEventData eventData){
        offSet = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
        DefaultParent = transform.parent;
        DefaultParent = DefaultTempCardParent = transform.parent;

        AudioKlick();
        
        TempCardGO.transform.SetParent(DefaultParent);
        TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        

    }
    public void OnDrag(PointerEventData eventData){
        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        newPos.z = 0;
        
        transform.position = newPos + offSet;
        if (TempCardGO.transform.parent != DefaultTempCardParent)
                TempCardGO.transform.SetParent(DefaultTempCardParent);
        CheckPosition();
        
    
    }
    public void OnEndDrag(PointerEventData eventData){
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());
        TempCardGO.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCardGO.transform.localPosition = new Vector3(2368, 0);
    }
    void CheckPosition()
    {
        int newIndex = DefaultTempCardParent.childCount;

        for(int i = 0; i < DefaultTempCardParent.childCount; i++)
        {
            if(transform.position.x < DefaultTempCardParent.GetChild(i).position.x)
            {
                newIndex = i;

                if (TempCardGO.transform.GetSiblingIndex() < newIndex)
                    newIndex--;

                break;
            }
        }

        // if (TempCardGO.transform.parent == DefaultParent)
        //     newIndex = startID;

        TempCardGO.transform.SetSiblingIndex(newIndex);
    }
}
