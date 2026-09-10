using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCont : MonoBehaviour
{
    private int _health;
    private float _speed;
    private float _searchRange = 10f;
    private float searchTimer = 0f;
    private float searchCD = 2f;
    private float attackTimer = 0f;
    private float attackCD = 2f;
    [SerializeField] private LayerMask searchLayer;
    private PlayerCont target;
    private Transform randomPoint;
    [SerializeField] private NavMeshAgent agent;
    private float attackRange = 1f;
    private int damage = 1;
    public EnemyState enemy_state {  get; private set; }=EnemyState.Idle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void SearchForPlayer()
    {
        agent.ResetPath();
        Collider[] colliders = Physics.OverlapSphere(transform.position, _searchRange, searchLayer);
        if (colliders.Length == 0)
        {
            target = null;
            enemy_state = EnemyState.Idle;
            return;
        }
        if (colliders[0].gameObject.TryGetComponent<PlayerCont>(out PlayerCont player)&& player.isAlive)
        {
            target = player;
            enemy_state = EnemyState.Chase;
            
        }
        else
        {
            target = null;
            enemy_state = EnemyState.Idle;
        }
    }

    // Update is called once per frame
     private void Update()
    {
        searchTimer += Time.deltaTime;
        if(searchTimer>= searchCD)
        {
            searchTimer = 0;
            SearchForPlayer();
        }
       
        switch (enemy_state)
        {
            case EnemyState.Idle:
                Move();
                break;
            case EnemyState.Chase:
                GoTo();
                break;
            case EnemyState.Attack:
                Attack();
                break;

        }
    }
    public void Initialize(EnemyStats stats)
    {
        _health = stats.Health;
        _speed = stats.Speed;
    }
    public void TakeDamage(int damage)
    {
        if (_health < 0)
        {
            return;
        }
        _health -= damage;
    }
    private void Move()
    {

    }
    private void GoTo()
    {
        if (target == null) return;
        agent.SetDestination(target.transform.position);
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < attackRange)
        {
            enemy_state = EnemyState.Attack;
        }

    }
    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer <= attackCD) return;
        attackTimer = 0;
        if (target == null) return;
        target.TakeDamage(damage);
    }
}
public enum EnemyState
{
    None=0,
    Idle=1,
    Chase=2,
    Attack=3
}
