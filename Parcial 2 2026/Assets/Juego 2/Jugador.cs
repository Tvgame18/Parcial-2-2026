using UnityEngine;

public class Jugador : MonoBehaviour
{
    public bool esEnemigo;
    public Mano mano;
    public int puntos;
    

    public void RecibirCarta(Carta carta)
    {
        if (carta == null) return;
        mano.AgregarCarta(carta);
        carta.MostrarCarta(!esEnemigo);
    }

    public void QuitarCarta(Carta carta)
    {
        mano.QuitarCarta(carta);
    }
    public Carta ElegirCartaAleatoria()
    {
        if (mano.cartas.Count == 0) return null;

        int aleatorio = Random.Range(0, mano.cartas.Count);
        return mano.cartas[aleatorio];
    }

}
