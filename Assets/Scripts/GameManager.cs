using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Jugador")]
    public GameObject player;
    public float playerMoveSpeed = 5f;
    public float meleeRange = 1.5f;
    public float rangedRange = 5f;
    public GameObject meleeWeapon;
    public GameObject rangedWeapon;

    [Header("Enemigos")]
    public GameObject enemyPrefab;
    public Transform[] enemySpawnPoints;
    public float enemyMoveSpeed = 3f;
    public float enemyDetectionRange = 5f;
    public float enemySpawnInterval = 8f;
    public int maxEnemies = 5;

    private Rigidbody2D playerRb;
    private Vector2 playerMoveInput;
    private Vector2 playerMoveVelocity;
    private Transform nearestEnemy;

    private int enemiesSpawned = 0;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        // Movimiento del jugador
        playerMoveInput.x = Input.GetAxisRaw("Horizontal");
        playerMoveInput.y = Input.GetAxisRaw("Vertical");
        playerMoveVelocity = playerMoveInput.normalized * playerMoveSpeed;

        // Buscar enemigo m�s cercano
        FindNearestEnemy();

        if (nearestEnemy != null)
        {
            float distance = Vector2.Distance(player.transform.position, nearestEnemy.position);

            if (distance <= meleeRange)
            {
                meleeWeapon.SetActive(true);
                rangedWeapon.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Space))
                    AttackMelee();
            }
            else if (distance >= rangedRange)
            {
                meleeWeapon.SetActive(false);
                rangedWeapon.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space))
                    AttackRanged();
            }
            else
            {
                meleeWeapon.SetActive(false);
                rangedWeapon.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        if (playerRb != null)
            playerRb.linearVelocity = playerMoveVelocity;

        // Mover enemigos activos
        EnemyController[] enemies = Object.FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
        foreach (EnemyController e in enemies)
        {
            e.MoveEnemy(player.transform);
        }
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(player.transform.position, enemy.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                nearestEnemy = enemy.transform;
            }
        }
    }

    void AttackMelee()
    {
        Debug.Log("Ataque melee activado");
    }

    void AttackRanged()
    {
        Debug.Log("Ataque a distancia activado");
    }

    IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < maxEnemies)
        {
            Transform spawnPoint = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)];
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // A�adir EnemyController
            EnemyController ec = newEnemy.AddComponent<EnemyController>();
            ec.moveSpeed = enemyMoveSpeed;
            ec.detectionRange = enemyDetectionRange;

            enemiesSpawned++;
            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }
}

// ===========================================
// ============== ENEMIGO ====================
// ===========================================

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 5f;
    public Vector2[] patrolPoints;

    private int currentPoint = 0;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (patrolPoints != null && patrolPoints.Length > 0)
            transform.position = patrolPoints[0];
    }

    public void MoveEnemy(Transform player)
    {
        if (player == null) return;

        Vector2 targetPos;

        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            targetPos = player.position; // Persigue
        }
        else if (patrolPoints != null && patrolPoints.Length > 0)
        {
            targetPos = patrolPoints[currentPoint];
            if (Vector2.Distance(rb.position, targetPos) < 0.1f)
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
        else
        {
            return;
        }

        // Movimiento 4 direcciones
        Vector2 direction = targetPos - rb.position;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction.y = 0;
        else
            direction.x = 0;

        direction.Normalize();
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}
