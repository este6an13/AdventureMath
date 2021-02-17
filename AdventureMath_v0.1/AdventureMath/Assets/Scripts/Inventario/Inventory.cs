using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// COMPONENTE usado por la clase Jugador
public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }

        instance = this;
    }
    
    #endregion 
    // El Singleton permite usar un tipo especial de notación para acceder al Inventario
    // Funciona como una clase estática. Solo puede una instancia Inventario por partida

    // Método Delegate
    // Se usa para actualizar la GUI del inventario
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallBack;

    public AdminJuego aj; // Referencia al AdminJuego
       
    public int space = 20; // Número de slots del inventario
    public List<Item> items = new List<Item>(); // Lista con los items almacenados

    // Método para agregar Item
    public bool AddItem(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                aj.MostrarMensaje("No hay suficiente espacio en tu maleta");
                return false;
            }

            items.Add(item);
            
            if (OnItemChangedCallBack != null)
                OnItemChangedCallBack.Invoke();
        }
        return true;
    }

    // Se llama cuando el usuario hace clic sobre la x del slot del inventario
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        
        if (OnItemChangedCallBack != null)
            OnItemChangedCallBack.Invoke();
    }

}
