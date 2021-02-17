using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Esta CLASE contiene los métodos que interactúan con la GUI cuando se lanza un dialogo de un interactable (NPC o Item)
public class AdminDialogo : MonoBehaviour {

	public Text textoNombre;
	public Text textoDialogo;
    public Image caraNPC;
    private Queue<string> frases; // Colección FIFO

    public static bool hablando;

	public Animator animador;

	// Use this for initialization
	void Start () {
        
		frases = new Queue<string>();

	}

	public void IniciarDialogo (Dialogo dialogo)
	{
		animador.SetBool("IsOpen", true);

        hablando = true;

		textoNombre.text = dialogo.nombre;

        caraNPC.sprite = dialogo.caraNPC;

		frases.Clear();

		foreach (string frase in dialogo.frases)
		{
			frases.Enqueue(frase);
		}

		MostrarSiguienteFrase();
	}

	public void MostrarSiguienteFrase ()
	{
		if (frases.Count == 0)
		{
			TerminarDialogo();
			return;
		}

		string frase = frases.Dequeue();
		StopAllCoroutines();
		StartCoroutine(EscribirFrase(frase));
	}

	IEnumerator EscribirFrase(string frase)
	{
		textoDialogo.text = "";
		foreach (char letra in frase.ToCharArray())
		{
			textoDialogo.text += letra;
			yield return null;
		}
	}

	public void TerminarDialogo()
	{
		animador.SetBool("IsOpen", false);
        hablando = false; 
	}
}
