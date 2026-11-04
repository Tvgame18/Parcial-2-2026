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
}
