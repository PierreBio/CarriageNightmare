using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBody : Body
{
    [SerializeField] EnemyController enemy;
    int lastDamage;
    [SerializeField] int enemyId;

    public void TakeDamage(int damage, int playerId)
    {
        bool isLit = enemy.isLit[0] || enemy.isLit[1] || enemy.isLit[2];
        if (isLit)
        {
            SoundManager.GetInstance().Play("Touched", this.gameObject);
            GetComponentInChildren<ParticleSystem>().Play();
            base.TakeDamage(damage);
            lastDamage = playerId;
        }
    }

    protected override void Death()
    {
        enemy.Death();
        GameManager.Instance.Kill(enemyId, lastDamage);
    }

}
