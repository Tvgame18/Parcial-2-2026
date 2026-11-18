using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public CharacterData CharacterData;
    public CameraFollow camara;
    public Transform spawnPoint;
    public void Spawn()
    {
        GameObject myPlayer = Instantiate(CharacterData.prefab, spawnPoint.position, spawnPoint.rotation);
        Player info = myPlayer.GetComponent<Player>();

        info.speed = CharacterData.speed;
        info.jumpForce = CharacterData.jumpForce;

        camara.target = myPlayer.transform;
    }
}
