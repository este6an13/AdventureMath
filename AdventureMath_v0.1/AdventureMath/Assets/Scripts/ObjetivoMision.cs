using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// CLASE usada como tipo de atributo por la clase Misión
[System.Serializable] // Hace visible todos los atributos en el inspector
public class ObjetivoMision
{
    public TipoObjetivo tipoObjetivo;

    // Solo usado cuando el tipo de objetivo es RecogerFrutas por el momento
    public int cantidadRequerida;
    public int cantidadActual;

    public Text textoCantidadRequerida; // Usada para actualizar la GUI correspondiente

    // Método que verifica si el objetivo fue completado
    // Solo usado cuando el tipo de objetivo es RecogerFrutas por el momento
    public bool Completado()
    {
        return (cantidadActual >= cantidadRequerida);
    }

    // Solo usado cuando el tipo de objetivo es RecogerFrutas por el momento
    public void itemRecogido(Item item)
    {
        if (this.tipoObjetivo == TipoObjetivo.RecogerFrutas && item.tipo == "Fruta")
        {
            cantidadActual++;
            textoCantidadRequerida.text = cantidadActual.ToString();
        }
            
    }
}

// Enum que define los tipos de objetivos disponibles hasta ahora
public enum TipoObjetivo
{
    RecogerFrutas,
    Ejercicio
}
