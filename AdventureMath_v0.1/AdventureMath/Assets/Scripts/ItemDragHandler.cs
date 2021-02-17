using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// COMPONENTE usado por los GameObjects de la GUI que pueden ser arrastrados
public class ItemDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler // Son Interfaces
{

    private CanvasGroup canvasGroup;

    void Start() 
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    

    // Los siguientes son los tres métodos creados al implementar cada interfaz
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        canvasGroup.blocksRaycasts = true;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }
}
