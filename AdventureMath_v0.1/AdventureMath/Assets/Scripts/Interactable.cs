using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* Esta es la CLASE padre de la clase NPC y del componente ItemPickUp
 * que a su vez es usado por los Items que pueden recogerse.
 * Se creo porqué se determinó que los items en el juego pueden tener
 * características comunes a un NPC, es decir, contener diálogos (mensajes, pistas),
 * e iniciar misiones.
     */
public class Interactable : MonoBehaviour
{
    public Dialogo[] dialogos; // Conversaciones del GameObject interactivo
    public string interactableTag;

    // Variables usadas para interactuar
    public float radius = 2f; // Distancia desde la cuál se puede interactuar
    public Transform interactionTransform; // Contiene la posición, rotación y escala del GameObject
    public bool isFocus = false; // Por defecto, el GameObject interactivo no es foco si no se ha hecho clic sobre él
    Transform player; // Accede a la posición, rotación y escala del jugador que está interactuando. Útil para saber si está dentro del radio
    public bool hasInteracted = false;

    // Misión del NPC o Item. Se define desde el inspector
    public Mision mision;

    public Jugador jugador; // Referencia al jugador, útil en algunos métodos

    // Referencias a las GUI correspondientes para hacer visible la información de la misión
    public GameObject ventanaMision;
    public Text textoTitulo;
    public Text textoDescripcion;
    public Text textoXP;
    public Text textoFib;
    public Text textoTituloStatus;
    public Text textoCantidadRequeridad;
    public Text textoDescripcionStatus;
    public Text textoCantidadActual;
    public GameObject ventanaStatusMision;


    public virtual void Interact() // Este método es virtual porque es sobreescrito en las clase NPC y en el componente ItemPickup
    {
        Jugador.interactuando = true;
    }

    void Update() // Se llama cada frame
    {

        // Verifica si este GameObject es foco y si no se ha interactuado para como tal iniciar la interacción
        if (isFocus && !hasInteracted)
        {
            float distance = Vector2.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }


        // Estos if se crearon para asignar la misión del NPC al jugdor cuando corresponde
        if (jugador.mis1Mar == true && jugador.rec1Mar == false)
            mision = jugador.margarita.mision;

        if (jugador.mis2Fab == true && jugador.mis2FabOK == false)
            mision = jugador.fabricioTest.mision;

        if (jugador.mis3Car == true && jugador.rec3Car == false)
            mision = jugador.carminTest.mision;

        if (jugador.mis4Placas == true && jugador.rec4Placas == false)
            mision = jugador.placaPar.mision;

        if (jugador.mis5Portal == true && jugador.rec5Portal == false)
            mision = jugador.aj.iPortalUlam.mision;
    }

    // Método llamado desde la clase jugador cuando un interactivo es foco
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDeFocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
        Jugador.interactuando = false;
    }

    // Método que lanza el dialogo correspondiente
    // FindObjectofType es similar a GetComponent
    public void LanzarDialogo(int i)
    {
        FindObjectOfType<AdminDialogo>().IniciarDialogo(dialogos[i]);
    }

    // Abre la GUI correspondiente con los datos de la misión
    public void AbrirVentanaMision()
    {
        ventanaMision.SetActive(true);
        textoTitulo.text = mision.titulo;
        textoDescripcion.text = mision.descripcion;
        textoXP.text = mision.recompensaXP.ToString() + " XP";
        textoFib.text = mision.recompensaFib.ToString() + " FB";
    }

    // Se llama cuando el jugador ACEPTA la misión desde la GUI
    public void AceptarMision()
    {
        ventanaMision.SetActive(false);

        mision.activa = true;
        Jugador.mision = this.mision; // Pasa la misión de este objeto al jugador

        // Se ejecuta si la misión es de recoger objetos. 
        // No se ejecuta si la misión es un ejercicio como tal
        if (this.mision.objetivoMision.tipoObjetivo != TipoObjetivo.Ejercicio)
        {
            textoTituloStatus.text = mision.titulo;
            textoCantidadActual.text = "0";
            textoCantidadRequeridad.text = mision.objetivoMision.cantidadRequerida.ToString();
            textoDescripcionStatus.text = mision.descripcion;
            ventanaStatusMision.SetActive(true);
        }
    } 
}
