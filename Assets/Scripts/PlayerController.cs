using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;

    [Header("Combate")]
    public float meleeRange = 1.5f;
    public float rangedRange = 5f;
    public GameObject meleeWeapon;
    public GameObject rangedWeapon;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Transform nearestEnemy;

    void Start()
    {
        // Inicializar Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Inicializar armas desactivadas
        if (meleeWeapon != null) meleeWeapon.SetActive(false);
        if (rangedWeapon != null) rangedWeapon.SetActive(false);
    }

    void Update()
    {
        // --- Movimiento ---
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveVelocity = moveInput.normalized * moveSpeed;

        // --- Encontrar enemigo más cercano ---
        FindNearestEnemy();

        // --- Combate según distancia ---
        if (nearestEnemy != null)
        {
            float distance = Vector2.Distance(transform.position, nearestEnemy.position);

            if (distance <= meleeRange)
            {
                // Solo melee
                if (meleeWeapon != null) meleeWeapon.SetActive(true);
                if (rangedWeapon != null) rangedWeapon.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Space))
                    AttackMelee();
            }
            else if (distance >= rangedRange)
            {
                // Solo rango
                if (meleeWeapon != null) meleeWeapon.SetActive(false);
                if (rangedWeapon != null) rangedWeapon.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space))
                    AttackRanged();
            }
            else
            {
                // Ningún ataque
                if (meleeWeapon != null) meleeWeapon.SetActive(false);
                if (rangedWeapon != null) rangedWeapon.SetActive(false);
            }
        }
        else
        {
            // No hay enemigos, desactivar armas
            if (meleeWeapon != null) meleeWeapon.SetActive(false);
            if (rangedWeapon != null) rangedWeapon.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        // Aplicar movimiento
        rb.linearVelocity = moveVelocity;
    }

    // --- Encontrar enemigo más cercano ---
    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
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
        // Aquí va la lógica de daño melee
    }

    void AttackRanged()
    {
        Debug.Log("Ataque a distancia activado");
        // Aquí va la lógica de dagas
    }
}


