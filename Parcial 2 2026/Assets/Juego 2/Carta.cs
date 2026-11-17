using UnityEngine;

public class Carta : MonoBehaviour
{
    
    public CartaData datos;
    
    public Renderer miRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Configurar(CartaData nuevaCarta)
    {
        datos = nuevaCarta;

        MostrarCarta(true);
    }

    public void MostrarCarta(bool mostrar)
    {
        if (mostrar)
        {
            miRenderer.material = datos.material;
            miRenderer.material.color = Color.white;

            return;
        }
        else
        {
            miRenderer.material = datos.material;
            miRenderer.material.color = Color.black;
        }


    }
}
