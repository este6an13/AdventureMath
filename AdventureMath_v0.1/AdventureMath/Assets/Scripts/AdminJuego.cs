using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * Esta CLASE se creó con el fin de que sirva de administrador de la secuencia del juego
 * Se asignan instancias de objetos y algunos métodos que son llamados desde otras clases
 * Debido a que al principio no hubo suficiente claridad de algunos conceptos de Unity y C#,
 * esta función de administrar la secuencia se comparte con la clase Jugador, auqnue no se sea
 * lo más adecuado.
 */
public class AdminJuego : MonoBehaviour
{
    public static GameObject[] frutasPrefabs; // Se creo para instanciar las frutas cuando se necesita
    public GameObject[] setFrutasPrefabs; // Creada para visualizar una variable estática en el inspector

    public GameObject ventanaMensajes;
    public Text textoMensajes;

    public GameObject botonInicioLeccion1;

    public GameObject leccion101;
    public GameObject leccion102;
    public GameObject leccion103;
    public GameObject leccion104;
    public GameObject leccion105;
    public GameObject leccion106;
    public GameObject leccion107;
    public GameObject leccion108;
    public GameObject leccion109;
    public GameObject leccion110;
    public GameObject leccion111;
    public GameObject leccion112;
    public GameObject leccion113;
    public GameObject leccion114;
    public GameObject leccion115;
    public GameObject leccion116;
    public GameObject leccion117;
    public GameObject leccion118;
    public GameObject leccion119;
    public GameObject leccion120;
    public GameObject leccion121;
    public GameObject leccion122;
    public GameObject leccion123;
    public GameObject leccion124;
    public GameObject leccion125;
    public GameObject leccion126;
    public GameObject leccion127;
    public GameObject leccion128;
    public GameObject leccion129;
    public GameObject leccion130;
    public GameObject leccion131;
    public GameObject leccion132;
    public GameObject leccion133;
    public GameObject leccion134;
    public GameObject leccion135;
    public GameObject leccion136;
    public GameObject leccion137;
    public GameObject leccion138;
    public GameObject leccion139;
    public GameObject leccion140;
    public GameObject leccion141;
    public GameObject leccion142;
    public GameObject leccion143;
    public GameObject leccion144;
    public GameObject leccion145;


    public GameObject ventanaEjeMis2;
    public GameObject ventanaEjeMis3;
    public GameObject ventanaEjeMis4;
    public GameObject ventanaEjeMis4a;

    public int leccionesMostradas = 0;
    public int leccionesTerminadas = 0;

    // Estas dos variables se usan en la ventana de EjercicioMision2
    public GameObject[] imagenesFrutas;
    public RectTransform[] panelesFrutas;
    public Dictionary<GameObject, Vector3> posicionesFrutas = new Dictionary<GameObject, Vector3>();

    // Usado en el drag & drop 2
    public GameObject[] numeros1a20;
    public RectTransform[] espaciosVacios;

    public GameObject margarita;
    public GameObject fabricio; // Se instanció en esta clase para ocultar a Fabricio mientras no se necesita
    public GameObject sandia; // Se creo para instanciar el prefab de la sandía cuando se requiere
    public GameObject carmin; // Se instanció en esta clase para ocultar a Fabricio mientras no se necesita
    public GameObject azaleia; // Esta referencia se usa de Jugador para hacerla visible cuando se necesita
    public GameObject coco; // No es el item como tal, sino una imagen

    public GameObject carminClone; // Referencia al Clone instanciado de Carmín desde el prefab

    // Referencias para hacer visibles las placas cuando es necesario
    public GameObject placaPar;
    public GameObject placaImpar;

    // Referencias a los Objetos Interactivos para poder acceder a sus diálogos desde Jugador
    // Pudieron haber sido referenciados en la clase Jugador directamente
    public Interactable placaParI;
    public Interactable placaImparI;


    public Light luzAzaleia;
    public Light luzCarmin;

    // Referencias para hacer visibles y no vivisbles las ventanas del hechizo de Azaleia
    public GameObject frutasAzaleia;
    public GameObject numerosAzaleia;

    public int contadorHechizoAzaleia = 0;
    public bool efeLuzAza = false; // Efecto de Luz de Azaleia
    public bool desLuzAza = false; // Deshacer Luz de Azaleia 
    public bool hechizoAzaTerminado = false; // El hechizo de Azaleia terminó

    // Para facilitar el método de MostrarSiguienteLeccion(), RetrocederLeccion() y TerminarLeccion() de los botones de las leccioenes
    public Dictionary<int, GameObject> lecciones = new Dictionary<int, GameObject>();

    // Relaciona a cada fruta con su panel correspondiente en el EjercicioMision2
    public Dictionary<GameObject, RectTransform> imgPanelFrutas = new Dictionary<GameObject, RectTransform>();

    public Dictionary<GameObject, RectTransform> numParImpar = new Dictionary<GameObject, RectTransform>();

    public Dictionary<GameObject, Vector3> posicionesNumeros = new Dictionary<GameObject, Vector3>();

    public Dictionary<GameObject, Vector3> posicionesTrozos = new Dictionary<GameObject, Vector3>();

    public GameObject msjEjeMis4;
    public Text textoMsjEjeMis4;

    public GameObject[] trozosNotCom;
    public GameObject[] espaciosDropTrozos;

    // Son los inputFields del EjercicioMision3
    public InputField inputA;
    public InputField inputB;
    public InputField inputC;
    public InputField inputD;
    public InputField inputE;
    public InputField inputF;
    public GameObject msjSigueIntentando;

    public bool ejeMis3OK = false; // True cuando el jugador completa el ejercicioMision3. 
                                   // Se utiliza en la clase Jugador para continuar secuencia


    // Ejercicios de drag & drop completados para determinar como se debe comportar el Item Drop Handler
    public bool dragdrop0 = false;
    public bool dragdrop1 = false;
    public bool dragdrop2 = false;

    public int contadorDropsNumeros = 0;
    public int contadorDropsTrozos = 0;

    public Animator[] animadoresPrimos;
    public GameObject[] numerosPrimos;

    public GameObject panelPrimos;

    public GameObject piedraNumero;
    public int contadorInsPieNum = 0;
    private List<GameObject> piedrasNumInstanciadas = new List<GameObject>();

    public GameObject portalUlam;
    public Interactable iPortalUlam;

    public GameObject ventanaNumerosPrimos;
    public GameObject ventanaEspiralUlam;
    public GameObject botonEjeMis5;

    public static List<Button> botonesPrimos = new List<Button>();

    private HashSet<int> primos1a100 = new HashSet<int>();

    public bool ejeMis5OK = false;

    private Camera cam;

    public GameObject ventanaMensajeUbicacion;
    public Text textoUbicacion;

    public GameObject imagenMonteNatural;
    public bool imgMonteNaturalMostrada = false;

    public Dictionary<Interactable, Dialogo[]> dialogosRandom = new Dictionary<Interactable, Dialogo[]>();

    public GameObject señal1;
    public Interactable iSeñal1;
    public GameObject ventanaCaminoMonteNatural;

    public List<GameObject> tilesAldeaPacaembu = new List<GameObject>();
    public CreadorMapa cm;
    public List<GameObject> tilesMonteNatural = new List<GameObject>();
    public List<GameObject> goAldeaPacaembu = new List<GameObject>();
    public List<GameObject> goMonteNatural = new List<GameObject>();
    public List<GameObject> goCatedral = new List<GameObject>();
    public List<GameObject> tilesCatedral = new List<GameObject>();

    public AdminDialogo ad;

    public GameObject jugador;
    public Jugador j;

    public NPC iNylea;

    public bool enMonteNatural = false; // El jugador viajó al Monte Natural 

    public GameObject goLuzNylea;
    public Light luzNylea;
    public bool efeLuzNyl = false;
    public bool desLuzNyl = false;
    public GameObject nylea;

    public Interactable narrador;

    public List<GameObject> prefabsPiedrasErupcion = new List<GameObject>();
    public List<GameObject> piedrasLavaInstanciadas = new List<GameObject>();

    public GameObject margarita2;
    public GameObject fabricio2;
    public GameObject seneca1;
    public GameObject seneca2;
    public NPC iSeneca;
    public GameObject esbirro1;
    public GameObject esbirro2;
    public GameObject esbirro3;
    public GameObject reliquiaInfinito;
    public GameObject colliderEscenaCombate;
    public GameObject colliderEscenaCombate2;

    public GameObject MNarbol53;
    public GameObject MNarbol54;

    public Text textoDaño;
    public GameObject goTextoDaño;

    public Text textoCura;
    public GameObject goTextoCura;

    public BattleSystem bs;

    public GameObject señal2;
    public Interactable iSeñal2;
    public GameObject ventanaCaminoCatedral;

    public bool detenerErupcion = false;
    public bool enCatedral = false;

    public GameObject imagenCatedral;
    public bool imgCatedralMostrada = false;

    public GameObject patricio;
    public NPC iPatricio;
    public int contadorLeccionesVenn = 0;

    public GameObject obBelzentok;
    public Light luzObBelzentok;
    public GameObject goLuzObBel;
    public bool efeLuzObBel = false;
    public bool desLuzObBel = false;
    public NPC iObBelzentok;

    public GameObject ventanaEntrarCatedral;
    public GameObject pantallaFinal;
    public GameObject pantallaInicial;
    public GameObject pantallaSeleccionPersonaje;

    public Image imagenPersonaje1;
    public Image imagenPersonaje2;
    public bool presionadoPersonaje1 = false;
    public bool presionadoPersonaje2 = false;

    public GameObject goTextoMensajeElecciónPersonaje;
    public Text textoMensajeEleccionPersonaje;
    public InputField campoEleccionNombre;

    public RuntimeAnimatorController animadorPersonaje1;
    public RuntimeAnimatorController animadorPersonaje2;
    public bool usarAnimador1;

    public Image caraJugador;
    public Sprite caraPersonaje1;
    public Sprite caraPersonaje2;
    public Text textoNombre;
    public GameObject pantallaCapitulo1;

    public Image imgPersonajeCombate;
    public Sprite spriteCombate1;
    public Sprite spriteCombate2;

    public GameObject ventanaUsarItem;

    public GameObject[] botonesLecciones;
    public int contadorBotonLecciones;
    public Dictionary<int, GameObject> relacionBotonLeccionInicial = new Dictionary<int, GameObject>();
    public GameObject[] leccionesIniciales; // Lecciones que inician un grupo de lecciones
    public Dictionary<int, GameObject> botonesTerminar = new Dictionary<int, GameObject>();
    public GameObject[] listaBotonesTerminar;
    public GameObject[] botonesCerrar;

    public GameObject goLibroLecciones;
    public GameObject goBotonLibroLecciones;
    public GameObject goInventarioGUI;

    public Text textoMSJEjeMis3;

    public Dialogo dialogoPersonaje; // Diálogo de cuando el jugador se encuentra en medio de la erupción.

    public NPC iJorge;
    public NPC iRaymundo;
    public NPC iMadelena;


    private void Start() // Usado para inicializar las instancias
    {
        FindObjectOfType<AudioManager>().Play("PantallaInicial");

        frutasPrefabs = setFrutasPrefabs; // Guarda el contenido del inspector en la variable estática

        // Agrega las lecciones al diccionario
        lecciones.Add(0, leccion101);
        lecciones.Add(1, leccion102);
        lecciones.Add(2, leccion103);
        lecciones.Add(3, leccion104);
        lecciones.Add(4, leccion105);
        lecciones.Add(5, leccion106);
        lecciones.Add(6, leccion107);
        lecciones.Add(7, leccion108);
        lecciones.Add(8, leccion109);
        lecciones.Add(9, leccion110);
        lecciones.Add(10, leccion111);
        lecciones.Add(11, leccion112);
        lecciones.Add(12, leccion113);
        lecciones.Add(13, leccion114);
        lecciones.Add(14, leccion115);
        lecciones.Add(15, leccion116);
        lecciones.Add(16, leccion117);
        lecciones.Add(17, leccion118);
        lecciones.Add(18, leccion119);
        lecciones.Add(19, leccion120);
        lecciones.Add(20, leccion121);
        lecciones.Add(21, leccion122);
        lecciones.Add(22, leccion123);
        lecciones.Add(23, leccion124);
        lecciones.Add(24, leccion125);
        lecciones.Add(25, leccion126);
        lecciones.Add(26, leccion127);
        lecciones.Add(27, leccion128);
        lecciones.Add(28, leccion129);
        lecciones.Add(29, leccion130);
        lecciones.Add(30, leccion131);
        lecciones.Add(31, leccion132);
        lecciones.Add(32, leccion133);
        lecciones.Add(33, leccion134);
        lecciones.Add(34, leccion135);
        lecciones.Add(35, leccion136);
        lecciones.Add(36, leccion137);
        lecciones.Add(37, leccion138);
        lecciones.Add(38, leccion139);
        lecciones.Add(39, leccion140);
        lecciones.Add(40, leccion141);
        lecciones.Add(41, leccion142);
        lecciones.Add(42, leccion143);
        lecciones.Add(43, leccion144);
        lecciones.Add(44, leccion145);


        // Relaciona los paneles con las frutas para el EjercicioMision2
        imgPanelFrutas.Add(imagenesFrutas[0], panelesFrutas[0]);
        imgPanelFrutas.Add(imagenesFrutas[1], panelesFrutas[0]);
        imgPanelFrutas.Add(imagenesFrutas[2], panelesFrutas[1]);
        imgPanelFrutas.Add(imagenesFrutas[3], panelesFrutas[1]);
        imgPanelFrutas.Add(imagenesFrutas[4], panelesFrutas[2]);
        imgPanelFrutas.Add(imagenesFrutas[5], panelesFrutas[2]);
        imgPanelFrutas.Add(imagenesFrutas[6], panelesFrutas[3]);
        imgPanelFrutas.Add(imagenesFrutas[7], panelesFrutas[3]);
        imgPanelFrutas.Add(imagenesFrutas[8], panelesFrutas[4]);

        luzAzaleia = azaleia.GetComponentInChildren<Light>();

        numParImpar.Add(numeros1a20[0], espaciosVacios[10]); // 1 en 0 de Impar
        numParImpar.Add(numeros1a20[1], espaciosVacios[0]); // 2 en 0 de Par
        numParImpar.Add(numeros1a20[2], espaciosVacios[11]); // 3 en 1 de Impar
        numParImpar.Add(numeros1a20[3], espaciosVacios[1]); // 4 en 1 de Par
        numParImpar.Add(numeros1a20[4], espaciosVacios[12]); // 5 en 2 de Impar
        numParImpar.Add(numeros1a20[5], espaciosVacios[2]); // 6 en 2 de Par
        numParImpar.Add(numeros1a20[6], espaciosVacios[13]); // 7 en 3 de Impar
        numParImpar.Add(numeros1a20[7], espaciosVacios[3]); // 8 en 3 de Par
        numParImpar.Add(numeros1a20[8], espaciosVacios[14]); // 9 en 4 de Impar
        numParImpar.Add(numeros1a20[9], espaciosVacios[4]); // 10 en 4 de Par
        numParImpar.Add(numeros1a20[10], espaciosVacios[15]); // 11 en 5 de Impar
        numParImpar.Add(numeros1a20[11], espaciosVacios[5]); // 12 en 5 de Par
        numParImpar.Add(numeros1a20[12], espaciosVacios[16]); // 13 en 6 de Impar
        numParImpar.Add(numeros1a20[13], espaciosVacios[6]); // 14 en 6 de Par
        numParImpar.Add(numeros1a20[14], espaciosVacios[17]); // 15 en 7 de Impar
        numParImpar.Add(numeros1a20[15], espaciosVacios[7]); // 16 en 7 de Par
        numParImpar.Add(numeros1a20[16], espaciosVacios[18]); // 17 en 8 de Impar
        numParImpar.Add(numeros1a20[17], espaciosVacios[8]); // 18 en 8 de Par
        numParImpar.Add(numeros1a20[18], espaciosVacios[19]); // 19 en 9 de Impar
        numParImpar.Add(numeros1a20[19], espaciosVacios[9]); // 20 en 9 de Par

        for (int i = 0; i < leccionesIniciales.Length; i++)
        {
            relacionBotonLeccionInicial.Add(i, leccionesIniciales[i]);
        }

        for (int i = 0; i < leccionesIniciales.Length; i++) // El número de lecciones iniciales es el mismo de botones Terminar
        {
            botonesTerminar.Add(i, listaBotonesTerminar[i]);
        }

        foreach (GameObject numero in numeros1a20)
        {
            posicionesNumeros.Add(numero, numero.GetComponent<RectTransform>().anchoredPosition);
        }

        foreach (GameObject trozo in trozosNotCom)
        {
            posicionesTrozos.Add(trozo, trozo.GetComponent<RectTransform>().anchoredPosition);
        }

        foreach (GameObject fruta in imagenesFrutas)
        {
            posicionesFrutas.Add(fruta, fruta.GetComponent<RectTransform>().anchoredPosition);
        }

        EncontrarNumerosPrimos();

        cam = Camera.main;

        MostrarMensajeUbicacion("Aldea Principal de Pacaembú");

        dialogosRandom.Add(iSeñal1, iSeñal1.dialogos);
        dialogosRandom.Add(iSeñal2, iSeñal2.dialogos);
        dialogosRandom.Add(iJorge, iJorge.dialogos);
        dialogosRandom.Add(iRaymundo, iRaymundo.dialogos);
        dialogosRandom.Add(iMadelena, iMadelena.dialogos);

    }

    private void Update() // No se requiere por el momento
    {
        ClicSobreInteractable();
    }

    // Instancia los prefabs de las frutas de la primera mision en posiciones al azar
    public static void InstanciarFrutas()
    {
        float maxY = 22f;
        float minY = -3f;
        float maxX = 25f;
        float minX = -9f;


        for (int i = 0; i < frutasPrefabs.Length; i++)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);

            Instantiate(frutasPrefabs[i], new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    public IEnumerator InsPieNum()
    {
        float maxY = 6f;
        float minY = 4f;
        float maxX = 16f;
        float minX = 0f;

        GameObject piedra;

        for (int i = 1; i <= 100; i++)
        {
            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);

            piedra = Instantiate(piedraNumero, new Vector3(x, y, 0), Quaternion.identity);
            piedra.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = i.ToString();
            contadorInsPieNum++;
            piedrasNumInstanciadas.Add(piedra);
            yield return new WaitForSeconds(0.3f);
        }
        luzCarmin.intensity = 0;
        luzCarmin.range = 0;
    }

    public IEnumerator DestruirPieNum()
    {
        foreach (GameObject piedra in piedrasNumInstanciadas)
        {
            Destroy(piedra);
            yield return new WaitForSeconds(0.3f);
        }

    }

    public void InstanciarPiedrasNumeros()
    {
        StartCoroutine(InsPieNum());
    }

    public void DestruirPiedrasNumeros()
    {
        StartCoroutine(DestruirPieNum());
    }


    IEnumerator TransMovPrimos(Animator animadorPrimo)
    {
        while (true)
        { 
            animadorPrimo.SetBool("mover", true);
            yield return new WaitForSeconds(1.5f);
            animadorPrimo.SetBool("mover", false);
            yield return new WaitForSeconds(1.5f);
        }
    }
        

    public void TransicionMovimientoPrimos(Animator animadorPrimo)
    {
        StartCoroutine(TransMovPrimos(animadorPrimo));
    }

    // Corutina para mostrar mensaje del sistema por cinco segundos
    // No funciona cuando se lanzan varios mensajes al mismo tiempo :(
    IEnumerator MostrarMSJ(string mensaje)
    {
        ventanaMensajes.SetActive(true);
        textoMensajes.text = mensaje;
        yield return new WaitForSeconds(5);
        ventanaMensajes.SetActive(false);
    }

    IEnumerator MostrarMSJUbicacion(string mensaje)
    {
        ventanaMensajeUbicacion.SetActive(true);
        textoUbicacion.text = mensaje;
        yield return new WaitForSeconds(5);
        ventanaMensajeUbicacion.SetActive(false);
    }

    public void MostrarMensajeUbicacion(string mensaje)
    {
        StartCoroutine(MostrarMSJUbicacion(mensaje));
    }


    IEnumerator MostrarIMGMonteNatural()
    {
        imagenMonteNatural.SetActive(true);
        yield return new WaitForSeconds(7);
        imagenMonteNatural.SetActive(false);
        imgMonteNaturalMostrada = true;
    }

    public void MostrarImagenMonteNatural()
    {
        StartCoroutine(MostrarIMGMonteNatural());
    }

    IEnumerator MostrarScreenCapitulo1()
    {
        pantallaCapitulo1.SetActive(true);
        yield return new WaitForSeconds(6);
        pantallaCapitulo1.SetActive(false);
        FindObjectOfType<AudioManager>().Stop("PantallaInicial");
        FindObjectOfType<AudioManager>().Play("Aldea");
    }

    public void MostrarPantallaCapitulo1()
    {
        StartCoroutine(MostrarScreenCapitulo1());
    }

    IEnumerator MostrarIMGCatedral()
    {
        imagenCatedral.SetActive(true);
        yield return new WaitForSeconds(7);
        imagenCatedral.SetActive(false);
        imgCatedralMostrada = true;
    }

    public void MostrarImagenCatedral()
    {
        StartCoroutine(MostrarIMGCatedral());
    }

    // Corutina para mostrar y ocultar mensaje después de tres segundos cuando el jugador tiene un error en EjercicioMision3
    IEnumerator MostrarMSJSigueIntentando(List<string> frases)
    {
        msjSigueIntentando.SetActive(true);
        foreach (string frase in frases)
        {
            textoMSJEjeMis3.text = frase;
            yield return new WaitForSeconds(3);
        }
        msjSigueIntentando.SetActive(false);
    }

    IEnumerator MostrarMSJEjeMis4(string msj)
    {
        msjEjeMis4.SetActive(true);
        textoMsjEjeMis4.text = msj;
        yield return new WaitForSeconds(2);
        msjEjeMis4.SetActive(false);
    }

    IEnumerator MostrarMSJEleccionPersonaje(string msj)
    {
        goTextoMensajeElecciónPersonaje.SetActive(true);
        textoMensajeEleccionPersonaje.text = msj;
        yield return new WaitForSeconds(3);
        goTextoMensajeElecciónPersonaje.SetActive(false);
    }

    void MostrarMensajeEleccionPersonaje(string msj)
    {
        StartCoroutine(MostrarMSJEleccionPersonaje(msj));
    }

    public void MostrarMensajeEjeMis4(string msj)
    {
        StartCoroutine(MostrarMSJEjeMis4(msj));
    }

    // Método que llama a la corutina. Este método se llama desde otras clases que muestran mensajes del sistema.
    public void MostrarMensaje(string mensaje)
    {
        StartCoroutine(MostrarMSJ(mensaje));
    }

    // Curva que determina la cantidad de experiencia requerida para subir de nivel en cada nivel
    public float GetExperienciaRequerida(int nivel)
    {
        return Mathf.Floor(100 * nivel * Mathf.Pow(nivel, 0.5f));
    }

    // Curva que determina la cantidad de salud maxima por nivel
    public float GetSaludMax(int nivel)
    {
        return Mathf.Floor(100 * nivel * Mathf.Pow(nivel, 0.3f));
    }

    public float GetDañoMax(int nivel)
    {
        return Mathf.Floor(10 * nivel * Mathf.Pow(nivel, 0.5f));
    }

    public float GetDañoMin(int nivel)
    {
        return Mathf.Floor(5 * nivel * Mathf.Pow(nivel, 0.5f));
    }

    // Muestra la sigueinte lección y oculta la presente
    // Se llama desde los botones Siguiente >> de cada lección
    public void MostrarSiguienteLeccion(int i)
    {
        if (i > 0)
            lecciones[i - 1].SetActive(false);
        botonInicioLeccion1.SetActive(false);
        lecciones[i].SetActive(true);
        leccionesMostradas++;
    }

    // Oculta la lección actual para volver al mapa
    // Se llama desde los botones Terminar >> de cada lección
    public void TerminarLeccion(int i)
    {
        lecciones[i].SetActive(false);
        botonesLecciones[contadorBotonLecciones].SetActive(true);
        leccionesTerminadas++;
        contadorBotonLecciones++;
    }

    // Verifica si la respuesta del EjercicioMision3 es correcta
    // Si es así, asigna true a ejeMis3OK para continuar con la secuencia de la clase Jugador
    // Si no es correcta, la ventana no se cierra y llama a la corutina correspondiente
    public void VerificarEjeMis3()
    {
        List<string> pistas = new List<string>();
        pistas.Add("Tienes algún error");
        pistas.Add("amarilla, roja, verde, morada, anaranjada y marrón");
        pistas.Add("Las tildes y las mayúsculas importan en este mundo");
        pistas.Add("Presta atención a la ortografía");

        var respuestaA = "{x|x es una fruta amarilla";
        var respuestaB = "{x|x es una fruta roja";
        var respuestaC = "{x|x es una fruta verde";
        var respuestaD = "{x|x es una fruta morada";
        var respuestaE = "{x|x es una fruta anaranjada";
        var respuestaF = "{x|x es una fruta marrón";

        if (
            inputA.text == respuestaA &&
            inputB.text == respuestaB &&
            inputC.text == respuestaC &&
            inputD.text == respuestaD &&
            inputE.text == respuestaE &&
            inputF.text == respuestaF
            )
        {
            ejeMis3OK = true;
        }
        else
        {
            ejeMis3OK = false;
            StartCoroutine(MostrarMSJSigueIntentando(pistas));
        }
        goInventarioGUI.SetActive(false);
    }

    public void VerificarEjeMis4()
    {
        if (contadorDropsTrozos < 16)
        {
            MostrarMensajeEjeMis4("Te falta mover algunos trozos. Continúa.");
        }

        else if (
            trozosNotCom[0].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[0].GetComponent<RectTransform>().anchoredPosition &&

            (trozosNotCom[1].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[1].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[1].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[4].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[1].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[9].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[1].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[12].GetComponent<RectTransform>().anchoredPosition) &&

            (trozosNotCom[2].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[2].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[2].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[10].GetComponent<RectTransform>().anchoredPosition) &&

            (trozosNotCom[3].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[3].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[3].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[11].GetComponent<RectTransform>().anchoredPosition) &&

            (trozosNotCom[4].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[1].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[4].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[4].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[4].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[9].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[4].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[12].GetComponent<RectTransform>().anchoredPosition) &&

            trozosNotCom[5].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[5].GetComponent<RectTransform>().anchoredPosition &&

            (trozosNotCom[6].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[6].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[6].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[14].GetComponent<RectTransform>().anchoredPosition) &&

            (trozosNotCom[7].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[7].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[7].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[15].GetComponent<RectTransform>().anchoredPosition) &&


            trozosNotCom[8].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[8].GetComponent<RectTransform>().anchoredPosition &&

            (trozosNotCom[9].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[1].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[9].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[4].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[9].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[9].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[9].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[12].GetComponent<RectTransform>().anchoredPosition) &&

            (trozosNotCom[10].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[2].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[10].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[10].GetComponent<RectTransform>().anchoredPosition) &&

            (trozosNotCom[11].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[3].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[11].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[11].GetComponent<RectTransform>().anchoredPosition) &&

            (trozosNotCom[12].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[1].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[12].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[4].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[12].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[9].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[12].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[12].GetComponent<RectTransform>().anchoredPosition) &&

            trozosNotCom[13].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[13].GetComponent<RectTransform>().anchoredPosition &&

            (trozosNotCom[14].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[6].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[14].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[14].GetComponent<RectTransform>().anchoredPosition) &&

            (trozosNotCom[15].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[7].GetComponent<RectTransform>().anchoredPosition ||
            trozosNotCom[15].GetComponent<RectTransform>().anchoredPosition == espaciosDropTrozos[15].GetComponent<RectTransform>().anchoredPosition)
            )
        {
            dragdrop2 = true;
            ventanaEjeMis4a.SetActive(false);
        }
        else
        {
            MostrarMensajeEjeMis4("Algo anda mal. Inicia otra vez");
            foreach (GameObject trozo in trozosNotCom)
            {
                trozo.GetComponent<RectTransform>().anchoredPosition = posicionesTrozos[trozo];
            }
        }
    }

    public void ReiniciarEjeMis2()
    {
        foreach (GameObject fruta in imagenesFrutas)
        {
            fruta.GetComponent<RectTransform>().anchoredPosition = posicionesFrutas[fruta];
        }
        foreach (RectTransform panel in panelesFrutas)
        {
            panel.GetComponent<ItemDropHandler>().contadorDrops = 0;
        }
    }

    public void ReiniciarEjeMis4a()
    {
        foreach (GameObject num in posicionesNumeros.Keys)
        {
            num.GetComponent<RectTransform>().anchoredPosition = posicionesNumeros[num];
        }
        contadorDropsNumeros = 0;
    }

    public void ReiniciarEjeMis4b()
    {
        foreach (GameObject trozo in trozosNotCom)
        {
            trozo.GetComponent<RectTransform>().anchoredPosition = posicionesTrozos[trozo];
        }
        contadorDropsTrozos = 0;
    }

    IEnumerator HechizoAzaleia()
    {
        frutasAzaleia.SetActive(true);
        yield return new WaitForSeconds(3);
        while (contadorHechizoAzaleia < 30)
        {
            frutasAzaleia.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            frutasAzaleia.SetActive(false);
            numerosAzaleia.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            numerosAzaleia.SetActive(false);

            contadorHechizoAzaleia++;
        }
        numerosAzaleia.SetActive(true);
        yield return new WaitForSeconds(5);
        numerosAzaleia.SetActive(false);
        hechizoAzaTerminado = true;
    }

    public void LanzarHechizoAzaleia()
    {
        StartCoroutine(HechizoAzaleia());
    }

    IEnumerator LuzHechizoAzaleia()
    {
        var r = 0;
        var i = 0;
        while (r <= 10 && i <= 100)
        {
            luzAzaleia.intensity = i;
            luzAzaleia.range = r;
            i += 5;
            yield return new WaitForSeconds(0.1f);
            if (i % 10 == 0)
            {
                r++;
                yield return new WaitForSeconds(0.5f);
            }
        }
        efeLuzAza = true;
    }

    IEnumerator LanzarLuzObBelzentok()
    {
        var i = 0;
        while (i <= 500)
        {
            luzObBelzentok.intensity = i;
            i += 5;
            yield return new WaitForSeconds(0.05f);
        }
        efeLuzObBel = true;
    }

    IEnumerator DeshacerLuzObBelzentok()
    {
        var i = 500;
        while (i >= 0)
        {
            luzObBelzentok.intensity = i;
            i -= 5;
            yield return new WaitForSeconds(0.05f);
        }
        desLuzObBel = true;
    }

    public void ManejarLuzObBelzentok(int i)
    {
        if (i == 0)
            StartCoroutine(LanzarLuzObBelzentok());
        if (i == 1)
            StartCoroutine(DeshacerLuzObBelzentok());
    }


    IEnumerator LanzarLuzNylea()
    {
        var i = 0;
        while (i <= 500)
        {
            luzNylea.intensity = i;
            i += 5;
            yield return new WaitForSeconds(0.05f);
        }
        efeLuzNyl = true;
        desLuzNyl = false;
    }

    IEnumerator DeshacerLuzNylea()
    {
        var i = 500;
        while (i >= 0)
        {
            luzNylea.intensity = i;
            i -= 5;
            yield return new WaitForSeconds(0.05f);
        }
        desLuzNyl = true;
        efeLuzNyl = false;
    }

    public void ManejarLuzNylea(int i)
    {
        if (i == 0)
            StartCoroutine(LanzarLuzNylea());
        if (i == 1)
            StartCoroutine(DeshacerLuzNylea());
    }

    IEnumerator DeshacerLuzAzaleia()
    {
        var r = 11;
        var i = 100;
        while (r >= 0 && i >= 0)
        {
            luzAzaleia.intensity = i;
            luzAzaleia.range = r;
            i -= 5;
            yield return new WaitForSeconds(0.1f);
            if (i % 10 == 0)
            {
                r--;
                yield return new WaitForSeconds(0.5f);
            }
        }
        desLuzAza = true;
    }

    public void LanzarLuzHechizoAzaleia()
    {
        StartCoroutine(LuzHechizoAzaleia());
    }

    public void TerminarLuzAzaleia()
    {
        StartCoroutine(DeshacerLuzAzaleia());
    }

    public void VerificarEjercicioPrimos()
    {
        int contador = 0;
        foreach (Button boton in botonesPrimos)
        {
            if (primos1a100.Contains(int.Parse(boton.GetComponentInChildren<Text>().text)))
            {
                contador++;
            }
        }
        if (contador == 25)
        {
            ejeMis5OK = true;
        }
    }

    public void EncontrarNumerosPrimos()
    {
        for (int i = 2; i <= 100; i++)
        {
            if (i == 2)
            {
                primos1a100.Add(i);
                continue;
            }
            if (i % 2 == 0)
                continue;
            int k = 0;
            for (int j = 3; j <= i; j++)
                if (i % j == 0)
                    k++;
            if (k == 1)
                primos1a100.Add(i);          
        }
    }

   public void ClicSobreInteractable()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            if (hit.collider)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    try
                    {
                        if (interactable.interactableTag == "cateVitral0")
                        {
                            leccion133.SetActive(true);
                            contadorLeccionesVenn++;
                        }
                        else if (interactable.interactableTag == "cateVitral1")
                        {
                            leccion134.SetActive(true);
                            contadorLeccionesVenn++;
                        }
                        else if (interactable.interactableTag == "cateVitral2")
                        {
                            leccion135.SetActive(true);
                            contadorLeccionesVenn++;
                        }
                        else if (interactable.interactableTag == "cateVitral3")
                        {
                            leccion137.SetActive(true);
                            contadorLeccionesVenn++;
                        }
                        else if (interactable.interactableTag == "cateVitral4")
                        {
                            leccion140.SetActive(true);
                            contadorLeccionesVenn++;
                        }
                        else if (interactable.interactableTag == "cateVitral5")
                        {
                            leccion143.SetActive(true);
                            contadorLeccionesVenn++;
                        }
                        else if (interactable.interactableTag == "PuertaCatedral")
                            ventanaEntrarCatedral.SetActive(true);
                        else if (interactable.interactableTag == "SeñalCaminoMonteNatural")
                            ventanaCaminoMonteNatural.SetActive(true);
                        else if (interactable.interactableTag == "SeñalCaminoCatedral")
                            ventanaCaminoCatedral.SetActive(true);
                        else
                            interactable.LanzarDialogo(Random.Range(0, dialogosRandom[interactable].Length));
                    }
                    catch (KeyNotFoundException)
                    {
                        return;
                    }
                }
            }
        }
    }

    public void CerrarVentanaCaminoMonteNatural()
    {
        ventanaCaminoMonteNatural.SetActive(false);
        ad.TerminarDialogo();
    }

    public void CerrarVentanaCaminoCatedral()
    {
        ventanaCaminoCatedral.SetActive(false);
        ad.TerminarDialogo();
    }

    public void ViajarAlMonteNatural()
    {
        ventanaCaminoMonteNatural.SetActive(false);
        ad.TerminarDialogo();

        foreach (GameObject go in goAldeaPacaembu)
            go.SetActive(false);
        foreach (GameObject tile in tilesAldeaPacaembu)
            tile.SetActive(false);

        cm.GenerateMap(1, tilesMonteNatural);
        foreach (GameObject go in goMonteNatural)
            go.SetActive(true);
        MostrarMensajeUbicacion("Monte Natural");

        jugador.GetComponent<Transform>().position = new Vector3(27.92666f, -5.491231f, 0);
        FindObjectOfType<AudioManager>().Stop("Aldea");
        FindObjectOfType<AudioManager>().Play("MonteNatural");

        enMonteNatural = true;
        carminClone.SetActive(false);
    }


    public void ViajarALaCatedral()
    {
        ventanaCaminoCatedral.SetActive(false);
        ad.TerminarDialogo();

        foreach (GameObject go in goMonteNatural)
            go.SetActive(false);
        foreach (GameObject tile in tilesMonteNatural)
            tile.SetActive(false);

        cm.GenerateMap(2, tilesCatedral);
        foreach (GameObject go in goCatedral)
            go.SetActive(true);
        MostrarMensajeUbicacion("Catedral de los Diagramas de Venn");

        jugador.GetComponent<Transform>().position = new Vector3(-10, -5, 0);
        FindObjectOfType<AudioManager>().Stop("MonteNatural");
        FindObjectOfType<AudioManager>().Play("Catedral");

        nylea.SetActive(false);

        enCatedral = true;
    }


    public IEnumerator Erupcion()
    {
        float maxY = 32f;
        float minY = -3f;
        float maxX = 25f;
        float minX = -9f;

        GameObject piedra;
        var num = 0;
        var i = 0;

        while (detenerErupcion == false)
        {
            num++;

            if (num % 10 == 0)
            {
                goMonteNatural[i].SetActive(false);
                i++;
            }

            if (num == 40)
            {
                narrador.LanzarDialogo(3);
            }

            float x = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);

            piedra = Instantiate(prefabsPiedrasErupcion[Random.Range(0, 3)], new Vector3(x, y, 0), Quaternion.identity);
            piedra.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text = num.ToString();
            piedrasLavaInstanciadas.Add(piedra);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void IniciarErupcion()
    {
        FindObjectOfType<AudioManager>().Play("Erupcion");
        FindObjectOfType<AudioManager>().Play("Fuego");
        StartCoroutine(Erupcion());
    }

    IEnumerator MostrarDamage(int daño)
    {
        goTextoDaño.SetActive(true);
        textoDaño.text = "- " + daño.ToString();
        yield return new WaitForSeconds(2f);
        goTextoDaño.SetActive(false);
    }

    public void MostrarDaño(int daño)
    {
        FindObjectOfType<AudioManager>().Play("Daño");
        StartCoroutine(MostrarDamage(daño));
    }

    IEnumerator MostrarHeal(int cura)
    {
        goTextoCura.SetActive(true);
        textoCura.text = "+ " + cura.ToString();
        yield return new WaitForSeconds(2f);
        goTextoCura.SetActive(false);
    }

    public void MostrarCura(int cura)
    {
        FindObjectOfType<AudioManager>().Play("Cura");
        bs.HUDjugador.SetHP(j.saludActual);
        StartCoroutine(MostrarDamage(cura));
    }

    public void EntrarALaCatedral()
    {
        ventanaEntrarCatedral.SetActive(false);
        jugador.GetComponent<Transform>().position = new Vector3(8, 11, 0);
    }

    public void BotonNOPuertaCatedral()
    {
        ventanaEntrarCatedral.SetActive(false);
    }

    public void OnBotonIniciarPartida()
    {
        pantallaInicial.SetActive(false);
        pantallaSeleccionPersonaje.SetActive(true);
    }

    public void OnBotonEscogerPersonaje1()
    {
        Color color = new Color();
        if (presionadoPersonaje1 == false)
        {
            presionadoPersonaje1 = true;
            if (ColorUtility.TryParseHtmlString("#56E517", out color))
                imagenPersonaje1.color = color;
            presionadoPersonaje2 = false;
            if (ColorUtility.TryParseHtmlString("#311C0B", out color))
                imagenPersonaje2.color = color;
            return;
        }
        if (presionadoPersonaje1 == true)
        {
            presionadoPersonaje1 = false;
            if (ColorUtility.TryParseHtmlString("#311C0B", out color))
                imagenPersonaje1.color = color;
        }
    }
    public void OnBotonEscogerPersonaje2()
    {
        Color color = new Color();
        if (presionadoPersonaje2 == false)
        {
            presionadoPersonaje2 = true;
            if (ColorUtility.TryParseHtmlString("#56E517", out color))
                imagenPersonaje2.color = color;
            presionadoPersonaje1 = false;
            if (ColorUtility.TryParseHtmlString("#311C0B", out color))
                imagenPersonaje1.color = color;
            return;
        }
        if (presionadoPersonaje2 == true)
        {
            presionadoPersonaje2 = false;
            if (ColorUtility.TryParseHtmlString("#311C0B", out color))
                imagenPersonaje2.color = color;
        }
    }

    public void OnBotonIniciar()
    {
        if (presionadoPersonaje1 == false && presionadoPersonaje2 == false)
        {
            MostrarMensajeEleccionPersonaje("Escoge un personaje");
            return;
        }
        else if (campoEleccionNombre.text == "")
        {
            MostrarMensajeEleccionPersonaje("Escoge un nombre para tu personaje");
            return;
        }
        if (presionadoPersonaje1 == true)
        {
            usarAnimador1 = true;
            caraJugador.sprite = caraPersonaje1;
            imgPersonajeCombate.sprite = spriteCombate1;
            dialogoPersonaje.caraNPC = caraPersonaje1;
        }
        else
        {
            usarAnimador1 = false;
            caraJugador.sprite = caraPersonaje2;
            imgPersonajeCombate.sprite = spriteCombate2;
            dialogoPersonaje.caraNPC = caraPersonaje2;
        }
        j.nombre = campoEleccionNombre.text;
        textoNombre.text = campoEleccionNombre.text;
        dialogoPersonaje.nombre = j.nombre;
        pantallaSeleccionPersonaje.SetActive(false);
        MostrarPantallaCapitulo1();
        goInventarioGUI.SetActive(false);
    }

    public void OnBotonLibroLecciones(int i)
    {
        relacionBotonLeccionInicial[i].SetActive(true);
        botonesTerminar[i].SetActive(false);
        botonesCerrar[i].SetActive(true);
        goLibroLecciones.SetActive(false);
    }

    public void AbrirLibroLecciones()
    {
        goLibroLecciones.SetActive(true);
    }

    public void CerrarLibroLecciones()
    {
        goLibroLecciones.SetActive(false);
    }

    public void CerrarLeccion(int i)
    {
        lecciones[i].SetActive(false);
        goLibroLecciones.SetActive(true);
    }
}
