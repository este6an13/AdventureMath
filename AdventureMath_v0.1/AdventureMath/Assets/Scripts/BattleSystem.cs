using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {  START, PLAYERTURN, ENEMYTURN, WON, LOST  }


public class BattleSystem : MonoBehaviour
{

    public Jugador jugador;
    public Enemy enemigo;

    public BattleState state;

    public Text dialogueText;

    public BattleHud HUDjugador;
    public BattleHud HUDenemigo;

    public GameObject estadisticasJugador;

    public List<GameObject> hechizos = new List<GameObject>();
    public int contadorHechizos = 0;

    public InputField textoHechizo0;
    public InputField texto0Hechizo1;
    public InputField texto1Hechizo1;
    public InputField texto0Hechizo2;
    public InputField texto1Hechizo2;
    public InputField texto2Hechizo2;
    public InputField texto0Hechizo3;
    public InputField texto1Hechizo3;
    public InputField textoHechizo4;
    public BotonNumeros botonVacio1;
    public BotonNumeros botonVacio2;
    public InputField texto0Hechizo5;
    public InputField texto1Hechizo5;
    public InputField textoHechizo6;
    public InputField texto0Hechizo7;
    public InputField texto1Hechizo7;
    public InputField textoHechizo8;
    public InputField textoHechizo9;

    public GameObject pantallaBatalla;
    public GameObject inventarioBatalla;

    public GameObject canvasLecciones;
    public GameObject botonPrepararHechizo;
    public GameObject botonUsarItem;

    public AdminJuego aj;

    // Start is called before the first frame update
    public void StartBattle()
    {
        canvasLecciones.SetActive(false);
        estadisticasJugador.SetActive(false);
        pantallaBatalla.SetActive(true);
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        
    }

    IEnumerator SetupBattle()
    {
        dialogueText.text = "Un " + enemigo.enemyName + " salvaje apareció ...";
        HUDjugador.SetHUDJugador(jugador);
        HUDenemigo.SetHUDEnemy(enemigo);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Elige una acción:";
        botonPrepararHechizo.SetActive(true);
        botonUsarItem.SetActive(true);
    }

    IEnumerator Atacar(int daño, bool completo, List<string> respuestas)
    {
        botonPrepararHechizo.SetActive(false);
        botonUsarItem.SetActive(false);
        bool isDead = enemigo.TakeDamage(daño);
        HUDenemigo.SetHP(enemigo.currentHP);
        if (daño == 0)
            dialogueText.text = "Fallaste esta vez";
        else if (!completo)
            dialogueText.text = "Puedes hacerlo mejor la próxima vez";
        else
            dialogueText.text = "Preparaste el hechizo exitosamente";
        yield return new WaitForSeconds(2f);
        dialogueText.text = "Las respuestas eran";
        yield return new WaitForSeconds(1f);
        foreach (string respuesta in respuestas)
        {
            dialogueText.text = respuesta;
            yield return new WaitForSeconds(1f);
        }
        dialogueText.text = "Te quedan " + (10 - contadorHechizos) + " hechizos";
        yield return new WaitForSeconds(2f);
        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void ET()
    {
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemigo.enemyName + " atacó";
        yield return new WaitForSeconds(1f);
        bool isDead = jugador.TakeDamage(Random.Range(enemigo.minDamage, enemigo.maxDamage));
        HUDjugador.SetHP(jugador.saludActual);
        yield return new WaitForSeconds(1f);
        if (isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }


    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "¡Ganaste la batalla!";
            yield return new WaitForSeconds(2f);
            dialogueText.text = "Obtuviste " + enemigo.xpOtorgada.ToString() + " puntos de experiencia";
            jugador.SetExperiencia(enemigo.xpOtorgada);
            yield return new WaitForSeconds(2f);
            dialogueText.text = "Obtuviste " + enemigo.fibOtorgados.ToString() + " fibonaccis";
            jugador.SetExperiencia(enemigo.fibOtorgados);

            if (jugador.experiencia >= jugador.aj.GetExperienciaRequerida(jugador.nivel))
            {
                yield return new WaitForSeconds(2f);
                dialogueText.text = "Has subido al nivel " + jugador.nivel.ToString();
            }
        }
        else
        {
            dialogueText.text = "Has sido derrotado";
            yield return new WaitForSeconds(2f);
        }

        pantallaBatalla.SetActive(false);
        canvasLecciones.SetActive(true);
        estadisticasJugador.SetActive(true);
        jugador.batallaFinalizada = true;
    }

    public void OnBotonPrepararHechizo()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        if (contadorHechizos > 9)
            StartCoroutine(EndBattle());
        else
        {
            hechizos[contadorHechizos].SetActive(true);
            pantallaBatalla.SetActive(false);
        }
    }

    public void OnBotonLanzarHechizo()
    {

        hechizos[contadorHechizos].SetActive(false);
        pantallaBatalla.SetActive(true);

        if (contadorHechizos == 0)
        {
            List<string> respuestas = new List<string>();
            respuestas.Add("elementos");

            if (textoHechizo0.text.ToLower() == "elementos")
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax)), true, respuestas));
            else
                StartCoroutine(Atacar(0, false, respuestas));
        }
        if (contadorHechizos == 1)
        {
            List<string> respuestas = new List<string>();
            respuestas.Add("pertenencia");
            respuestas.Add("pertenece");

            if (texto0Hechizo1.text.ToLower() == "pertenencia" && texto1Hechizo1.text.ToLower() == "pertenece")
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax)), true, respuestas));
            else if (texto0Hechizo1.text.ToLower() == "pertenencia" || texto1Hechizo1.text.ToLower() == "pertenece")
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax) / 2), false, respuestas ));
            else
                StartCoroutine(Atacar(0, false, respuestas));        }
        if (contadorHechizos == 2)
        {
            List<string> respuestas = new List<string>();
            respuestas.Add("subconjunto");
            respuestas.Add("x");
            respuestas.Add("B");

            var correctas = 0;
            if (texto0Hechizo2.text.ToLower() == "subconjunto")
                correctas++;
            if (texto1Hechizo2.text == "x")
                correctas++;
            if (texto2Hechizo2.text == "B")
                correctas++;
            if (correctas == 0)
                StartCoroutine(Atacar(0, false, respuestas));
            if (correctas == 1)
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax) / 2), false, respuestas));
            if (correctas == 2)
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax)), false, respuestas));
            if (correctas == 3)
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax) * 2), true, respuestas));
        }
        if (contadorHechizos == 3)
        {
            List<string> respuestas = new List<string>();
            respuestas.Add("contenido");
            respuestas.Add("contenencia");

            if (texto0Hechizo3.text.ToLower() == "contenido" && texto1Hechizo3.text.ToLower() == "contenencia")
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax)), true, respuestas));
            else if (texto0Hechizo3.text.ToLower() == "contenido" || texto0Hechizo3.text.ToLower() == "contenencia")
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax) / 2), false, respuestas));
            else
                StartCoroutine(Atacar(0, false, respuestas));
        }
        if (contadorHechizos == 4)
        {
            List<string> respuestas = new List<string>();
            respuestas.Add("0 o cero");
            respuestas.Add("Ø");

            var correctas = 0;
            if (textoHechizo4.text == "0" || textoHechizo4.text == "cero")
                correctas++;
            if (botonVacio1.presionado == true)
                correctas++;
            if (botonVacio2.presionado == false)
                correctas++;
            if (correctas == 0)
                StartCoroutine(Atacar(0, false, respuestas));
            if (correctas == 1)
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax) / 3), false, respuestas));
            if (correctas == 2)
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax) / 2), false, respuestas));
            if (correctas == 3)
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax)), true, respuestas));
        }
        if (contadorHechizos == 5)
        {
            List<string> respuestas = new List<string>();
            respuestas.Add("múltiplo de 3 o es múltiplo de 3");
            respuestas.Add("30");

            var correctas = 0;
            if (texto0Hechizo5.text.ToLower() == "múltiplo de 3" || texto0Hechizo5.text.ToLower() == "es múltiplo de 3")
                correctas++;
            if (texto1Hechizo5.text == "30")
                correctas++;
            if (correctas == 0)
                StartCoroutine(Atacar(0, false, respuestas));
            if (correctas == 1)
                StartCoroutine(Atacar(Random.Range(jugador.dañoMin, jugador.dañoMax), false, respuestas));
            if (correctas == 2)
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax) * 2), true, respuestas));
        }
        if (contadorHechizos == 6)
        {
            List<string> respuestas = new List<string>();
            respuestas.Add("B = {6, 8, 10, 12, 14, 16, 18}");

            if (textoHechizo6.text == "B = {6, 8, 10, 12, 14, 16, 18}" || textoHechizo6.text == "B={6,8,10,12,14,16,18}" || textoHechizo6.text == "B = {6,8,10,12,14,16,18}")
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax) * 2), true, respuestas));
            else
                StartCoroutine(Atacar(0, false, respuestas));
        }
        if (contadorHechizos == 7)
        {
            List<string> respuestas = new List<string>();
            respuestas.Add("dos divisores");
            respuestas.Add("1 o uno");

            if (texto0Hechizo7.text.ToLower() == "dos divisores" && (texto1Hechizo7.text == "1" || texto1Hechizo7.text.ToLower() == "uno"))
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax)), true, respuestas));
            else if (texto0Hechizo7.text.ToLower() == "dos divisores" || texto1Hechizo7.text == "1" || texto1Hechizo7.text.ToLower() == "uno")
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax) / 2), false, respuestas));
            else
                StartCoroutine(Atacar(0, false, respuestas));
        }
        if (contadorHechizos == 8)
        {
            List<string> respuestas = new List<string>();
            respuestas.Add("C = {2, 3, 5, 7, 11, 13}");

            if (textoHechizo8.text == "C = {2, 3, 5, 7, 11, 13}" || textoHechizo8.text == "C = {2,3,5,7,11,13}" || textoHechizo8.text == "C={2,3,5,7,11,13}")
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax) * 2), true, respuestas));
            else
                StartCoroutine(Atacar(0, false, respuestas));
        }
        if (contadorHechizos == 9)
        {
            List<string> respuestas = new List<string>();
            respuestas.Add("números naturales");

            if (textoHechizo9.text == "números naturales")
                StartCoroutine(Atacar((Random.Range(jugador.dañoMin, jugador.dañoMax) * 3), true, respuestas));
            else
                StartCoroutine(Atacar(0, false, respuestas));
        }

        contadorHechizos++;
    }

    public void OnBotonUsarItem()
    {
        pantallaBatalla.SetActive(false);
        aj.ventanaUsarItem.SetActive(true);   
    }
}
