using UnityEngine;

public class Enemy : CharacterBase
{
    public float moveSpeed = 3f;          // Velocidad de movimiento
    public Vector2[] patrolPoints;        // Puntos de patrulla
    public float detectionRange = 5f;     // Distancia para detectar al jugador

    private int currentPoint = 0;
    private Rigidbody2D rb;
    private Transform player;


    protected override void Start()
    {
        hearts = 5; // 5 corazones = 50 HP
        base.Start();
       {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (patrolPoints.Length > 0)
            transform.position = patrolPoints[0];
    }
    }

    public void ReceiveMeleeAttack()
    {
        TakeDamage(40); // 4 corazones = 40 HP
    }

    public void ReceiveRangedAttack()
    {
        TakeDamage(30); // 3 corazones = 30 HP
    }
    void FixedUpdate()
    {
        Vector2 targetPos;

        // Verificar distancia al jugador
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            // Persiguiendo al jugador
            targetPos = player.position;
        }
        else
        {
            // Patrullaje
            targetPos = patrolPoints[currentPoint];

            if (Vector2.Distance(rb.position, targetPos) < 0.1f)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
            }
        }

        // Movimiento en cuatro direcciones
        Vector2 direction = targetPos - rb.position;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Mover horizontalmente
            direction.y = 0;
        }
        else
        {
            // Mover verticalmente
            direction.x = 0;
        }

        direction.Normalize();
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}