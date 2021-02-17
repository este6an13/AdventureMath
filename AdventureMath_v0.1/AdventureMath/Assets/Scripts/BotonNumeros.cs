using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonNumeros : MonoBehaviour
{
    public Button boton;
    public Text texto;
    public bool presionado = false;
    Color color = new Color();

    public void PresionarBoton()
    {
        if (presionado == false)
        {
            presionado = true;
            if (ColorUtility.TryParseHtmlString("#32F8F4", out color))
                boton.GetComponent<Image>().color = color;
            texto.color = Color.white;
            AdminJuego.botonesPrimos.Add(boton);
            return;
        }
        if (presionado == true)
        {
            presionado = false;
            if (ColorUtility.TryParseHtmlString("#20124D", out color))
                boton.GetComponent<Image>().color = color;
            if (ColorUtility.TryParseHtmlString("#FFBB00", out color))
                texto.color = color;
            AdminJuego.botonesPrimos.Remove(boton);
        }
    }
}
