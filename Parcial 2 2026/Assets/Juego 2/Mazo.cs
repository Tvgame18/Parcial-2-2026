using System.Collections.Generic;
using UnityEngine;

public class Mazo : MonoBehaviour
{
    public Carta cartaPrefab;
    public List<CartaData> cartasDisponibles;
    public List<Carta> cartas = new List<Carta>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        CrearMazo();
    }

    // Update is called once per frame
    public void CrearMazo()
    {
        foreach (var datos in cartasDisponibles)
        {
            Carta nueva = Instantiate(cartaPrefab, transform);

            nueva.Configurar(datos);
            nueva.gameObject.SetActive(false);
            cartas.Add(nueva);

        }


        Mezclar();
    }
    public void Mezclar()
    {
        for (int i = 0; i < cartas.Count; i++)
        {
            int random = Random.Range(i, cartas.Count);

            Carta auxiliar = cartas[i];
            cartas[i] = cartas[random];
            cartas[random] = auxiliar;
        }
    }
    public Carta RobarCarta()
    {
        if (cartas.Count == 0)
        {
            return null;
        }

        Carta carta = cartas[0];
        cartas.RemoveAt(0);
        carta.gameObject.SetActive(true);
        return carta;
    }
    public void DevolverCarta(Carta carta)
    {
        carta.transform.SetParent(transform);
        carta.gameObject.SetActive(false);

        cartas.Add(carta);
    }
}
