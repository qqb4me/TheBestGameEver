using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageAttackEvent : MonoBehaviour
{
    public EnemyAI enemyAI;

    public void AttackDamageEvent()
    {
        enemyAI.AttackDamage();
    }
}
