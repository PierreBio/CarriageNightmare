using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    protected GameObject player;
    protected NavMeshAgent navMesh;

    [Header("Caract�ristiques")]
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float speed = 5f;

    protected bool canAttack = true;
    //protected float xAxis = 2f;
    //protected float zAxis = 2f;

    [SerializeField] Animator enemyAnim;
    [SerializeField] float attackCoolDown = 3;

    CarriageBody attackTarget;

    public bool[] isLit = new bool[3];

    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = speed;
        player = GameManager.Instance.carriage;
        LightReserveManager.OnLightDisabled += ExitedLight;
    }

    void ExitedLight(int id)
    {
        isLit[id] = false;
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.SetDestination(player.transform.position);
    }

    public void Death()
    {
        canAttack = false;
        navMesh.isStopped = true;
        SoundManager.GetInstance().Play("Die_1", this.gameObject);
        // TODO death anim
        StartCoroutine(DeathCoroutine());
    }

    public void AttackHit()
    {
        attackTarget.TakeDamage(damage);
        SoundManager.GetInstance().Play("Carriage_damage", this.gameObject);
    }

    public void AttackEnded()
    {
        navMesh.isStopped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Light"))
            isLit[other.GetComponent<LightGameObject>().id] = true;
    }

    private void OnTriggerExit(Collider other)
    {   
        if (other.CompareTag("Light"))
            isLit[other.GetComponent<LightGameObject>().id] = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Carriage")&&canAttack)
        {
            attackTarget = other.gameObject.GetComponent<CarriageBody>();
            
            // Attack
            navMesh.isStopped = true;
            enemyAnim.Play("Attack");
            StartCoroutine(AttackCooldownCoroutine());
        }
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }

    IEnumerator DeathCoroutine()
    {
        enemyAnim.Play("Death");
        yield return new WaitForSeconds(2);
        LightReserveManager.OnLightDisabled -= ExitedLight;
        Destroy(gameObject);
    }

    //private IEnumerator AttackCoroutine()
    //{
        
    //    //isAttacking = false;
    //    //var aleatoireX = Random.Range(0, 2);
    //    //var aleatoireZ = Random.Range(0, 2);

    //    //xAxis = aleatoireX == 0 ? Random.Range(3, 2) : Random.Range(-3, -2);
    //    //zAxis = aleatoireZ == 0 ? Random.Range(3, 2) : Random.Range(-3, -2);

    //    //yield return new WaitForSeconds(2);
    //    //isAttacking = true;

    //}
}
