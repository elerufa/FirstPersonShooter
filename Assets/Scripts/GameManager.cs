using UnityEngine;
using UnityEngine.SceneManagement; // Serve per ricaricare la scena

public class GameManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject victoryPanel;
    public GameObject defeatPanel;

    private int totalEnemies = 0;
    private bool gameEnded = false;

    void Start()
    {
        // Assicura che il tempo scorra normalmente all'avvio e nasconde i cursori
        Time.timeScale = 1f;
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }

    // Chiamato dallo script Enemy quando un nemico viene generato
    public void AddEnemy()
    {
        totalEnemies++;
    }

    // Chiamato dallo script Enemy quando muore
    public void EnemyDied()
    {
        totalEnemies--;

        // Se non ci sono più nemici e il giocatore non ha perso, vinci
        if (totalEnemies <= 0 && !gameEnded)
        {
            Victory();
        }
    }

    public void Defeat()
    {
        if (gameEnded) return;
        gameEnded = true;
        defeatPanel.SetActive(true);
        EndGameLogic();
    }

    void Victory()
    {
        if (gameEnded) return;
        gameEnded = true;
        victoryPanel.SetActive(true);
        EndGameLogic();
    }

    void EndGameLogic()
    {
        Time.timeScale = 0f; // Mette il gioco in pausa
        Cursor.lockState = CursorLockMode.None; // Sblocca il mouse per cliccare i bottoni
        Cursor.visible = true;
    }

    // Metodi per i bottoni
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Ricarica il livello attuale
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Gioco Chiuso!"); // Visibile solo nell'editor
    }
}