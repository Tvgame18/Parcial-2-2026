using System.Collections.Generic;
using UnityEngine;

public class Mano : MonoBehaviour
{
    public List<Carta> cartas = new List<Carta>();

    public float separacion = 0.9f;
    public float angulo = 30f;
    public float alturaCurva = 0.12f;

    public void AgregarCarta(Carta carta)
    {
        cartas.Add(carta);
        carta.transform.SetParent(transform, false);

        OrganizarCartas();
    }
    
    public void QuitarCarta(Carta carta)
    {
        cartas.Remove(carta);

        OrganizarCartas();
    }

    public void OrganizarCartas()
    {
        int cantidad = cartas.Count;
        for (int i = 0; i < cantidad; i++)
        {
            float centro = (cantidad - 1) / 2f;
            float diferencia = i - centro;

            float x = diferencia * separacion;
            float y = -Mathf.Abs(diferencia)*alturaCurva;

            cartas[i].transform.localPosition = new Vector3(x, y, 0);

            cartas[i].transform.localRotation = Quaternion.Euler(0, 0, -diferencia * angulo);
        }
    }
}
