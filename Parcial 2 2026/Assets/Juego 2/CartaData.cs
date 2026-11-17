using UnityEngine;

[CreateAssetMenu(fileName = "NuevaCarta", menuName = "Cartas/Nueva carta")]
public class CartaData : ScriptableObject
{
    public TipoCarta tipo;
    [Range(1, 3)]
    public int potencia;
    public Material material;


}
