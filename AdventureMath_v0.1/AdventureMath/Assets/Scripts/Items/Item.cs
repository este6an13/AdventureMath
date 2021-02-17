using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =  "New Item", menuName = "Inventory/Item")] // Menú al crear el Asset
public class Item : ScriptableObject // Los items pueden ser creados como Assets
{
    new public string name = "New Item"; // new keyword para evitar conflictos con la variable de todo GameObject .name
    public string tipo;
    public Sprite icon = null;
    public bool isDefaultItem = false; // Algunos items no se deberían almacenar en el inventario
    public int cura;

    // Método que se llama cuando se hace clic sobre un item en el GUI inventario
    // Hasta el momento solo imprime un mensaje en la consola
    public virtual void Use()
    {
        Debug.Log("Using " + this.name);
    } 
}
