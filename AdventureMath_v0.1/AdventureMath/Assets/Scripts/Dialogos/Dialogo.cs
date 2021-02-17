using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CLASE usada como tipo de atributo por los interactables
[System.Serializable]
public class Dialogo : ScriptableObject
{
	public string nombre;
	[TextArea(3, 10)]
	public string[] frases;
    public Sprite caraNPC;
}
