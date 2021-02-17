using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// CLASE que administra los prefabs BarraProgreso interactuando con la GUI
[ExecuteInEditMode()] // Permite la ejecución en Modo Editar, y no únicamente en Modo Juego
public class BarraProgreso : MonoBehaviour
{
    public int minimo;
    public int maximo;
    public int actual;
    public Image mask;
    public Image fill;
    public Color color;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetFillActual();
    }

    void GetFillActual()
    {
        float offsetActual = actual - minimo;
        float offsetMaximo = maximo - minimo;
        float cantidadFill = offsetActual / offsetMaximo;
        mask.fillAmount = cantidadFill;

        fill.color = color;
    }
}
