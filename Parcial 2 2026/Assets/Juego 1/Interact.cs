using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameManager manager;

    public enum TypeInteract
    {
        win,
        lose
    }
    
    public TypeInteract type;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (type == TypeInteract.lose)
            {
                manager.ShowLose();
            }
            else if (type == TypeInteract.win)
            {
                manager.ShowVictory();
            }
        }
    }
}
