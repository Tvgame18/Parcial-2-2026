using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManagerCartas : MonoBehaviour
{
    public static GameManagerCartas Instance;

    public Mazo mazo;
    public Jugador jugador;
    public Jugador enemigo;
    public Tablero tablero;
    public TextMeshProUGUI textEnemigo;
    public TextMeshProUGUI textJugador;
    public Camera camara;

    public float duracionMovimiento = 0.5f;

    private bool rondaEnCurso;
    private bool puedeJugar;

    // Indica quién comienza la siguiente ronda.
    private bool empiezaEnemigo = false;

    // Guarda la carta si el enemigo juega primero.
    private Carta cartaEnemigoPendiente;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            jugador.RecibirCarta(mazo.RobarCarta());
            enemigo.RecibirCarta(mazo.RobarCarta());
        }
        textEnemigo.text = "Enemigo:" + enemigo.puntos;
        textJugador.text = "Jugador:" + jugador.puntos;
        IniciarRonda();
    }

    private void Update()
    {
        if (!puedeJugar)
            return;

        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouse = Mouse.current.position.ReadValue();

            Ray ray = camara.ScreenPointToRay(mouse);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Carta carta = hit.collider.GetComponent<Carta>();

                if (carta != null &&
                    jugador.mano.cartas.Contains(carta))
                {
                    JugarCarta(carta);
                }
            }
        }
    }

    private void IniciarRonda()
    {
        rondaEnCurso = false;
        puedeJugar = false;
        cartaEnemigoPendiente = null;

        if (empiezaEnemigo)
        {
            Debug.Log("Comienza el enemigo");

            StartCoroutine(EnemigoJuegaPrimero());
        }
        else
        {
            Debug.Log("Comienza el jugador");

            puedeJugar = true;
        }
    }

    private IEnumerator EnemigoJuegaPrimero()
    {
        rondaEnCurso = true;

        yield return new WaitForSeconds(0.5f);

        cartaEnemigoPendiente = enemigo.ElegirCartaAleatoria();

        enemigo.QuitarCarta(cartaEnemigoPendiente);
        cartaEnemigoPendiente.MostrarCarta(true);

        yield return StartCoroutine(
            MoverCarta(cartaEnemigoPendiente, tablero.posicionEnemigo)
        );

        // Ahora el jugador puede responder.
        rondaEnCurso = false;
        puedeJugar = true;
    }

    public void JugarCarta(Carta carta)
    {
        if (!puedeJugar || rondaEnCurso)
            return;

        if (!jugador.mano.cartas.Contains(carta))
            return;

        puedeJugar = false;

        StartCoroutine(Ronda(carta));
    }

    private IEnumerator Ronda(Carta cartaJugador)
    {
        rondaEnCurso = true;

        // El jugador coloca su carta.
        jugador.QuitarCarta(cartaJugador);

        yield return StartCoroutine(
            MoverCarta(cartaJugador, tablero.posicionJugador)
        );

        Carta cartaEnemigo;

        if (empiezaEnemigo)
        {
            // El enemigo ya había jugado.
            cartaEnemigo = cartaEnemigoPendiente;
        }
        else
        {
            // El enemigo responde al jugador.
            yield return new WaitForSeconds(0.5f);

            cartaEnemigo = enemigo.ElegirCartaAleatoria();

            enemigo.QuitarCarta(cartaEnemigo);
            cartaEnemigo.MostrarCarta(true);

            yield return StartCoroutine(
                MoverCarta(cartaEnemigo, tablero.posicionEnemigo)
            );
        }

        // Comparamos las cartas.
        int resultado = CompararCartas(cartaJugador, cartaEnemigo);

        if (resultado == 1)
        {
            jugador.puntos++;
            empiezaEnemigo = false;
            textJugador.text = "Jugador:" + jugador.puntos;
            Debug.Log("Ganó el jugador");
        }
        else if (resultado == -1)
        {
            enemigo.puntos++;
            empiezaEnemigo = true;
            textEnemigo.text = "Enemigo:" + enemigo.puntos;
            Debug.Log("Ganó el enemigo");
        }
        else
        {
            Debug.Log("Empate");
            // Se mantiene quién comenzó esta ronda.
        }

        Debug.Log("Jugador: " + jugador.puntos +
                  " | Enemigo: " + enemigo.puntos);

        yield return new WaitForSeconds(2f);

        // Devolvemos las cartas al mazo.
        mazo.DevolverCarta(cartaJugador);
        mazo.DevolverCarta(cartaEnemigo);

        mazo.Mezclar();

        // Repartimos una nueva carta a cada jugador.
        jugador.RecibirCarta(mazo.RobarCarta());
        enemigo.RecibirCarta(mazo.RobarCarta());

        // Iniciamos la siguiente ronda.
        IniciarRonda();
    }

    private int CompararCartas(Carta jugador, Carta enemigo)
    {
        CartaData datosJugador = jugador.datos;
        CartaData datosEnemigo = enemigo.datos;

        if (datosJugador.tipo == datosEnemigo.tipo)
        {
            if (datosJugador.potencia > datosEnemigo.potencia)
                return 1;

            if (datosJugador.potencia < datosEnemigo.potencia)
                return -1;

            return 0;
        }

        if (datosJugador.tipo == TipoCarta.Espada &&
            datosEnemigo.tipo == TipoCarta.Escudo)
            return 1;

        if (datosJugador.tipo == TipoCarta.Escudo &&
            datosEnemigo.tipo == TipoCarta.Arco)
            return 1;

        if (datosJugador.tipo == TipoCarta.Arco &&
            datosEnemigo.tipo == TipoCarta.Espada)
            return 1;

        return -1;
    }

    private IEnumerator MoverCarta(Carta carta, Transform destino)
    {
        carta.transform.SetParent(null, true);

        Vector3 posicionInicial = carta.transform.position;
        Quaternion rotacionInicial = carta.transform.rotation;

        float tiempo = 0f;

        while (tiempo < duracionMovimiento)
        {
            tiempo += Time.deltaTime;

            float t = Mathf.Clamp01(tiempo / duracionMovimiento);

            carta.transform.position = Vector3.Lerp(
                posicionInicial,
                destino.position,
                t
            );

            carta.transform.rotation = Quaternion.Slerp(
                rotacionInicial,
                destino.rotation,
                t
            );

            yield return null;
        }

        carta.transform.position = destino.position;
        carta.transform.rotation = destino.rotation;
    }
}