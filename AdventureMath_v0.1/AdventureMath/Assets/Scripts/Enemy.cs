using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyLevel;
    public int maxDamage;
    public int minDamage;
    public int maxHP;
    public int currentHP;
    public int xpOtorgada;
    public int fibOtorgados;
    public AdminJuego aj;

    private void Start()
    {
        maxDamage = (int)aj.GetDañoMax(enemyLevel);
        minDamage = (int)aj.GetDañoMin(enemyLevel);
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        aj.MostrarDaño(dmg);
        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
