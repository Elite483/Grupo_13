using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float meleeRange = 1.5f;          // Distancia máxima para ataque melee
    public float rangedRange = 5f;           // Distancia mínima para ataque a distancia
    public GameObject meleeWeapon;           // Objeto/melee del jugador
    public GameObject rangedWeapon;          // Objeto/rango (dagas)

    private Transform nearestEnemy;

    void Update()
    {
        FindNearestEnemy();

        if (nearestEnemy != null)
        {
            float distance = Vector2.Distance(transform.position, nearestEnemy.position);

            if (distance <= meleeRange)
            {
                // Solo se permite melee
                meleeWeapon.SetActive(true);
                rangedWeapon.SetActive(false);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AttackMelee();
                }
            }
            else if (distance >= rangedRange)
            {
                // Solo se permite rango
                meleeWeapon.SetActive(false);
                rangedWeapon.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    AttackRanged();
                }
            }
            else
            {
                // Entre meleeRange y rangedRange, opcional: desactivar ambos
                meleeWeapon.SetActive(false);
                rangedWeapon.SetActive(false);
            }
        }
    }

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
        // Aquí pones tu lógica de daño melee
    }

    void AttackRanged()
    {
        Debug.Log("Ataque a distancia activado");
        // Aquí pones tu lógica de disparo de dagas
    }
}