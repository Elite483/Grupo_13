using UnityEngine;

public class Enemy : CharacterBase
{
    protected override void Start()
    {
        hearts = 5; // 5 corazones = 50 HP
        base.Start();
    }

    public void ReceiveMeleeAttack()
    {
        TakeDamage(40); // 4 corazones = 40 HP
    }

    public void ReceiveRangedAttack()
    {
        TakeDamage(30); // 3 corazones = 30 HP
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ReceiveMeleeAttack();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ReceiveRangedAttack();
        }
    }
}