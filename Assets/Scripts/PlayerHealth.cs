using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    [Header("UI Reference")]
    public Image[] hearts;

    private GameManager gameManager;

    // NUOVO: Variabili per l'invulnerabilità
    public float invulnerabilityTime = 1f; // 1 secondo di respiro tra un colpo e l'altro
    private float lastDamageTime = -2f;

    void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void TakeDamage()
    {
        // Se è passato meno di 1 secondo dall'ultimo danno, ignora il colpo
        if (Time.time < lastDamageTime + invulnerabilityTime) return;

        lastDamageTime = Time.time; // Registra il momento in cui hai preso danno

        if (currentHealth <= 0) return;

        currentHealth--;
        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            gameManager.Defeat();
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
}