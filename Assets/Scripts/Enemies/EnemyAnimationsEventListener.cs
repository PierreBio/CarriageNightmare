using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationsEventListener : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;

    public void AttackHit()
    {
        enemyController.AttackHit();
    }
    public void AttackEnded()
    {
        enemyController.AttackEnded();
    }
}
