using UnityEngine;

public abstract class Destructible : MonoBehaviour, IDestructible
{
    [SerializeField] private DestructibleConfig _config;

    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        float relSpeed = collision.relativeVelocity.magnitude;
        if (relSpeed > _config.BreakForce)
        {
            Die();
        }
        else
        {
            AudioManager.Instance.PlayRandomSFX(_config.ImpactClips);
        }
    }

    public virtual void Die()
    {
        AudioManager.Instance.PlayRandomSFX(_config.DeathClips);
        ParticlePoolManager.Instance.PlayAnimationAt(transform);
        Destroy(gameObject);
    }
}
