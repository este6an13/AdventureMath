using UnityEngine;


// COMPONENTE usado por algunos Items. Permite que se puedan recoger y almacenar en el Componente Inventario
public class ItemPickUp : Interactable
{
    public Item item; // Una referecnia al ítem que usa este componente
    Jugador jugador; // Una referencia al jugador que interactúa con el item
    Mision mision = Jugador.mision; // Una referencia a la misión del jugador
    public AdminJuego aj; // Una referencia al AdminJuego para llamar algunos métodos

    public override void Interact() // Se sobreescribe el método virtual Interactuar
    {
        PickUp(); 

        // Se ejecuta durante la primera misión
        if (mision.activa)
        {
            mision.objetivoMision.itemRecogido(item);
            if (mision.objetivoMision.Completado())
                mision.Lista();
        }
    }

    // Método Recoger Item y almacenarlo en el Inventario
    void PickUp()
    {
        bool wasPickedUp = Inventory.instance.AddItem(item); // Se accede con esta notación porque se usó un Singleton

        if (wasPickedUp)
            gameObject.SetActive(false); // Oculta el item cuando se recoge satisfactoriamente

        Jugador.haRecogido = true; // Se utiliza en la misión 1 para crear flujos alternos
    }

}
