using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public int damage = 10;
    public LayerMask enemyLayer;
    public LayerMask environmentLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        Debug.Log("Jugador realiza ataque cuerpo a cuerpo...");

        // ERROR intencional: raycast ignora objetos del entorno
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, 5f, enemyLayer))
        {
            hit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        else
        {
            // Log de advertencia cuando el ataque atraviesa objetos
            Debug.LogError("¡El ataque atravesó un objeto del entorno! Colisión no registrada.");
        }
    }
    }

// Clase auxiliar de salud del enemigo
public class EnemyHealth : MonoBehaviour
{
    public int health = 100;

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        Debug.Log("Enemigo recibió daño: " + dmg);
    }
}
