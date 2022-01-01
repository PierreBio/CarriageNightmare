using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Body : MonoBehaviour
{
    public int maxHp;
    public int hp;

    protected virtual void Start()
    {
        hp = maxHp;
    }

    public virtual void TakeDamage(int damage)
    {
        SoundManager.GetInstance().Play("Touched", this.gameObject);

        hp -= damage;
        if (hp <= 0)
           Death();
    }

    protected abstract void Death();
}
