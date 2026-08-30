using UnityEngine;

public class ProjectTileCont : MonoBehaviour
{
    [SerializeField] private float _pushForce = 10f;
    [SerializeField] private Rigidbody _rb;
    private int _damage = 10;
    [SerializeField] private float _lifeTime = 5f;
    private float _timer = 0;
    
    public void Initialized(int damage, Vector3 pushDirection)
    {
        _damage = damage;
        _rb.AddForce(pushDirection*_pushForce, ForceMode.Impulse);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer>= _lifeTime)
        {
            OnExplosion();
        }
    }
    private void OnExplosion()
    {
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerCont>(out PlayerCont player))
        {
             player.TakeDamage(_damage);
            OnExplosion();
        }
        else if (collision.gameObject.TryGetComponent<EnemyCont>(out EnemyCont enemy))
        {
             enemy.TakeDamage(_damage);
            OnExplosion();
        }
        else OnExplosion();
    }
}
