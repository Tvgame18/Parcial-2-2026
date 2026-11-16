using UnityEngine;

public class Carta : MonoBehaviour
{
    public TipoCarta tipo;
    public int potencia;
    public Renderer miRenderer;
    public TextMesh textoCarta;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Configurar(TipoCarta nuevoTipo, int nuevaPotencia)
    {
        tipo= nuevoTipo;
        potencia= nuevaPotencia;

        MostrarCarta(true);
    }

    public void MostrarCarta(bool mostrar)
    {
        if(!mostrar)
        {
            miRenderer.material.color = Color.gray;
            if(textoCarta != null )
            {
                textoCarta.text = "?";
            }
            return;
        }

        switch (tipo)
        {
            case TipoCarta.Espada:
                miRenderer.material.color = Color.red;
                break;
            case TipoCarta.Arco:
                miRenderer.material.color= Color.yellow;
                break;
            case TipoCarta.Escudo:
                miRenderer.material.color=Color.blue;
                break;
            default:
                break;
        }
        if(textoCarta != null )
        {
            textoCarta.text = tipo + "\n" + potencia;
        }
    }
}
