using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject victoryPanel;
    public GameObject losePanel;
    
    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ShowLose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public GameObject personaje1;
    public GameObject personaje2;

    public GameObject canvas;
    public Transform spawnPoint;

    public void SeleccionarPersonaje(int personaje)
    {
        GameObject personajeElegido = null;

        if (personaje == 1)
            personajeElegido = personaje1;

        if (personaje == 2)
            personajeElegido = personaje2;

        if (personajeElegido == null)
        {
            Debug.Log("Paso por aca");
            Instantiate(
                personajeElegido,
                spawnPoint.position,
                spawnPoint.rotation
            );

            canvas.SetActive(false);
        }
    }
}
