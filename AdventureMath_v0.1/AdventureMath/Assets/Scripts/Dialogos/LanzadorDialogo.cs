using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CLASE que interactúa con AdminDialogo para lanzar las conversaciones
public class LanzadorDialogo : MonoBehaviour {

	public Dialogo dialogo;

    // FindObjectOfType es similar a GetComponent
	public void LanzarDialogo ()
	{
		FindObjectOfType<AdminDialogo>().IniciarDialogo(dialogo);
	}

}
