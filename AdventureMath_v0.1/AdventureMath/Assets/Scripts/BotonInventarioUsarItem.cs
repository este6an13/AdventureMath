using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonInventarioUsarItem : MonoBehaviour
{
    public Item item;
    public AdminJuego aj;
    public BattleSystem bs;
    public GameObject boton;

    public void Usar()
    {
        aj.ventanaUsarItem.SetActive(false);
        aj.j.Curar(item.cura);
        bs.pantallaBatalla.SetActive(true);
        boton.SetActive(false);
        bs.ET(); // EnemyTurn
    }

}
