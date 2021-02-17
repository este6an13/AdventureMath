using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public BarraProgreso hpSlider;
    public BarraProgreso xpSlider;
    public Text textoActualHP;
    public Text textoMaxHP;
    public Text textoActualXP;
    public Text textoMaxXP;


    public void SetHUDEnemy(Enemy enemy)
    {
        nameText.text = enemy.enemyName;
        levelText.text = enemy.enemyLevel.ToString();
        hpSlider.maximo = enemy.maxHP;
        hpSlider.minimo = 0;
        hpSlider.actual = enemy.currentHP;
        textoActualHP.text = enemy.currentHP.ToString();
        textoMaxHP.text = enemy.maxHP.ToString();

    }

    public void SetHUDJugador(Jugador jugador)
    {
        nameText.text = jugador.nombre;
        levelText.text = jugador.nivel.ToString();
        hpSlider.maximo = jugador.saludMax;
        hpSlider.minimo = 0;
        hpSlider.actual = jugador.saludActual;
        xpSlider.maximo = jugador.xpRequerida;
        xpSlider.minimo = jugador.xpRequeridaAnterior;
        xpSlider.actual = jugador.experiencia;
        textoActualHP.text = jugador.saludActual.ToString();
        textoMaxHP.text = jugador.saludMax.ToString();
        textoActualXP.text = jugador.experiencia.ToString();
        textoMaxXP.text = jugador.xpRequerida.ToString();
    }

    public void SetHP(int hp)
    {
        hpSlider.actual = hp;
        textoActualHP.text = hp.ToString();
    }
}
