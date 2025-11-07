using UnityEngine;

public class Protagonist : CharacterBase
{
    protected override void Start()
    {
        hearts = 3; // 3 corazones = 30 HP
        base.Start();
    }

    public void ReceiveMeleeAttack()
    {
        TakeDamage(10); // 1 corazón = 10 HP
    }

    public void ReceiveRangedAttack()
    {
        TakeDamage(10); // también 1 corazón = 10 HP
    }
}
