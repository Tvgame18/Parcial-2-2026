using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/Data")]
public class CharacterData : ScriptableObject
{
    public float speed;
    public float jumpForce;
    public GameObject prefab;
    
}
