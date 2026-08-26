using UnityEngine;

public class EnemyCont : MonoBehaviour
{
    private int _health;
    private float _speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initialize(EnemyStats stats)
    {
        _health = stats.Health;
        _speed = stats.Speed;
    }
}
