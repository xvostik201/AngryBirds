using UnityEngine;

public abstract class Destructible : MonoBehaviour, IDestructible
{
    [SerializeField] protected float _deathSpeedThreshold = 2f;
    protected Rigidbody2D _rb;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDestructible>(out var other))
            other.Die();

        float relSpeed = collision.relativeVelocity.magnitude;
        if (relSpeed > _deathSpeedThreshold)
            Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}