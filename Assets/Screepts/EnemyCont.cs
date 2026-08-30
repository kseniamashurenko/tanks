using UnityEngine;

public class EnemyCont : MonoBehaviour
{
    private int _health;
    private float _speed;
    public EnemyState enemy_state {  get; private set; }=EnemyState.Idle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
     private void Update()
    {
        switch (enemy_state)
        {
            case EnemyState.Idle:
                Move();
                break;
            case EnemyState.Chase:
                Chase();
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
    private void Chase()
    {

    }
    private void Attack()
    {

    }
}
public enum EnemyState
{
    None=0,
    Idle=1,
    Chase=2,
    Attack=3
}
