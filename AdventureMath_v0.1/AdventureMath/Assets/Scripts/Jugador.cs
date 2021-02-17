using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


/* Esta CLASE contiene los atributos y métodos básicos del jugador.
 * Además incluye una serie de variables del tipo bool (flags) que 
 * determinan la secuencia del juego. El flujo de secuencia del juego 
 * se encuentra en el método Update() que se llama cada frame. Era ideal
 * manejar esto únicamente desde la clase AdminJuego, pero no hubo claridad
 * al principio del desarrollo y se tuvo que continuar en esta clase.
     */
public class Jugador : MonoBehaviour // Monobehaviour es la clase base de Unity
{
    public string nombre;

    // Variables para el movimiento
    [SerializeField] // Permite la visualización de un atributo privado en el inspector
    private float speed;
    private Animator animador; // Animator es una interfaz que controla las animaciones
    private Vector2 direccion;

    // Usadas en el Raycast para identificar colliders
    private Camera cam;

    // Se asigna cada vez que el jugador hace clic en un objeto interactable y que tenga el componente collider
    public Interactable focus; // Interactable es la clase de la cuál heredan el componente ItemPickUp y la clase NPC

    // Atributos de la clase Jugador
    public int saludActual; // No se usa aún, pero será útil cuándo se incluyan combates
    public int saludMax; // Salud máxima por nivel
    public int experiencia = 0;
    public int xpRequerida = 100;
    public int xpRequeridaAnterior = 0;
    public int fibonaccis = 0; // Moneda del juego
    public int nivel = 1;

    public int dañoMin;
    public int dañoMax;

    public static Mision mision; // La misión actual del Jugador
    public Mision setMision; // Creada para visualizar una variable estática en el inspector

    // Instancias para actualizar la GUI de las estádisticas del jugador en la parte superior izquierda
    public BarraProgreso barraXP;
    public BarraProgreso barraSalud;
    public Text fib;
    public Text textoNivel;
    public Text textoXPactual;
    public Text textoXPmax;
    public Text textoHPactual;
    public Text textoHPmax;

    // Instancia para acceder a los métodos, atributos, instancias y variables de la clase AdminJuego
    public AdminJuego aj;

    // Instancias usadas principalmente para acceder a los diálogos de cada NPC y usarlos en la secuencia
    public NPC margarita;
    public NPC fabricio;
    public NPC carmin;
    public NPC azaleia;

    // Fue necesario recurrir a estos clones ocultos de cada NPC para acceder a sus misiones
    // No fue posible acceder a estas misiones desde las instancias originales por alguna razón
    public NPC fabricioTest;
    public NPC carminTest;
    public Interactable placaPar;

    public Inventory inventario; // Referencia del Componente Inventario

    public BarraProgreso barraHPjugadorHUD;
    public BarraProgreso barraXPjugadorHUD;

    //  Las siguientes son FLAGS que controlan el flujo de secuencia del juego
    // Ver siginificado de cada una

    public bool dia0Mar = false; // Dialogo 0 Margarita
    public bool dia1Mar = false; // Dialogo 1 Margarita
    public bool mis1Mar = false; // Mision 1 Margarita
    public bool frutasInstanciadas = false;
    public bool dia2Mar = false; // Dialogo 2 Margarita
    public bool dia3Mar = false; // Dialogo 3 Margarita
    public bool dia4Mar = false; // Dialogo 4 Margarita
    public bool rec1Mar = false; // Recompensa Mision 1 Margarita
    public static bool interactuando = false; // Creada para determinar cuándo el jugador está interactuando
                                              // Se utilizó al principio del flujo pero no era necesaria realmente
    public static bool haRecogido = false; // Determina si el jugador ha recogido items en la misión 1
                                           // Se creó para crear flujos alternos pero no fue muy bien utilizada
    public bool botLec1 = false; // Boton Iniciar Leccion 1 Apareció
    public bool fabInstanciado = false; // Fabricio es visible en el juego (no instanciado realmente, pues no se usó el prefab al final)
    public bool dia0Fab = false; // Dialogo 0 Fabricio
    public bool sandiaInstanciada = false;
    public bool dia6Mar = false; // Dialogo 6 Margarita
    public bool dia1Fab = false; // Dialogo 1 Fabricio
    public bool lec104 = false; // Ventana Leccion 1.04 visible
    public bool dia2Fab = false; // Dialogo 2 Fabricio
    public bool lec105 = false; // Ventana Leccion 1.05 visible
    public bool dia3Fab = false; // Dialogo 3 Fabricio
    public bool dia4Fab = false; // Dialogo 4 Fabricio
    public bool dia7Mar = false; // Dialogo 7 Margarita
    public bool mis2Fab = false; // Ventana Misión 2 Fabricio visible
    public bool venEjeMis2 = false; // Ventana Ejercicio Misión 2 Fabricio visible
    public bool mis2FabOK = false; // Misión 2 Fabricio fue completada
    public bool dia5Fab = false; // Dialogo 5 Fabricio
    public bool lec109 = false; // Ventana Leccion 1.09 visible
    public bool dia6Fab = false; // Dialogo 6 Fabricio
    public bool dia8Mar = false; // Dialogo 8 Margarita
    public bool dia0Car = false; // Dialogo 0 Carmín
    public bool dia1Car = false; // Dialogo 1 Carmín
    public bool dia2Car = false; // Dialogo 2 Carmín
    public bool lec113 = false; // Ventana Leccion 1.13 visible
    public bool lec114 = false; // Ventana Leccion 1.14 visible
    public bool dia3Car = false; // Dialogo 3 Carmín
    public bool mis3Car = false; // Ventana Misión 3 Carmín visible
    public bool venEjeMis3 = false; // Ventana Ejercicio Misión 3 Carmín visible
    public bool rec3Car = false; // Recompensa Misión 3 Carmín
    public bool dia4Car = false; // Diálogo 4 Carmín
    public bool azaleiaVisible = false;
    public bool dia5Car = false; // Diálogo 5 Carmín
    public bool dia0Aza = false; // Diálogo 0 Azaleia
    public bool animacionLuzAzaleia = false;
    public bool hechizoAzaleia = false; // Animación transformación de frutas a números
    public bool inicioDeshLuzAza = false; // Se empezó a deshacer la luz de Azaleia
    public bool dia2Aza = false; // Diálogo 2 Azaleia
    public bool ocultarAzaleia = false;
    public bool dia6Car = false; // Diálogo 6 Carmín
    public bool lec115 = false; // Ventana Lección 1.15 visible
    public bool dia7Car = false; // Diálogo 7 Carmín
    public bool lec119 = false; // Ventana Lección 1.19 visible
    public bool dia8Car = false; // Diálogo 8 Carmín
    public bool placasVisibles = false;
    public bool dia0PlacaPar = false; // Diálogo 0 Placa Par
    public bool dia0PlacaImpar = false; // Diálogo 0 Placa Impar
    public bool lec121 = false; // Ventana Lección 1.21 visible
    public bool lec123 = false; // Ventana Lección 1.22 visible
    public bool carminDesplazamiento0 = false; // Carmín se desplaza desde el Lago Tornasol hasta el lugar de las Placas
    public bool mis4Placas = false; // Ventana misión placas visible
    public bool venEjeMis4 = false; // Ventana Ejercicio Misión 4 Placas visible
    public bool venEje2Mis4 = false; // Ventana de la segunda parte del Ejercicio Misión 4 Placas visible
    public bool rec4Placas = false; // Recompensa Misión 4 Placas
    public bool dia10Car = false; // Diálogo 10 Carmín
    public bool dia11Car = false; // Diálogo 11 Carmín
    public bool numerosPrimosVisibles = false;
    public bool lec125 = false; // Ventana Lección 1.25 visible
    public bool dia12Car = false; // Diálogo 12 Carmín
    public bool hechizoCarmin = false; // Hechizo en el que Carmín extiende los números hasta 100
    public bool dia13Car = false; // Diálogo 13 Carmín
    public bool piedrasDestruidas = false;
    public bool dia0Por = false; // Diálogo 0 Portal Ulam
    public bool mis5Portal = false; // Ventanas Misión 5 Portal
    public bool ventanasMision5 = false; // Dos ventanas Ejercicio Misión 5 Portal visibles
    public bool rec5Portal = false; // Recompensa Misión 5 Portal
    public bool dia14Car = false; // Diálogo 14 Carmín
    public bool lec126 = false; // Ventana Lección 1.26 visible
    public bool dia15Car = false; // Diálogo 15 Carmin
    public bool lec128 = false; // Ventana Lección 1.28 visible
    public bool dia16Car = false; // Diálogo 16 Carmin
    public bool imgMonteNatural = false; // Imagen Monte Natural Visible
    public bool dia17Car = false; // Diálogo 17 Carmin
    public bool señal1visible = false;
    public bool dia0Nyl = false; // Diálogo 0 Nylea
    public bool nyleaOculta = false; // Para este punto del juego Nylea está visible y solo queremos ocultarla
    public bool iniLuzNylea = false; // Inicio Luz Nylea
    public bool iniDeshLuzNylea = false; // Inicio Deshacer Luz Nylea
    public bool dia0Narr = false; // Dialogo 0 Narración
    public bool inicioErupcion = false;
    public bool dia1Narr = false; // Dialogo 1 Narración
    public bool dia2Narr = false; // Dialogo 2 Narración
    public bool dia3Narr = false; // Dialogo 3 Narración
    public bool escenaCombateVisible = false; // Sprites de Margarita, Fabricio, Seneca y esbirros
    public bool dia7Fab = false; // Dialogo 7 Fabricio
    public bool dia9Mar = false; // Dialogo 9 Margarita
    public bool dia0Sen = false; // Dialogo 0 Seneca
    public bool colliderCombateOculto = false;
    public bool inicioBatalla = false;
    public bool batallaFinalizada = false;
    public bool dia1Nyl = false; // Diálogo 1 Nylea 
    public bool inicioLuzNylea2 = false;
    public bool restauracionBosque = false;
    public bool iniDeshNylea2 = false;
    public bool señal2visible = false;
    public bool dia2Nyl = false; // Diálogo 2 Nylea
    public bool dia3Nyl = false; // Diálogo 3 Nylea
    public bool dia0Pat = false; // Dialogo 0 Patricio
    public bool obBelVisible = false;
    public bool dia0ObBel = false; // Diálogo 0 Ob Belzentok
    public bool obBelOculto = false;
    public bool pantallaFinalVisible = false;
    public bool musicaFinal = false;
    public bool aparicionNylea2 = false;


    public float posX;
    public float posY;
    public Transform transformJugador;

    // Start is called before the first frame update. Usado para inicializar las instancias.
    public void Start()
    {
        cam = Camera.main;

        animador = GetComponent<Animator>(); // Se accede al Componente Animator y lo guarda en animador

        inventario = GetComponent<Inventory>(); // Se accede al Componente Inventario y lo guarda en inventario

        // Establece los valores de la GUI de estadísticas del jugador al inicio del juego
        barraXP.actual = 0;
        barraXP.minimo = 0;
        barraXP.maximo = 100;
        saludMax = 100;
        saludActual = 100;
        barraSalud.minimo = 0;
        barraSalud.maximo = saludMax;
        barraSalud.actual = saludActual;
        textoNivel.text = nivel.ToString();
        fib.text = fibonaccis.ToString();
        textoHPactual.text = saludActual.ToString();
        textoHPmax.text = saludMax.ToString();
        textoXPactual.text = "0";
        textoXPmax.text = "100";

        dañoMax = (int)aj.GetDañoMax(nivel);
        dañoMin = (int)aj.GetDañoMin(nivel);

    }

    // Update is called once per frame
    // En este método se incluyó el flujo de secuencia que debió ir en AdminJuego
    public void Update()
    {
        if (aj.usarAnimador1)
            GetComponent<Animator>().runtimeAnimatorController = aj.animadorPersonaje1;
        else
            GetComponent<Animator>().runtimeAnimatorController = aj.animadorPersonaje2;

        setMision = mision; // Se visualiza la misión actual en el inspector

        GetInput(); // Método que verifica qué flecha está presionando el jugador
        Mover(); // Mueve el jugador

        // Este if pudo haber sido definido en un método aparte. 
        // Verifica si el jugador hace clic sobre un GameObject Interactable (NPC o Item) con collider
        // Si es así, lo define como el foco del jugador
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            if (hit.collider)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }

        posX = transformJugador.position.x;
        posY = transformJugador.position.y;


        // AQUÍ EMPIEZA EL FLUJO DE SECUENCIA DEL JUEGO
        // Se utilizan los FLAGS definidos más arriba
        // AdminDialogo.hablando se utiliza para lanzar un dialogo sólo cuando el anterior haya terminado
        // Cuando se menciona a focus, es el foco interactable (NPC) sobre el cual el jugador hizo clic
        // Luego se comprendió que no era necesario y además hacía más complejo el flujo.

        if (interactuando)
        {
            if (dia0Mar == false)
            {
                focus.LanzarDialogo(0);
                dia0Mar = true;
            }
            if (dia0Mar == true && dia1Mar == false && mis1Mar == false && AdminDialogo.hablando == false)
            {
                focus.LanzarDialogo(1);
                dia1Mar = true;
            }
            if (dia1Mar == true && mis1Mar == false && AdminDialogo.hablando == false)
            {
                focus.AbrirVentanaMision();
                mis1Mar = true;
            }
            if (mis1Mar == true && setMision.activa == true && frutasInstanciadas == false)
            {
                FindObjectOfType<AudioManager>().Stop("Aldea");
                FindObjectOfType<AudioManager>().Play("Misiones");
                AdminJuego.InstanciarFrutas();
                frutasInstanciadas = true;
            }
            if (dia2Mar == false && frutasInstanciadas == true && haRecogido == true && inventario.items.Count == 0)
            {
                focus.LanzarDialogo(2);
                dia2Mar = true;
            }
            if (dia3Mar == false && frutasInstanciadas == true && haRecogido == true && setMision.objetivoMision.Completado() == false && inventario.items.Count > 0)
            {
                focus.LanzarDialogo(3);
                dia3Mar = true;
            }
            if (dia4Mar == false && frutasInstanciadas == true && setMision.objetivoMision.Completado() == true && inventario.items.Count >= 7)
            {
                focus.LanzarDialogo(4);
                dia4Mar = true;
            }
            if (rec1Mar == false && dia4Mar == true && AdminDialogo.hablando == false)
            {
                FindObjectOfType<AudioManager>().Play("Aldea");
                FindObjectOfType<AudioManager>().Stop("Misiones");
                setMision.Completar();
                rec1Mar = true;
            }
            if (rec1Mar == true && botLec1 == false)
            {
                aj.botonInicioLeccion1.SetActive(true);
                botLec1 = true;
            }          
        }
        if (aj.leccionesTerminadas == 1 && fabInstanciado == false)
        {
            aj.goBotonLibroLecciones.SetActive(true);
            aj.fabricio.SetActive(true);
            margarita.LanzarDialogo(5);
            fabInstanciado = true;
        }
        if (fabInstanciado == true && AdminDialogo.hablando == false && focus.name == "Fabricio" && dia0Fab == false)
        {
            focus.LanzarDialogo(0);
            dia0Fab = true;
        }
        if (dia0Fab == true && sandiaInstanciada == false && AdminDialogo.hablando == false && dia6Mar == false)
        {
            Instantiate(aj.sandia, new Vector3(-3.35f, -4.51f, 0), Quaternion.identity);
            sandiaInstanciada = true;
            margarita.LanzarDialogo(6);
            dia6Mar = true;
        }
        if (dia6Mar == true && dia1Fab == false && AdminDialogo.hablando == false)
        {
            fabricio.LanzarDialogo(1);
            dia1Fab = true;
        }
        if (AdminDialogo.hablando == false && lec104 == false && dia1Fab == true)
        {
            foreach (Item item in inventario.items)
            {
                if (item.name == "Sandia")
                {
                    aj.leccion104.SetActive(true);
                    lec104 = true;
                }
            }   
        }
        if (aj.leccionesTerminadas == 2 && dia2Fab == false)
        {
            fabricio.LanzarDialogo(2);
            dia2Fab = true;
        }
        if (AdminDialogo.hablando == false && dia2Fab == true && lec105 == false)
        {
            aj.leccion105.SetActive(true);
            lec105 = true;
        }
        if (aj.leccionesTerminadas == 3 && lec105 == true && dia3Fab == false)
        {
            fabricio.LanzarDialogo(3);
            dia3Fab = true;
        }
        if (AdminDialogo.hablando == false && dia3Fab == true && dia7Mar == false)
        {
            margarita.LanzarDialogo(7);
            dia7Mar = true;
        }
        if (AdminDialogo.hablando == false && dia7Mar == true && dia4Fab == false)
        {
            fabricio.LanzarDialogo(4);
            dia4Fab = true;
        }
        if (AdminDialogo.hablando == false && dia4Fab == true && mis2Fab == false)
        {
            fabricioTest.AbrirVentanaMision();
            mis2Fab = true;
        }
        if (mis2Fab == true && setMision.activa == true && venEjeMis2 == false)
        {
            FindObjectOfType<AudioManager>().Stop("Aldea");
            FindObjectOfType<AudioManager>().Play("Misiones");
            aj.ventanaEjeMis2.SetActive(true);
            venEjeMis2 = true;
        }
        if (venEjeMis2 == true && mis2FabOK == false)
        {
            var drops = 0;
            {
                foreach (RectTransform panel in aj.panelesFrutas)
                {
                    drops += panel.GetComponent<ItemDropHandler>().contadorDrops;
                    if (drops == 9)
                    {
                        aj.ventanaEjeMis2.SetActive(false);
                        FindObjectOfType<AudioManager>().Play("Aldea");
                        FindObjectOfType<AudioManager>().Stop("Misiones");
                        setMision.Completar();
                        mis2FabOK = true;
                        aj.dragdrop0 = true;
                        break;
                    }
                }
            }
        }
        if (mis2FabOK == true && dia5Fab == false)
        {
            fabricio.LanzarDialogo(5);
            dia5Fab = true;
        }
        if (AdminDialogo.hablando == false && dia5Fab == true && lec109 == false)
        {
            aj.leccion109.SetActive(true);
            lec109 = true;
        }
        if (aj.leccionesTerminadas == 4 && dia6Fab == false)
        {
            fabricio.LanzarDialogo(6);
            dia6Fab = true;
        }
        if (AdminDialogo.hablando == false && dia6Fab == true && dia8Mar == false)
        {
            margarita.LanzarDialogo(8);
            aj.carminClone = Instantiate(aj.carmin, new Vector3(12f, 30.5f, 0), Quaternion.identity);
            dia8Mar = true;
        }
        if (focus.name == "Carmin(Clone)" && dia8Mar == true && dia0Car == false)
        {
            carmin.LanzarDialogo(0);
            aj.margarita.SetActive(false);
            aj.fabricio.SetActive(false);
            dia0Car = true;
        }
        if (AdminDialogo.hablando == false && dia0Car == true && dia1Car == false)
        {
            carmin.LanzarDialogo(1);
            aj.coco.SetActive(true);
            dia1Car = true;
        }
        if (AdminDialogo.hablando == false && dia1Car == true && lec113 == false)
        {
            aj.coco.SetActive(false);
            aj.leccion113.SetActive(true);
            lec113 = true;
        }
        if (aj.leccionesTerminadas == 5 && dia2Car == false)
        {
            carmin.LanzarDialogo(2);
            dia2Car = true;
        }
        if (AdminDialogo.hablando == false && dia2Car == true && lec114 == false)
        {
            aj.leccion114.SetActive(true);
            lec114 = true;
        }
        if (aj.leccionesTerminadas == 6 && dia3Car == false)
        {
            carmin.LanzarDialogo(3);
            dia3Car = true;
        }
        if (AdminDialogo.hablando == false && dia3Car == true && mis3Car == false)
        {
            carmin.AbrirVentanaMision();
            mis3Car = true;
        }
        if (mis3Car == true && setMision.activa == true && venEjeMis3 == false)
        {
            FindObjectOfType<AudioManager>().Stop("Aldea");
            FindObjectOfType<AudioManager>().Play("Misiones");
            aj.ventanaEjeMis3.SetActive(true);
            venEjeMis3 = true;
        }
        if (aj.ejeMis3OK == true && rec3Car == false)
        {
            aj.ventanaEjeMis3.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Aldea");
            FindObjectOfType<AudioManager>().Stop("Misiones");
            setMision.Completar();
            rec3Car = true;
        }
        if (rec3Car == true && dia4Car == false)
        {
            carmin.LanzarDialogo(4);
            dia4Car = true;
        }
        if (AdminDialogo.hablando == false && dia4Car == true && azaleiaVisible == false)
        {
            aj.azaleia.SetActive(true);
            azaleiaVisible = true;
        }
        if (azaleiaVisible == true && dia5Car == false)
        {
            carmin.LanzarDialogo(5);
            dia5Car = true;
        }
        if (AdminDialogo.hablando == false && dia5Car == true && dia0Aza == false)
        {
            azaleia.LanzarDialogo(0);
            dia0Aza = true;
        }
        if (AdminDialogo.hablando == false && dia0Aza == true && aj.efeLuzAza == false && animacionLuzAzaleia == false)
        {
            azaleia.LanzarDialogo(1);
            aj.LanzarLuzHechizoAzaleia();
            animacionLuzAzaleia = true;
        }
        if (aj.efeLuzAza == true && hechizoAzaleia == false)
        {
            aj.LanzarHechizoAzaleia();
            hechizoAzaleia = true;
        }
        if (aj.hechizoAzaTerminado == true && hechizoAzaleia == true && aj.desLuzAza == false && inicioDeshLuzAza == false)
        {
            aj.TerminarLuzAzaleia();
            inicioDeshLuzAza = true;
        }
        if (aj.desLuzAza == true && dia2Aza == false)
        {
            azaleia.LanzarDialogo(2);
            dia2Aza = true;
        }
        if (AdminDialogo.hablando == false && dia2Aza == true && ocultarAzaleia == false)
        {
            aj.azaleia.SetActive(false);
            ocultarAzaleia = true;
        }
        if (ocultarAzaleia == true && dia6Car == false)
        {
            carmin.LanzarDialogo(6);
            dia6Car = true;
        }
        if (AdminDialogo.hablando == false && dia6Car == true && lec115 == false)
        {
            aj.leccion115.SetActive(true);
            lec115 = true;
        }
        if (aj.leccionesTerminadas == 7 && dia7Car == false)
        {
            carmin.LanzarDialogo(7);
            dia7Car = true;
        }
        if (AdminDialogo.hablando == false && dia7Car == true && lec119 == false)
        {
            aj.leccion119.SetActive(true);
            lec119 = true;
        }
        if (aj.leccionesTerminadas == 8 && lec119 == true && dia8Car == false)
        {
            carmin.LanzarDialogo(8);
            dia8Car = true;
        }
        if (placasVisibles == false && dia8Car == true)
        {
            aj.placaPar.SetActive(true);
            aj.placaImpar.SetActive(true);
            placasVisibles = true;
        }
        if (focus.name == "PlacaPar" && dia0PlacaPar == false)
        {
            aj.placaParI.LanzarDialogo(0);
            dia0PlacaPar = true;
        }
        if (focus.name == "PlacaImpar" && dia0PlacaImpar == false)
        {
            aj.placaImparI.LanzarDialogo(0);
            dia0PlacaImpar = true;
        }
        if (dia0PlacaPar == true && dia0PlacaImpar == true)
        {
            if (AdminDialogo.hablando == false && focus.name == "PlacaPar" && lec121 == false)
            {
                aj.leccion121.SetActive(true);
                lec121 = true;
            }
            if (AdminDialogo.hablando == false && focus.name == "PlacaImpar" && lec123 == false)
            {
                aj.leccion123.SetActive(true);
                lec123 = true;
            }
        }
        if (aj.leccionesTerminadas == 10 && carminDesplazamiento0 == false)
        {
            aj.carminClone.GetComponent<Transform>().position = new Vector3(6, 4, 0);
            carminDesplazamiento0 = true;
            carmin.LanzarDialogo(9);
        }
        if (AdminDialogo.hablando == false && carminDesplazamiento0 == true && mis4Placas == false)
        {
            aj.placaParI.AbrirVentanaMision();
            mis4Placas = true;
        }
        if (mis4Placas == true && setMision.activa == true && venEjeMis4 == false)
        {
            FindObjectOfType<AudioManager>().Stop("Aldea");
            FindObjectOfType<AudioManager>().Play("Misiones");
            aj.ventanaEjeMis4.SetActive(true);
            venEjeMis4 = true;
        }
        if (venEjeMis4 == true && aj.dragdrop1 == false && aj.contadorDropsNumeros == 20)
        { 
            aj.ventanaEjeMis4.SetActive(false);
            aj.ventanaEjeMis4a.SetActive(true);
            aj.placaPar.SetActive(false);
            aj.placaImpar.SetActive(false);
            aj.dragdrop1 = true;
        }
        if (aj.dragdrop2 == true && rec4Placas == false)
        {
            FindObjectOfType<AudioManager>().Play("Aldea");
            FindObjectOfType<AudioManager>().Stop("Misiones");
            setMision.Completar();
            rec4Placas = true;
        }
        if (rec4Placas == true && dia10Car == false)
        {
            carmin.LanzarDialogo(10);
            dia10Car = true;
        }
        if (AdminDialogo.hablando == false && dia10Car == true && numerosPrimosVisibles == false)
        {
            aj.panelPrimos.SetActive(true);
            foreach (GameObject numero in aj.numerosPrimos)
            {
                numero.SetActive(true);
            }
            carmin.LanzarDialogo(11);
            foreach (Animator animadorPrimo in aj.animadoresPrimos)
            {
                aj.TransicionMovimientoPrimos(animadorPrimo);
            }
            numerosPrimosVisibles = true;
        }
        if (AdminDialogo.hablando == false && numerosPrimosVisibles == true && lec125 == false)
        {
            aj.panelPrimos.SetActive(false);
            foreach (GameObject numero in aj.numerosPrimos)
            {
                numero.SetActive(false);
            }
            aj.leccion125.SetActive(true);
            lec125 = true;
        }
        if (aj.leccionesTerminadas == 11 && dia12Car == false && focus.name == "Carmin(Clone)")
        {
            carmin.LanzarDialogo(12);
            dia12Car = true;
        }
        if (AdminDialogo.hablando == false && dia12Car == true && hechizoCarmin == false)
        {
            aj.luzCarmin = aj.carminClone.GetComponentInChildren<Light>();
            aj.luzCarmin.intensity = 25;
            aj.luzCarmin.range = 5;
            aj.InstanciarPiedrasNumeros();
            hechizoCarmin = true; 
        }
        if (aj.contadorInsPieNum == 100  && focus.name == "Carmin(Clone)" && dia13Car == false)
        {
            carmin.LanzarDialogo(13);
            dia13Car = true;
            aj.portalUlam.SetActive(true);
        }
        if (AdminDialogo.hablando == false && dia13Car == true && piedrasDestruidas == false)
        {
            aj.DestruirPiedrasNumeros();
            piedrasDestruidas = true;
        }
        if (focus.name == "PortalUlam" && dia0Por == false)
        {
            aj.iPortalUlam.LanzarDialogo(0);
            dia0Por = true;
        }
        if (AdminDialogo.hablando == false && dia0Por == true && mis5Portal == false)
        {
            aj.iPortalUlam.AbrirVentanaMision();
            mis5Portal = true;
        }
        if (mis5Portal == true && setMision.activa == true && ventanasMision5 == false)
        {
            FindObjectOfType<AudioManager>().Stop("Aldea");
            FindObjectOfType<AudioManager>().Play("Misiones");
            aj.ventanaNumerosPrimos.SetActive(true);
            aj.ventanaEspiralUlam.SetActive(true);
            aj.botonEjeMis5.SetActive(true);
            ventanasMision5 = true;
        }
        if (aj.ejeMis5OK == true && rec5Portal == false)
        {
            aj.portalUlam.SetActive(false);
            aj.ventanaNumerosPrimos.SetActive(false);
            aj.ventanaEspiralUlam.SetActive(false);
            aj.botonEjeMis5.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Aldea");
            FindObjectOfType<AudioManager>().Stop("Misiones");
            setMision.Completar();
            rec5Portal = true;
        }
        if (rec5Portal == true && dia14Car == false)
        {
            aj.carminClone.GetComponent<Transform>().position = new Vector3(24, 31.5f, 0);
            carmin.LanzarDialogo(14);
            dia14Car = true;
        }
        if (AdminDialogo.hablando == false && dia14Car == true && lec126 == false)
        {
            aj.leccion126.SetActive(true);
            lec126 = true;
        }
        if (aj.leccionesTerminadas == 12 && dia15Car == false)
        {
            carmin.LanzarDialogo(15);
            dia15Car = true;
        }
        if (AdminDialogo.hablando == false && dia15Car == true && lec128 == false)
        {
            aj.leccion128.SetActive(true);
            lec128 = true;
        }
        if (aj.leccionesTerminadas == 13 && dia16Car == false)
        {
            carmin.LanzarDialogo(16);
            dia16Car = true;
        }
        if (AdminDialogo.hablando == false && dia16Car == true && imgMonteNatural == false)
        {
            aj.MostrarImagenMonteNatural();
            imgMonteNatural = true;
        }
        if (aj.imgMonteNaturalMostrada == true && dia17Car == false)
        {
            carmin.LanzarDialogo(17);
            dia17Car = true;
        }
        if (AdminDialogo.hablando == false && dia17Car == true && señal1visible == false)
        {
            aj.señal1.SetActive(true);
            señal1visible = true;
        }
        if (aj.enMonteNatural == true &&
            posX <= -0.8f && posX >= -7.8f &&
            posY <= 15.6f && posY >= 9 &&
            dia0Nyl == false)
        {
            aj.iNylea.LanzarDialogo(0);
            dia0Nyl = true;
        }
        if (AdminDialogo.hablando == false && dia0Nyl == true && aj.efeLuzNyl == false && iniLuzNylea == false)
        {
            aj.ManejarLuzNylea(0);
            iniLuzNylea = true;
        }
        if (aj.efeLuzNyl == true && nyleaOculta == false)
        {
            aj.nylea.SetActive(false);
            nyleaOculta = true;
        }
        if (nyleaOculta == true && aj.desLuzNyl == false && iniDeshLuzNylea == false)
        {
            aj.ManejarLuzNylea(1);
            iniDeshLuzNylea = true;
        }
        if (aj.desLuzNyl == true && dia0Narr == false && posX < -0.4f && posY < 0.6f)
        {
            FindObjectOfType<AudioManager>().Stop("MonteNatural");
            FindObjectOfType<AudioManager>().Play("Explosion1");
            aj.narrador.LanzarDialogo(0);
            dia0Narr = true;
        }
        if (AdminDialogo.hablando == false && dia0Narr == true && dia1Narr == false)
        {
            FindObjectOfType<AudioManager>().Play("Explosion2");
            aj.narrador.LanzarDialogo(1);
            dia1Narr = true;
        }
        if (AdminDialogo.hablando == false && dia1Narr == true && dia2Narr == false)
        {
            FindObjectOfType<AudioManager>().Play("Explosion3");
            aj.narrador.LanzarDialogo(2);
            dia2Narr = true;
        }
        if (AdminDialogo.hablando == false && dia2Narr == true && inicioErupcion == false)
        {
            aj.IniciarErupcion();
            inicioErupcion = true;
        }
        if (AdminDialogo.hablando == false && inicioErupcion == true && escenaCombateVisible == false)
        {
            aj.margarita2.SetActive(true);
            aj.fabricio2.SetActive(true);
            aj.seneca1.SetActive(true);
            aj.esbirro1.SetActive(true);
            aj.esbirro2.SetActive(true);
            aj.esbirro3.SetActive(true);
            aj.reliquiaInfinito.SetActive(true);
            aj.colliderEscenaCombate.SetActive(true);
            aj.colliderEscenaCombate2.SetActive(true);
            aj.MNarbol53.SetActive(false);
            aj.MNarbol54.SetActive(false);
            escenaCombateVisible = true;
        }
        if (AdminDialogo.hablando == false && escenaCombateVisible == true && posX >= 22 && posY >= 23 && dia7Fab == false)
        {
            fabricio.LanzarDialogo(7);
            dia7Fab = true;
        }
        if (AdminDialogo.hablando == false && dia7Fab == true && dia9Mar == false)
        {
            margarita.LanzarDialogo(9);
            dia9Mar = true;
        }
        if (AdminDialogo.hablando == false && dia9Mar == true && dia0Sen == false)
        {
            aj.seneca1.SetActive(false);
            aj.seneca2.SetActive(true);
            aj.iSeneca.LanzarDialogo(0);
            dia0Sen = true;
        }
        if (AdminDialogo.hablando == false && dia0Sen == true && colliderCombateOculto == false)
        {
            aj.colliderEscenaCombate.SetActive(false);
            aj.colliderEscenaCombate2.SetActive(false);
            colliderCombateOculto = true;
        }
        if (focus.name == "Enemigo1" && inicioBatalla == false)
        {
            aj.bs.StartBattle();
            FindObjectOfType<AudioManager>().Stop("Erupcion");
            FindObjectOfType<AudioManager>().Stop("Fuego");
            FindObjectOfType<AudioManager>().Play("Batalla");
            aj.margarita2.SetActive(false);
            aj.fabricio2.SetActive(false);
            aj.seneca1.SetActive(false);
            aj.seneca2.SetActive(false);
            aj.esbirro1.SetActive(false);
            aj.esbirro2.SetActive(false);
            aj.esbirro3.SetActive(false);
            aj.reliquiaInfinito.SetActive(false);
            aj.detenerErupcion = true;
            inicioBatalla = true;
        }
        if (batallaFinalizada == true && aparicionNylea2 == false)
        {
            FindObjectOfType<AudioManager>().Stop("Batalla");
            FindObjectOfType<AudioManager>().Play("MonteNatural");
            aj.nylea.SetActive(true);
            aj.goLuzNylea.SetActive(true);
            aj.nylea.GetComponent<Transform>().position = new Vector3(24.07f, 30, -0.337f);
            aj.goLuzNylea.GetComponent<Transform>().position = new Vector3(24.07f, 30, -1);
            aparicionNylea2 = true;
        }
        if (focus.name == "Nylea" && batallaFinalizada == true && dia1Nyl == false)
        {
            aj.iNylea.LanzarDialogo(1);
            dia1Nyl = true;
        }
        if (AdminDialogo.hablando == false && dia1Nyl == true && inicioLuzNylea2 == false)
        {
            aj.ManejarLuzNylea(0);
            inicioLuzNylea2 = true;
        }
        if (inicioLuzNylea2 == true && aj.efeLuzNyl == true && restauracionBosque == false)
        {
            foreach (GameObject go in aj.piedrasLavaInstanciadas)
                Destroy(go);

            foreach (GameObject go in aj.goMonteNatural)
                go.SetActive(true);
            restauracionBosque = true;
        }
        if (restauracionBosque == true && iniDeshNylea2 == false)
        {
            aj.ManejarLuzNylea(1);
            iniDeshNylea2 = true;
        }
        if (iniDeshNylea2 == true && aj.desLuzNyl == true && dia2Nyl == false)
        {
            aj.iNylea.LanzarDialogo(2);
            dia2Nyl = true;
        }
        if (AdminDialogo.hablando == false && dia2Nyl == true && aj.imgCatedralMostrada == false)
        {
            aj.MostrarImagenCatedral();
            aj.señal2.SetActive(true);
        }
        if (aj.imgCatedralMostrada == true && dia3Nyl == false)
        {
            aj.iNylea.LanzarDialogo(3);
            dia3Nyl = true;
        }
        if (focus.name == "Patricio" && dia0Pat == false)
        {
            aj.iPatricio.LanzarDialogo(0);
            dia0Pat = true;
        }
        if (aj.contadorLeccionesVenn == 1)
        {
            aj.patricio.SetActive(false);
        }
        if (aj.contadorLeccionesVenn >= 6 && obBelVisible == false)
        {
            FindObjectOfType<AudioManager>().Stop("Catedral");
            aj.obBelzentok.SetActive(true);
            obBelVisible = true;
        }
        if (focus.name == "ObBelzentok" && dia0ObBel == false)
        {
            aj.iObBelzentok.LanzarDialogo(0);
            dia0ObBel = true;
        }
        if (AdminDialogo.hablando == false && dia0ObBel == true && aj.efeLuzObBel == false)
        {
            aj.goLuzObBel.SetActive(true);
            aj.ManejarLuzObBelzentok(0);
        }
        if (aj.efeLuzObBel == true && obBelOculto == false)
        {
            aj.obBelzentok.SetActive(false);
            FindObjectOfType<AudioManager>().Play("TruenoObBel");
            obBelOculto = true;
        }
        if (obBelOculto == true && aj.desLuzObBel == false)
        {
            aj.ManejarLuzObBelzentok(1);
            aj.goLuzObBel.SetActive(false);
        }
        if (aj.desLuzObBel == true && pantallaFinalVisible == false)
        {
            aj.pantallaFinal.SetActive(true);
            pantallaFinalVisible = true;
        }
        if (pantallaFinalVisible == true && musicaFinal == false)
        {
            FindObjectOfType<AudioManager>().Stop("TruenoObBel");
            FindObjectOfType<AudioManager>().Play("Final");
            musicaFinal = true;
        }

        // AQUÍ TERMINA EL FLUJO DE SECUENCIA HASTA EL MOMENTO
    }

    // Asigna como foco al interactable (NPC o Item) sobre el cual el jugador hizo clic
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDeFocused();
            focus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDeFocused();
        focus = null;
    }

    // Anima el movimiento cuando el jugador camina
    public void AnimarMovimiento(Vector2 direccion)
    {
        animador.SetLayerWeight(1, 1); // Selecciona la capa Walk del Animator
        animador.SetFloat("x", direccion.x); // Parámetros del Animador
        animador.SetFloat("y", direccion.y); // Parámetros del Animador
    }

    public void Mover()
    {
        transform.Translate(direccion * speed * Time.deltaTime); // Translate mueve el Transform. Transform define la posición, rotación y escala del objeto
                                                                 // Time es una interfaz para obtener información del tiempo
                                                                 // DeltaTime retorna el tiempo en segundos desde el último frame

        if (direccion.x != 0 || direccion.y != 0) // Retorna las componentes x y y del vector2
        {
            AnimarMovimiento(direccion);
        }
        else
        {
            animador.SetLayerWeight(1, 0); // Determina qué capa del Animator se usa: (idle o walk)
        }

    }

    // Cambia la dirección dependiendo de las flechas que presione el jugador para moverse
    private void GetInput()
    {
        direccion = Vector2.zero; // Inicializa el vector en (0,0)

        if (Input.GetKey(KeyCode.UpArrow)) // Retorna true mientras el jugador presiones la tecla del parámetro
        {
            direccion += Vector2.up; // Cambia las cordenadas del vector a (0,1) en este caso
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direccion += Vector2.left;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            direccion += Vector2.down;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            direccion += Vector2.right;
        }
    }

    // Método que se llama cuando la experiencia actual es mayor a la requerida para subir de nivel
    void SubirNivel()
    {
        nivel++;
        saludMax = (int)aj.GetSaludMax(nivel);
        saludActual = saludMax;
        barraSalud.maximo = saludMax;
        barraSalud.actual = saludMax;
        barraHPjugadorHUD.maximo = saludMax;
        barraHPjugadorHUD.maximo = saludActual;
        textoNivel.text = nivel.ToString();
        textoXPmax.text = ((int)aj.GetExperienciaRequerida(nivel)).ToString();
        textoXPactual.text = ((int)aj.GetExperienciaRequerida(nivel-1)).ToString();
        textoHPmax.text = saludMax.ToString();
        textoHPactual.text = saludMax.ToString();
        dañoMax = (int)aj.GetDañoMax(nivel);
        dañoMin = (int)aj.GetDañoMin(nivel);
        aj.MostrarMensaje("!Felicitaciones!, has subido al NIVEL " + nivel.ToString() + ".");
        SetFibonaccis(100);
    }

    // Método que se llama cuando el jugador gana experiencia. Actualiza la GUI correspondiente
    public void SetExperiencia(int exp)
    {
        experiencia += exp;
        aj.MostrarMensaje("Obtuviste " + exp.ToString() + " puntos de XP");
        if (experiencia > aj.GetExperienciaRequerida(nivel))
            SubirNivel();
        xpRequerida = (int)aj.GetExperienciaRequerida(nivel);
        xpRequeridaAnterior = (int)aj.GetExperienciaRequerida(nivel - 1);
        barraXP.maximo = xpRequerida;
        barraXP.minimo = xpRequeridaAnterior;
        barraXP.actual = experiencia;
        barraXPjugadorHUD.maximo = xpRequerida;
        barraXPjugadorHUD.minimo = xpRequeridaAnterior;
        barraXPjugadorHUD.actual = experiencia;
        textoXPactual.text = experiencia.ToString();
    }

    // Método que se llama cuando el jugador gana fibonaccis. Actualiza la GUI correspondiente
    public void SetFibonaccis(int fb)
    {
        fibonaccis += fb;
        aj.MostrarMensaje("Obtuviste " + fb.ToString() + " fibonaccis");
        fib.text = fibonaccis.ToString();
    }

    public bool TakeDamage(int dmg)
    {
        saludActual -= dmg;
        aj.MostrarDaño(dmg);
        if (saludActual <= 0)
            return true;
        else
            return false;
    }

    public void Curar(int cantidad)
    {
        saludActual += cantidad;
        aj.MostrarCura(cantidad);
        if (saludActual > saludMax)
            saludActual = saludMax;
    }
}