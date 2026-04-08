using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform player;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float tooCloseDistance = 4f; // Distanza a cui il nemico scappa per vicinanza
    public float safeFleeDistance = 10f; // Di quanti metri scappa lontano dal giocatore

    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;
    public Slider healthBar;

    private NavMeshAgent agent;
    private GameManager gm;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        gm = FindAnyObjectByType<GameManager>();
        if (gm != null)
        {
            gm.AddEnemy();
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // NUOVA LOGICA: Controlla se la salute attuale è minore di quella massima (il nemico è stato colpito)
        bool hasTakenDamage = currentHealth < maxHealth;
        bool isTooClose = distanceToPlayer < tooCloseDistance;

        // Se ha subito almeno un colpo OPPURE il giocatore è troppo vicino, scappa. Altrimenti, insegui.
        if (hasTakenDamage || isTooClose)
        {
            Flee();
        }
        else
        {
            Chase();
        }
    }

    void LateUpdate()
    {
        if (healthBar != null)
        {
            healthBar.transform.LookAt(healthBar.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }

    void Chase()
    {
        // Imposta la destinazione direttamente sulla posizione del giocatore
        agent.SetDestination(player.position);
    }

    void Flee()
    {
        // Calcola la direzione opposta al giocatore e crea un punto lontano
        Vector3 dirToPlayer = transform.position - player.position;
        Vector3 newPos = transform.position + dirToPlayer.normalized * safeFleeDistance;

        NavMeshHit hit;
        // Cerca un punto valido sul NavMesh vicino alla posizione calcolata
        if (NavMesh.SamplePosition(newPos, out hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null) healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gm != null) gm.EnemyDied();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage();
            }
        }
    }
}