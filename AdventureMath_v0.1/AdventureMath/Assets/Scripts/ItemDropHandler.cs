using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// COMPONENTE usado por los paneles de la GUI que permiten soltar (drop) GameObjects
public class ItemDropHandler : MonoBehaviour, IDropHandler // interfaz
{
    public AdminJuego aj;
    public GameObject fruta1;
    public GameObject fruta2;
    public RectTransform panel;

    public int contadorDrops = 0;

    Vector3 posicionInicial1;
    Vector3 posicionInicial2;

    void Start()
    {
        if (aj.dragdrop0 == false)
        {
            posicionInicial1 = fruta1.GetComponent<RectTransform>().anchoredPosition;
            posicionInicial2 = fruta1.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    // Método de implementar la interfaz
    // Accede al diccionario de imagenes de frutas y paneles del Adminjuego para validar si es posible hacer el drop en el panel que contiene este componente
    public void OnDrop(PointerEventData eventData)
    {
        if (aj.dragdrop0 == false)
        {
            // eventData.pointerDrag es el GameObject que se está arrastrando
            if (eventData.pointerDrag != null && aj.imgPanelFrutas[eventData.pointerDrag] == panel)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                contadorDrops++;
            }
            else
            {
                if (eventData.pointerDrag == fruta1)
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = posicionInicial1;
                else eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = posicionInicial2;
            }
        }
        if (aj.dragdrop0 == true && aj.dragdrop1 == false)
        {
            if (eventData.pointerDrag != null && aj.numParImpar[eventData.pointerDrag] == panel)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                aj.contadorDropsNumeros++;
            }
            else
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = aj.posicionesNumeros[eventData.pointerDrag];
            }
        }
        if (aj.dragdrop0 == true && aj.dragdrop1 == true && aj.dragdrop2 == false)
        {
            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                aj.contadorDropsTrozos++;
            }
        }
    }
}
