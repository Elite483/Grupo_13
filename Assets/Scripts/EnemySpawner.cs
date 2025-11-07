using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;       // Prefab del enemigo
    public Transform[] spawnPoints;      // Posibles puntos de spawn
    public float spawnInterval = 8f;     // Tiempo entre cada spawn
    public int maxEnemies = 5;           // Máximo de enemigos a spawnear

    private int enemiesSpawned = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    System.Collections.IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < maxEnemies)
        {
            // Elegir un spawn point aleatorio
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instanciar enemigo
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            enemiesSpawned++;

            // Esperar intervalo
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
