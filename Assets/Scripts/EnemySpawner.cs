using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Riferimenti")]
    public GameObject enemyPrefab;    // Trascina qui il prefabbricato del tuo nemico
    public Transform playerTransform; // Trascina qui il tuo Player

    [Header("Impostazioni Spawn")]
    public int minEnemies = 1;
    public int maxEnemies = 8;
    public float spawnRadius = 2f;    // Raggio entro cui spawnano per non sovrapporsi

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        // Genera un numero casuale tra minEnemies e maxEnemies (il +1 serve perché il max è esclusivo nei numeri interi)
        int enemiesToSpawn = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Crea una posizione leggermente sfalsata per evitare che i nemici si incastrino tra loro
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0; // Manteniamo i nemici alla stessa altezza dello spawner
            Vector3 spawnPosition = transform.position + randomOffset;

            // Crea il nemico nella scena
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Trova lo script 'Enemy' sul nemico appena creato e assegnagli il Player
            Enemy enemyScript = spawnedEnemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.player = playerTransform;
            }
        }
    }
}
