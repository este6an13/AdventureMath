using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// CLASE Misión, usada como tipo de atributo por los Interactables y el Jugador

[System.Serializable] // Permite que toda la clase sea visible en el inspector
public class Mision
{
    public bool activa;

    public NPC npc;
    public Jugador jugador;
    public string titulo;
    [TextArea(3,10)] // Amplia el espacio del TextArea en el inspector
    public string descripcion;
    public int recompensaXP;
    public int recompensaFib;
    public ObjetivoMision objetivoMision;

    public AdminJuego aj; // Referencia al AdminJuego
    public GameObject ventanaStatusMision; // Para visibilizar la ventana de status misión

    // Si la misión está lista, es decir se cumplió con el objetivo, se muestra un mensaje en la pantalla
    public void Lista()
    {
        aj.MostrarMensaje("¡Muy bien!, has cumplido el objetivo. Vuelve con " + npc.nombre + ".");
    }

    // Método que se llama para otorgar las recompensas correspondientes
    public void Completar()
    {
        activa = false;
        jugador.SetExperiencia(this.recompensaXP);
        jugador.SetFibonaccis(this.recompensaFib);
        aj.MostrarMensaje("¡" + titulo + " fue completada!");

        if (objetivoMision.tipoObjetivo != TipoObjetivo.Ejercicio)
            ventanaStatusMision.SetActive(false);
        
    }


}
